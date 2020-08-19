using System;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Collections;

namespace TypeQuery
{
    public class ParameterSqlClause : SqlClause
    {
        private readonly object _value;
        private readonly string _key;

        public ParameterSqlClause(object value)
        {
            _value = value;
            _key = Guid.NewGuid().ToString().Replace("-", "");
        }

        public override string ToString()
        {
            return $"@{_key}";
        }

        public override string ToString(out ParameterDictionary parameters)
        {
            parameters = new ParameterDictionary
            {
                { _key, _value }
            };

            return ToString();
        }
    }
}
