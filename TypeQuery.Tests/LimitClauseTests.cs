using System;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Tests.Models;
using Xunit;

namespace TypeQuery.Tests
{
    public class LimitClauseTests
    {
        [Fact]
        public void ShouldGenerateMsSqlLimit()
        {
            var query = new MsSqlQuery<User>()
                .Limit(50, 25);

            string actual = query.ToString();

            Assert.Equal("OFFSET 25 ROWS FETCH NEXT 50 ROWS ONLY", actual);
        }

        [Fact]
        public void ShouldGenerateMySqlLimit()
        {
            var query = new MySqlQuery<User>()
                .Limit(75);

            string actual = query.ToString();

            Assert.Equal("LIMIT 75", actual);
        }

        [Fact]
        public void ShouldGenerateMySqlLimitWithOffset()
        {
            var query = new MySqlQuery<User>()
                .Limit(19, 20);

            string actual = query.ToString();

            Assert.Equal("LIMIT 19 OFFSET 20", actual);
        }
    }
}
