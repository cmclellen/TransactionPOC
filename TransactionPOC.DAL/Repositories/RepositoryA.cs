using Dapper;
using System;
using System.Data;
using System.Transactions;

namespace TransactionPOC.DAL.Repositories
{
    public interface IRepositoryA
    {
        void Create(DateTime dateTime);
    }

    public class RepositoryA : BaseRepository, IRepositoryA
    {
        public RepositoryA(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Create(DateTime dateTime)
        {
            var conn = UnitOfWork.GetConnection();
            
            conn.Execute("insert dbo.TableA (val) values (@val)", new { val = dateTime }, UnitOfWork.Transaction);
        }
    }
}