using System;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class LimitMySqlClause : SqlClause
    {
        public LimitMySqlClause(int limit, int offset)
        {
            Clauses.AddRange(
                new RawSqlClause("LIMIT"),
                new RawSqlClause(limit.ToString()));

            if (offset > 0)
            {
                Clauses.AddRange(
                    new RawSqlClause("OFFSET"),
                    new RawSqlClause(offset.ToString()));
            }
        }
    }
}