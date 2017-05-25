﻿using Castle.DynamicProxy;
using System.Data;
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
            var useTx = (txAttribute != null);
            IDbTransaction tx = null;

            UnitOfWork unitOfWork = ObjectFactory.Current.Resolve<UnitOfWork>();
            var conn = unitOfWork.GetConnection();
            conn.Open();

            if (useTx) {
                tx = unitOfWork.Transaction = conn.BeginTransaction();
                Logger.LogInfo("Transaction created");
            }
            try
            {   
                invocation.Proceed();
                if (useTx)
                {
                    tx.Commit();
                    Logger.LogInfo("Transaction committed");
                }
            }
            catch
            {
                if (useTx)
                {
                    tx.Rollback();
                    Logger.LogInfo("Transaction rolled back");
                }
                throw;
            }
            finally
            {
                if (useTx)
                {
                    tx.Dispose();
                }
            }
        }
    }
}