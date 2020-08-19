using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeQuery.Collections;
using TypeQuery.Extensions;

namespace TypeQuery
{
    public class SqlClause
    {
        private readonly string _sql;

        protected List<SqlClause> Clauses = new List<SqlClause>();
        public IEnumerable<SqlClause> ChildClauses => Clauses;

        public SqlClause() { }

        public SqlClause(string sql)
        {
            _sql = sql;
        }

        public SqlClause(IEnumerable<SqlClause> childClauses)
        {
            Clauses = childClauses.ToList();
        }

        public override string ToString()
        {
            if (Clauses.Any())
            {
                return string.Join(" ", Clauses.Select(clause => clause.ToString()));
            }

            return _sql;
        }

        public virtual string ToString(out ParameterDictionary parameters)
        {
            parameters = new ParameterDictionary();

            if (Clauses.Any())
            {
                foreach (var clause in Clauses)
                {
                    clause.ToString(out ParameterDictionary clauseParameters);
                    parameters = parameters.Union(clauseParameters).ToParameterDictionary();
                }
            }

            return ToString();
        }
    }
}
