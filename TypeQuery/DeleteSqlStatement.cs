using System;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class DeleteSqlStatement : SqlStatement
    {
        public DeleteSqlStatement(Type tableType)
        {
            Clauses.Add(new RawSqlClause($"DELETE FROM {tableType.GetTableName()}"));
        }
    }
}
