using System;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Tests.Models;
using Xunit;

namespace TypeQuery.Tests
{
    public class OrderByClauseTests
    {
        [Fact]
        public void ShouldGenerateSingleAscendingOrder()
        {
            var query = new SqlQuery<User>()
                .OrderBy(x => x.Name);

            string actual = query.ToString();

            Assert.Equal("ORDER BY users.name ASC", actual);
        }

        [Fact]
        public void ShouldGenerateSingleDescendingOrder()
        {
            var query = new SqlQuery<User>()
                .OrderByDescending(x => x.Name);

            string actual = query.ToString();

            Assert.Equal("ORDER BY users.name DESC", actual);
        }

        [Fact]
        public void ShouldGenerateMultipleOrder()
        {
            var query = new SqlQuery<User>()
                .OrderBy(x => x.Id, x => x.Name);

            string actual = query.ToString();

            Assert.Equal("ORDER BY users.id , users.name ASC", actual);
        }
    }
}
