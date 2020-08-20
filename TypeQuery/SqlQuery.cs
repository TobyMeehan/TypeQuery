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
            return base.Clone() as SqlQuery<T>;
        }

        private void SetStatement(SqlStatement value)
        {
            if (!Clauses.Any())
            {
                return;
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
    }
}
