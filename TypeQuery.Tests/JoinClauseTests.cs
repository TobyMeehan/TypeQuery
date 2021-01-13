using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeQuery.Collections;
using TypeQuery.Tests.Models;
using Xunit;

namespace TypeQuery.Tests
{
    public class JoinClauseTests
    {
        [Fact]
        public void ShouldGenerateInnerJoin()
        {
            var query = new SqlQuery<User>()
                .InnerJoin<Comment>((u, c) => c.UserId == u.Id);

            string actual = query.ToString();

            Assert.Equal("INNER JOIN comments ON comments.userid = users.id", actual);
        }

        [Fact]
        public void ShouldGenerateLeftJoin()
        {
            var query = new SqlQuery<User>()
                .LeftJoin<Comment>((u, c) => c.UserId == u.Id);

            string actual = query.ToString();

            Assert.Equal("LEFT JOIN comments ON comments.userid = users.id", actual);
        }

        [Fact]
        public void ShouldGenerateRightJoin()
        {
            var query = new SqlQuery<User>()
                .RightJoin<Comment>((u, c) => c.UserId == u.Id);

            string actual = query.ToString();

            Assert.Equal("RIGHT JOIN comments ON comments.userid = users.id", actual);
        }

        [Fact]
        public void ShouldGenerateFullJoin()
        {
            var query = new SqlQuery<User>()
                .FullJoin<Comment>((u, c) => c.UserId == u.Id);

            string actual = query.ToString();

            Assert.Equal("FULL OUTER JOIN comments ON comments.userid = users.id", actual);
        }

        [Fact]
        public void ShouldGenerateComplexJoin()
        {
            var query = new SqlQuery<User>()
                .Select()
                .LeftJoin<Comment>((u, c) => u.Id == c.UserId)
                .LeftJoin<Comment, Document>((c, d) => c.DocumentId == d.Id)
                .Where(u => u.Id == "Foo");

            string actual = query.ToString(out ParameterDictionary parameters);

            var param1 = parameters.Single(x => (string)x.Value == "Foo");

            Assert.Equal($"SELECT * FROM users " +
                $"LEFT JOIN comments ON users.id = comments.userid " +
                $"LEFT JOIN documents ON comments.documentid = documents.id " +
                $"WHERE users.id = @{param1.Key}", actual);
        }
    }
}
