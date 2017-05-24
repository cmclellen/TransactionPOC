using TransactionPOC.Core.Data;
using TransactionPOC.Core.Utils;

namespace TransactionPOC.DAL.Repositories
{
    public abstract class BaseRepository : IRepository
    {
        public BaseRepository(UnitOfWork unitOfWork)
        {
            Guard.NotNull(() => unitOfWork, unitOfWork);
            UnitOfWork = unitOfWork;
        }

        protected UnitOfWork UnitOfWork { get; private set; }
    }
}