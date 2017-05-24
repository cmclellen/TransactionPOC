using StructureMap;
using System.Configuration;
using TransactionPOC.BLL.Services;
using TransactionPOC.DAL;
using TransactionPOC.DAL.Repositories;
using TransactionPOC.WebApi.Controllers;

namespace TransactionPOC.WebApi.IoC
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(_ => {
                _.AssemblyContainingType<IRepositoryA>();
                _.AssemblyContainingType<IUpdate2TablesService>();

                _.Convention<RepositoryConvention>();
                _.Convention<ServiceConvention>();
                //_.WithDefaultConventions();
            });
            For<Update2TablesController>();
            For<UnitOfWork>().Singleton().Use(ctx => new UnitOfWork(ConfigurationManager.ConnectionStrings["some-database"].ConnectionString));
        }
    }
}