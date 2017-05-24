using Castle.DynamicProxy;
using System.Linq;
using System.Reflection;
using TransactionPOC.Core.Data;
using TransactionPOC.Core.IoC;
using TransactionPOC.Core.Logging;
using TransactionPOC.DAL;

namespace TransactionPOC.WebApi.Interceptors
{
    public class DapperTxInterceptor : IInterceptor
    {
        private static ILogger Logger = LoggerFactory.Current.Create(MethodBase.GetCurrentMethod().DeclaringType);

        public DapperTxInterceptor()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            TransactionAttribute txAttribute = invocation.MethodInvocationTarget.GetCustomAttributes(true).FirstOrDefault(attr => attr is TransactionAttribute) as TransactionAttribute;
            if (txAttribute == null)
            {
                return;
            }

            UnitOfWork unitOfWork = ObjectFactory.Current.Resolve<UnitOfWork>();
            var conn = unitOfWork.GetConnection();
            conn.Open();

            var tx = unitOfWork.Transaction = conn.BeginTransaction();
            try
            {
                Logger.LogInfo("Transaction created");
                invocation.Proceed();
                tx.Commit();
                Logger.LogInfo("Transaction committed");
            }
            catch
            {
                tx.Rollback();
                Logger.LogInfo("Transaction rolled back");
                throw;
            }
            finally
            {
                tx.Dispose();
            }
        }
    }
}