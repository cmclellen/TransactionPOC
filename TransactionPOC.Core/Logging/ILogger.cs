using System;
using System.IO;
using System.Text;

namespace TransactionPOC.Core.Logging
{
    public interface ILogger
    {
        void LogError(string msg, Exception ex);
        void LogInfo(string message);
    }

    public class Logger : ILogger
    {
        private Logger()
        {
        }

        private TextWriter Writer { get; } = Console.Out;

        public void LogError(string message, Exception exception)
        {
            Log("ERR", message, exception);
        }

        public void LogInfo(string message)
        {
            Log(null, message, null);
        }

        private void Log(string prefix, string message, Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(prefix))
            {
                sb.Append($"{prefix}: ");
            }
            sb.Append(message);
            if (ex != null)
            {
                sb.Append(ex);
            }
            Writer.WriteLine(sb);
        }
    }
}