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

        private SqlQuery<T> AddJoin<TLeft, TRight>(string joinType, Expression<Func<TLeft, TRight, bool>> expression)
        {
            Clauses.Add(new JoinSqlClause(joinType, typeof(TRight), expression));

            return this;
        }

        public SqlQuery<T> InnerJoin<TJoin>(Expression<Func<T, TJoin, bool>> joinOn) => CloneQuery().AddJoin("INNER", joinOn);
        public SqlQuery<T> InnerJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> joinOn) => CloneQuery().AddJoin("INNER", joinOn);

        public SqlQuery<T> LeftJoin<TJoin>(Expression<Func<T, TJoin, bool>> joinOn) => CloneQuery().AddJoin("LEFT", joinOn);
        public SqlQuery<T> LeftJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> joinOn) => CloneQuery().AddJoin("LEFT", joinOn);

        public SqlQuery<T> RightJoin<TJoin>(Expression<Func<T, TJoin, bool>> joinOn) => CloneQuery().AddJoin("RIGHT", joinOn);
        public SqlQuery<T> RightJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> joinOn) => CloneQuery().AddJoin("RIGHT", joinOn);

        public SqlQuery<T> FullJoin<TJoin>(Expression<Func<T, TJoin, bool>> joinOn) => CloneQuery().AddJoin("FULL OUTER", joinOn);
        public SqlQuery<T> FullJoin<TLeft, TRight>(Expression<Func<TLeft, TRight, bool>> joinOn) => CloneQuery().AddJoin("FULL OUTER", joinOn);
    }
}