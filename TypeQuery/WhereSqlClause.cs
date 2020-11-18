using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class WhereSqlClause : SqlClause
    {
        public WhereSqlClause(SqlExpression expression)
        {
            Clauses.AddRange(new RawSqlClause("WHERE"), expression);
        }

        public WhereSqlClause(Expression expression) : this(new SqlExpression(expression))
        {

        }
    }
}
