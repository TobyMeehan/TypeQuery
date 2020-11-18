using System;
using System.Collections.Generic;
using System.Text;

namespace TypeQuery
{
    public class ListSqlClause : SqlClause
    {
        public ListSqlClause(IEnumerable<SqlClause> clauses) : base(clauses)
        {

        }

        public override string ToString()
        {
            return string.Join(", ", Clauses);
        }
    }
}
