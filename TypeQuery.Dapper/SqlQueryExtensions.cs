using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using TypeQuery.Collections;

namespace TypeQuery.Dapper
{
    public static class SqlQueryExtensions
    {
        public static string ToString<T>(this SqlQuery<T> query, out object parameters)
        {
            string result = query.ToString(out ParameterDictionary dict);

            parameters = new DynamicParameters(dict);

            return result;
        }
    }
}
