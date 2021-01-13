using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class SelectSqlStatement : SqlStatement
    {
        public SelectSqlStatement(Type tableType)
        {
            Clauses.Add(new RawSqlClause("SELECT"));
            Clauses.Add(new RawSqlClause($"FROM {tableType.GetTableName()}"));
        }

        public SelectSqlStatement(Type tableType, params string[] columns) : this (tableType)
        {
            AddColumns(columns);
        }

        public SelectSqlStatement(Type tableType, params Expression[] columns) : this (tableType)
        {
            AddColumns(columns);
        }

        public void AddColumns(IEnumerable<string> columns)
        {
            Clauses.Insert(Clauses.Count - 1, SqlClause.Join(columns.Select(c => new RawSqlClause(c)), new RawSqlClause(",")));
        }

        public void AddColumns(IEnumerable<Expression> columns)
        {
            Clauses.Insert(Clauses.Count - 1, SqlClause.Join(columns.Select(c => new SqlExpression(c)), new RawSqlClause(",")));
        }
    }
}
