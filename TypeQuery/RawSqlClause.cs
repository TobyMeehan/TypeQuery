using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    public class RawSqlClause : SqlClause
    {
        private readonly string _sql;

        public RawSqlClause(string sql)
        {
            _sql = sql;
        }

        public override string ToString()
        {
            return _sql;
        }
    }
}
