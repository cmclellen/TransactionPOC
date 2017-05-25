using Castle.DynamicProxy;
using System.Linq;
using System.Reflection;
using System.Transactions;
using TransactionPOC.Core.Data;
using TransactionPOC.Core.Logging;

namespace TransactionPOC.WebApi.Interceptors
{
    public class TxScopeInterceptor : IInterceptor
    {
        private static ILogger Logger = LoggerFactory.Current.Create(MethodBase.GetCurrentMethod().DeclaringType);

        public TxScopeInterceptor()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            TransactionAttribute txAttribute = invocation.MethodInvocationTarget.GetCustomAttributes(true).FirstOrDefault(attr => attr is TransactionAttribute) as TransactionAttribute;
            var useTx = (txAttribute != null);
            TransactionScope scope = null;

            if (useTx)
            {
                scope = new TransactionScope(TransactionScopeOption.Required,
                    new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadCommitted,
                    });
                Logger.LogInfo("Transaction created");
            }
            try
            {                
                invocation.Proceed();
                if (useTx)
                {
                    scope.Complete();
                    Logger.LogInfo("Transaction committed");
                }
            }
            catch
            {
                if (useTx)
                {
                    Logger.LogInfo("Transaction rolled back");
                }
                throw;
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}