using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace TypeQuery.Dapper
{
    public static class DbConnectionExtensions
    {
        public static IEnumerable<T> Query<T>(this IDbConnection connection, SqlQuery<T> query, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.Query<T>(query.ToString(out object parameters), parameters, transaction, buffered, commandTimeout, commandType);
        }
        public static IEnumerable<T> Query<T>(this IDbConnection connection, SqlQuery<T> query, Type[] types, Func<object[], T> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.Query(query.ToString(out object parameters), types, map, parameters, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, SqlQuery<T> query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.QueryAsync<T>(query.ToString(out object parameters), parameters, transaction, commandTimeout, commandType);
        }
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, SqlQuery<T> query, Type[] types, Func<object[], T> map, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.QueryAsync(query.ToString(out object parameters), types, map, parameters, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        public static int Execute<T>(this IDbConnection connection, SqlQuery<T> query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.Execute(query.ToString(out object parameters), parameters, transaction, commandTimeout, commandType);
        }
        public static Task<int> ExecuteAsync<T>(this IDbConnection connection, SqlQuery<T> query, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return connection.ExecuteAsync(query.ToString(out object parameters), parameters, transaction, commandTimeout, commandType);
        }
    }
}
