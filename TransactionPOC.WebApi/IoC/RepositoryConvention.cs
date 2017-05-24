using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.Pipeline;
using StructureMap.TypeRules;
using System.Linq;
using TransactionPOC.Core.Data;

namespace TransactionPOC.WebApi.IoC
{
    public class RepositoryConvention : IRegistrationConvention
    {
        public void ScanTypes(TypeSet types, Registry registry)
        {
            foreach (var type in types.AllTypes())
            {
                if (type.CanBeCastTo<IRepository>() && !type.IsAbstract)
                {
                    var intType = type.AllInterfaces().First(i => i != typeof(IRepository));
                    registry.For(intType).LifecycleIs(new TransientLifecycle()).Use(type);
                }
            }
        }
    }
}