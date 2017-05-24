namespace TransactionPOC.Core.IoC
{
    public interface IObjectFactory
    {
        T Resolve<T>();
    }

    public class ObjectFactory
    {
        private ObjectFactory()
        {

        }

        public static IObjectFactory Current { get; set; }
    }
}