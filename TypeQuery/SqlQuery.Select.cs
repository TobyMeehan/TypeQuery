﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TypeQuery
{
    public partial class SqlQuery<T> : SqlClause
    {
        private SelectSqlStatement AddSelect()
        {
            if (!(GetStatement() is SelectSqlStatement selectStatement))
            {
                selectStatement = new SelectSqlStatement(typeof(T));
                SetStatement(selectStatement);
            }

            return selectStatement;
        }

        private SqlQuery<T> AddSelect(IEnumerable<string> columns)
        {
            AddSelect().AddColumns(columns);

            return this;
        }

        private SqlQuery<T> AddSelect<TTable>(IEnumerable<Expression<Func<TTable, object>>> columns)
        {
            AddSelect().AddColumns(columns);

            return this;
        }

        public SqlQuery<T> Select(params string[] columns)
        {
            return CloneQuery().AddSelect(columns);
        }

        public SqlQuery<T> Select(params Expression<Func<T, object>>[] columns)
        {
            return CloneQuery().AddSelect(columns);
        }

        public SqlQuery<T> Select<TForeign>(params Expression<Func<TForeign, object>>[] columns)
        {
            return CloneQuery().AddSelect(columns);
        }
    }
}