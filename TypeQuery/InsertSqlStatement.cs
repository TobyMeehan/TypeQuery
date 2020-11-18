using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class InsertSqlStatement : SqlStatement
    {
        public InsertSqlStatement(Type tableType)
        {
            Clauses.Add(new RawSqlClause($"INSERT INTO {tableType.GetTableName()}"));
        }

        public void AddValues(object values)
        {
            var properties = values.GetType().GetProperties();

            Clauses.AddRange(new RawSqlClause("("), new ListSqlClause(properties.Select(p => new RawSqlClause(p.Name))), new RawSqlClause(")"));

            Clauses.Add(new RawSqlClause("VALUES"));

            Clauses.AddRange(new RawSqlClause("("), new ListSqlClause(properties.Select(p => new ParameterSqlClause(p.GetValue(values)))), new RawSqlClause(")"));
        }
    }
}
