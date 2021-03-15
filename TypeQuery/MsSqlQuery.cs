using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    public class MsSqlQuery<T> : SqlQuery<T>
    {
        protected override SqlQuery<T> CloneQuery()
        {
            var query = new MsSqlQuery<T>();

            query.Clauses.AddRange(Clauses);

            return query;
        }

        protected override SqlQuery<T> AddLimit(int limit, int offset)
        {
            Clauses.Add(new LimitMsSqlClause(limit, offset));

            return this;
        }
    }
}
