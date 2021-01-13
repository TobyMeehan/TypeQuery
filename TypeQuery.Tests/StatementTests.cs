using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeQuery.Collections;
using TypeQuery.Tests.Models;
using Xunit;

namespace TypeQuery.Tests
{
    public class StatementTests
    {
        [Fact]
        public void SelectWithString()
        {
            var query = new SqlQuery<User>()
                .Select();

            string actual = query.ToString();

            Assert.Equal("SELECT * FROM users", actual);
        }

        [Fact]
        public void SelectWithExpression()
        {
            var query = new SqlQuery<User>()
                .Select(u => u.Id, u => u.Name);

            string actual = query.ToString();

            Assert.Equal("SELECT users.id , users.name FROM users", actual);
        }

        [Fact]
        public void Insert()
        {
            var query = new SqlQuery<User>()
                .Insert(new User
                {
                    Id = "0000-0000-0000-0000",
                    Name = "Name"
                });

            string actual = query.ToString(out ParameterDictionary parameters);

            var param1 = parameters.Single(x => (string)x.Value == "0000-0000-0000-0000");
            var param2 = parameters.Single(x => (string)x.Value == "Name");

            Assert.Equal($"INSERT INTO users ( Id, Name ) VALUES ( @{param1.Key}, @{param2.Key} )", actual);
        }

        [Fact]
        public void Update()
        {
            var query = new SqlQuery<User>()
                .Update(new
                {
                    Name = "NewName"
                });

            string actual = query.ToString(out ParameterDictionary parameters);

            var param1 = parameters.Single(x => (string)x.Value == "NewName");

            Assert.Equal($"UPDATE users SET ( Name = @{param1.Key} )", actual);
        }

        [Fact]
        public void Delete()
        {
            var query = new SqlQuery<User>()
                .Delete();

            string actual = query.ToString();

            Assert.Equal("DELETE FROM users", actual);
        }
    }
}
