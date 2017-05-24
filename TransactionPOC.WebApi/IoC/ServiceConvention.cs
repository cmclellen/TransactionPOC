using Castle.DynamicProxy;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.Pipeline;
using StructureMap.TypeRules;
using System;
using System.Linq;
using TransactionPOC.Core.Services;
using TransactionPOC.WebApi.Interceptors;

namespace TransactionPOC.WebApi.IoC
{
    public class ServiceConvention : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, Registry registry)
        {
            foreach (var type in types.AllTypes())
            {
                if (type.CanBeCastTo<IService>() && !type.IsAbstract)
                {
                    var interfaces = type.AllInterfaces().ToList();
                    var intType = interfaces[0];
                    
                    registry
                        .For(intType)
                        .LifecycleIs(new TransientLifecycle())
                        .Use(ctx => GetInstance(intType, ctx.GetInstance(type)));
                }
            }
        }

        private object GetInstance(Type intType, object concrete)
        {
            var interceptors =
                new IInterceptor[]
                {
                    //new TxScopeInterceptor(),
                    new DapperTxInterceptor(),
                };

            ProxyGenerator proxyGenerator = new ProxyGenerator();
            return proxyGenerator.CreateInterfaceProxyWithTarget(intType, concrete, interceptors);
        }
    }
}