using StructureMap;
using TransactionPOC.Core.IoC;
using TransactionPOC.Core.Utils;

namespace TransactionPOC.WebApi.IoC
{
    public class StructureMapObjectFactory : IObjectFactory
    {
        public StructureMapObjectFactory(Container container)
        {
            Guard.NotNull(() => container, container);
            this.Container = container;
        }

        private Container Container { get; set; }

        public T Resolve<T>()
        {
            return Container.GetInstance<T>();
        }
    }
}