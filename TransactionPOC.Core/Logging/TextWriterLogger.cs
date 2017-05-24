using System;
using System.IO;
using System.Text;
using TransactionPOC.Core.Utils;

namespace TransactionPOC.Core.Logging
{
    internal class TextWriterLogger : ILogger
    {
        public TextWriterLogger(Type type, TextWriter writer = null)
        {
            Guard.NotNull(() => type, type);
            Type = type;
            Writer = writer ?? Console.Out;
        }

        private Type Type { get; set; }
        private TextWriter Writer { get; set; }

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