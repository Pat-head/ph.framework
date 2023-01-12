using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PatHead.Framework.Uow.EFCore
{
    public static class EntityFrameworkCoreExtension
    {
        private static void CreateConnection(DatabaseFacade facade, string sql, out DbConnection connection,
            params object[] parameters)
        {
            var conn = facade.GetDbConnection();
            connection = conn;
            conn.Open();
        }

        private static List<T> DoSqlQuery<T>(this DatabaseFacade facade, string sql, object parameters)
        {
            CreateConnection(facade, sql, out DbConnection conn, parameters);
            try
            {
                return conn.Query<T>(sql, parameters).ToList();
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<T> SqlQuery<T>(this DatabaseFacade facade, string sql, object parameters)
        {
            return DoSqlQuery<T>(facade, sql, parameters);
        }
    }
}