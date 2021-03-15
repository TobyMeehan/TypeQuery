using System;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class LimitMsSqlClause : SqlClause
    {
        public LimitMsSqlClause(int limit, int offset)
        {
            Clauses.AddRange(
                new RawSqlClause("OFFSET"),
                new RawSqlClause(offset.ToString()),
                new RawSqlClause("ROWS"),
                new RawSqlClause("FETCH NEXT"),
                new RawSqlClause(limit.ToString()),
                new RawSqlClause("ROWS ONLY"));
        }
    }
}
