using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Common.Core.DataAccess
{
    public class DbQuery : IDbQuery
    {
        private readonly DbQueryOptions options;
        private SqlTransaction currentTransaction;
        private SqlConnection connection;
        private bool disposed = false;

        public DbQuery(IOptions<DbQueryOptions> options)
        {
            this.options = options.Value;
            connection = new SqlConnection(this.options.ConnectionString);
            connection.Open();
        }

        public SqlTransaction BeginTransaction()
        {
            currentTransaction = connection.BeginTransaction();
            return currentTransaction;
        }

        public Task ExecuteCommandAsync(string queryString, params SqlParameter[] parameters)
        {
            return Execute(queryString, command =>
            {
                command.Parameters.AddRange(parameters);
                return command.ExecuteNonQueryAsync();
            });
        }

        public Task<long> ExecuteInsertCommandAsync(string queryString, params SqlParameter[] parameters)
        {
            queryString += "; SELECT CAST(scope_identity() AS bigint)";
            return Execute(queryString, async command =>
            {
                command.Parameters.AddRange(parameters);
                return (long) await command.ExecuteScalarAsync();
            });
        }

        public Task<T> ExecuteQueryAsync<T>(string queryString, params SqlParameter[] parameters) where T : struct
        {
            return Execute(queryString, async command =>
            {
                command.Parameters.AddRange(parameters);
                return (T) await command.ExecuteScalarAsync();
            });
        }

        public Task<T> ExecuteReaderAsync<T>(string queryString, Func<SqlDataReader, T> readFunc, params SqlParameter[] parameters)
        {
            return Execute(queryString, async command =>
            {
                command.Parameters.AddRange(parameters);
                return readFunc(await command.ExecuteReaderAsync());
            });
        }

        private async Task<T> Execute<T>(string queryString, Func<SqlCommand, Task<T>> func)
        {
            if (currentTransaction != null && currentTransaction.Connection != null)
            {
                return await Execute(queryString, func, currentTransaction);
            }

            using (var transaction = BeginTransaction())
            {
                try
                {
                    var result = await Execute(queryString, func, transaction);
                    transaction.Commit();
                    return result;
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        private Task<T> Execute<T>(string queryString, Func<SqlCommand, Task<T>> func, SqlTransaction transaction)
        {
            var command = new SqlCommand(queryString, transaction.Connection);
            command.Transaction = transaction;
            return func(command);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
                if(currentTransaction != null)
                {
                    currentTransaction.Dispose();
                }
                connection.Dispose();
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~DbQuery()
        {
            Dispose(false);
        }
    }
}
