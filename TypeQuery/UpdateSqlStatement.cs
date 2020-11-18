using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class UpdateSqlStatement : SqlStatement
    {
        public UpdateSqlStatement(Type tableType)
        {
            Clauses.Add(new RawSqlClause($"UPDATE {tableType.GetTableName()} SET"));
        }

        public void AddValues(object values)
        {
            var properties = values.GetType().GetProperties();

            Clauses.Add(new ListSqlClause(
                properties.Select(p => new SqlClause(
                    new RawSqlClause("("),
                    new RawSqlClause(p.Name),
                    new RawSqlClause("="),
                    new ParameterSqlClause(p.GetValue(values)),
                    new RawSqlClause(")")))));
        }
    }
}
