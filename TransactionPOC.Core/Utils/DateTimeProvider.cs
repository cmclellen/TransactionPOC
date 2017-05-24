using System;

namespace TransactionPOC.Core.Utils
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        private DateTimeProvider()
        {
        }

        private static IDateTimeProvider _current;

        public static IDateTimeProvider Current
        {
            get { return _current ?? (_current = new DateTimeProvider()); }
            set { _current = value; }
        }

        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}