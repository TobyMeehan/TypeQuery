using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class OrderBySqlClause : SqlClause
    {
        public OrderBySqlClause(Expression[] columns, bool descending)
        {
            Clauses.AddRange(
                new RawSqlClause("ORDER BY"),
                SqlClause.Join(columns.Select(c => new SqlExpression(c)), new RawSqlClause(",")),
                new RawSqlClause(descending ? "DESC" : "ASC")
                );
        }
    }
}
