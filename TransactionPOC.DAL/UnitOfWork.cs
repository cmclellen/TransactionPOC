using System;
using System.Data;
using System.Data.SqlClient;
using TransactionPOC.Core.Data;
using TransactionPOC.Core.Utils;

namespace TransactionPOC.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(string connectionString)
        {
            Guard.NotNull(() => connectionString, connectionString);
            LazySqlConnection = new Lazy<SqlConnection>(() => new SqlConnection(connectionString));
        }
        
        private Lazy<SqlConnection> LazySqlConnection { get; set; }

        public SqlConnection GetConnection()
        {
            return LazySqlConnection.Value;
        }

        public IDbTransaction Transaction { get; set; }
    }
}