using Dapper;
using System;
using System.Data;
using System.Transactions;

namespace TransactionPOC.DAL.Repositories
{
    public interface IRepositoryB
    {
        void Create(DateTime dateTime);
    }

    public class RepositoryB : BaseRepository, IRepositoryB
    {
        public RepositoryB(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void Create(DateTime dateTime)
        {
            var conn = UnitOfWork.GetConnection();
            
            conn.Execute("insert dbo.TableB (val) values (@val)", new { val = dateTime }, UnitOfWork.Transaction);
        }
    }
}