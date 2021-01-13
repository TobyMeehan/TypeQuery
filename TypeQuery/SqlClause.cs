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
        protected List<SqlClause> Clauses { get; } = new List<SqlClause>();
        public IEnumerable<SqlClause> ChildClauses => Clauses;

        public SqlClause() { }

        public SqlClause(params SqlClause[] childClauses) : this(childClauses.ToList())
        {

        }

        public SqlClause(IEnumerable<SqlClause> childClauses)
        {
            Clauses = childClauses.ToList();
        }

        protected virtual SqlClause Clone()
        {
            return new SqlClause(Clauses.Select(c => c.Clone()));
        }

        public override string ToString()
        {
            return string.Join(" ", Clauses.Select(clause => clause.ToString()));
        }

        public virtual string ToString(out ParameterDictionary parameters)
        {
            parameters = new ParameterDictionary();

            foreach (var clause in Clauses)
            {
                clause.ToString(out ParameterDictionary clauseParameters);
                parameters = parameters.Union(clauseParameters).ToParameterDictionary();
            }

            return ToString();
        }

        public static SqlClause Join(IEnumerable<SqlClause> clauses, SqlClause separator)
        {
            SqlClause[] array = clauses.ToArray();

            if (array.Length == 0) return new SqlClause();
            if (array.Length == 1) return array[0];

            SqlClause clause = array[0];

            for (int i = 1; i < clauses.Count(); i++)
            {
                clause.Clauses.Add(separator);
                clause.Clauses.Add(array[i]);
            }

            return clause;
        }
    }
}
