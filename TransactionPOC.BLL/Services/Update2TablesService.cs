using System;
using TransactionPOC.Core.Data;
using TransactionPOC.Core.Services;
using TransactionPOC.Core.Utils;
using TransactionPOC.DAL.Repositories;

namespace TransactionPOC.BLL.Services
{
    public interface IUpdate2TablesService : IService
    {
        void Update2Tables();
    }

    public class Update2TablesService : IUpdate2TablesService
    {
        public Update2TablesService(IRepositoryA repositoryA, IRepositoryB repositoryB)
        {
            Guard.NotNull(() => repositoryA, repositoryA);
            Guard.NotNull(() => repositoryB, repositoryB);
            RepositoryA = repositoryA;
            RepositoryB = repositoryB;
        }

        private IRepositoryA RepositoryA { get; set; }
        private IRepositoryB RepositoryB { get; set; }

        [Transaction]
        public void Update2Tables()
        {
            var now = DateTimeProvider.Current.Now;
            RepositoryA.Create(now);
            //throw new Exception("Some exception");
            RepositoryB.Create(now);
        }
    }
}