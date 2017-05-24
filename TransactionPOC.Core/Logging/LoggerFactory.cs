using System;

namespace TransactionPOC.Core.Logging
{
    public interface ILoggerFactory
    {
        ILogger Create(Type type);
    }

    public class LoggerFactory : ILoggerFactory
    {
        private static ILoggerFactory _current;

        public static ILoggerFactory Current
        {
            get { return _current ?? (_current = new LoggerFactory()); }
            set { _current = value; }
        }

        public ILogger Create(Type type)
        {
            return new TextWriterLogger(type);
        }
    }
}