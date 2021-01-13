using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class JoinSqlClause : SqlClause
    {
        public JoinSqlClause(string joinType, Type tableType, SqlExpression expression)
        {
            Clauses.AddRange(new RawSqlClause($"{joinType} JOIN {tableType.GetTableName()} ON"), expression);
        }

        public JoinSqlClause(string joinType, Type tableType, Expression expression) : this (joinType, tableType, new SqlExpression(expression))
        {

        }
    }
}
