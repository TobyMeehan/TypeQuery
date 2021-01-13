using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TypeQuery
{
    public partial class SqlQuery<T> : SqlClause
    {
        private SqlStatement GetStatement() => Clauses.FirstOrDefault() as SqlStatement;

        protected SqlQuery<T> CloneQuery()
        {
            var query = new SqlQuery<T>();

            query.Clauses.AddRange(Clauses);

            return query;
        }

        private void SetStatement(SqlStatement value)
        {
            if (!Clauses.Any())
            {
                Clauses.Add(value);
            }

            if (Clauses[0] is SqlStatement)
            {
                Clauses[0] = value;
            }
            else
            {
                Clauses.Insert(0, value);
            }
        }

        private SqlQuery<T> AddWhere(Expression expression)
        {
            Clauses.Add(new WhereSqlClause(expression));

            return this;
        }

        public SqlQuery<T> Where(Expression<Predicate<T>> expression)
        {
            return CloneQuery().AddWhere(expression);
        }
    }
}
