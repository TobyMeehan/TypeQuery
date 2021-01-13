using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeQuery.Collections;
using TypeQuery.Tests.Models;
using Xunit;

namespace TypeQuery.Tests
{
    public class ExpressionTests
    {
        [Fact]
        public void ShouldGenerateSimpleExpression()
        {
            var query = new SqlQuery<User>()
                .Where(x => x.Id == "E");

            string actual = query.ToString(out ParameterDictionary parameters);

            var param1 = parameters.Single(x => (string)x.Value == "E");

            Assert.Equal($"WHERE users.id = @{param1.Key}", actual);
        }

        [Fact]
        public void ShouldGenerateComplexExpression()
        {
            var query = new SqlQuery<User>()
                .Where(x => x.Id == "Foo" && (x.Name == "Bar" || x.Name == "E"));

            string actual = query.ToString(out ParameterDictionary parameters);

            var param1 = parameters.Single(x => (string)x.Value == "Foo");
            var param2 = parameters.Single(x => (string)x.Value == "Bar");
            var param3 = parameters.Single(x => (string)x.Value == "E");

            Assert.Equal($"WHERE ( users.id = @{param1.Key} AND ( users.name = @{param2.Key} OR users.name = @{param3.Key} ) )", actual);
        }

        private string GetId(User user) => user.Id;
        [Fact]
        public void ShouldEvaluateMethodCall()
        {
            User user = new User { Id = "8" };

            var query = new SqlQuery<User>()
                .Where(x => x.Id == GetId(user));

            string actual = query.ToString(out ParameterDictionary parameters);

            var param1 = parameters.Single(x => (string)x.Value == "8");

            Assert.Equal($"WHERE users.id = @{param1.Key}", actual);
        }
    }
}
