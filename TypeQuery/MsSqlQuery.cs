using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    public class MsSqlQuery<T> : SqlQuery<T>
    {
        protected override SqlQuery<T> CloneQuery()
        {
            var query = new MySqlQuery<T>();

            query.Clauses.AddRange(Clauses);

            return query;
        }
    }
}
