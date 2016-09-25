using DbMigratorCore.Configurations;
using System;
using System.Data.SqlClient;

namespace DbMigratorCore.Utils
{
    public class DbQuery
    {
        private readonly DbQueryOptions options;
        private SqlTransaction currentTransaction;

        public DbQuery(DbQueryOptions options)
        {
            this.options = options;
        }

        public SqlTransaction BeginTransaction()
        {
            var connection = new SqlConnection(options.ConnectionString);
            connection.Open();
            currentTransaction = connection.BeginTransaction();
            return currentTransaction;
        }

        public void ExecuteCommand(string queryString, params SqlParameter[] parameters)
        {
            Execute(queryString, command =>
            {
                command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            });
        }

        public T ExecuteQuery<T>(string queryString, params SqlParameter[] parameters) where T : struct
        {
            return Execute(queryString, command =>
            {
                command.Parameters.AddRange(parameters);
                return (T)command.ExecuteScalar();
            });
        }

        public T ExecuteReader<T>(string queryString, Func<SqlDataReader, T> readFunc, params SqlParameter[] parameters)
        {
            return Execute(queryString, command =>
            {
                command.Parameters.AddRange(parameters);
                return readFunc(command.ExecuteReader());
            });
        }

        private T Execute<T>(string queryString, Func<SqlCommand, T> func)
        {
            if(currentTransaction != null && currentTransaction.Connection != null)
            {
                SqlCommand command = new SqlCommand(queryString, currentTransaction.Connection);
                command.Transaction = currentTransaction;
                return func(command);
            }

            using (SqlConnection connection = new SqlConnection(
                       options.ConnectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                return func(command);
            }
        }

        public bool TableExists(string tableName)
        {
            bool exists;

            try
            {
                // ANSI SQL way.  Works in PostgreSQL, MSSQL, MySQL.  
                var cmd = $"select case when exists((select * from information_schema.tables where table_name = '{tableName}')) then 1 else 0 end";
                exists =  ExecuteQuery<int>(cmd) > 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
                try
                {
                    // Other RDBMS.  Graceful degradation
                    exists = true;
                    var cmdOthers = $"select 1 from '{tableName}' where 1 = 0";
                    ExecuteCommand(cmdOthers);
                }
                catch
                {
                    exists = false;
                }
            }

            return exists;
        }
    }
}
