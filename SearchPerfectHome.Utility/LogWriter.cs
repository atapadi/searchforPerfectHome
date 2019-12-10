using log4net;
using Newtonsoft.Json;
using System;

namespace SearchPerfectHome.Utility
{
    public static class LogWriter
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void LogException(string message, Exception ex)
        {
            Log.Error(message, ex);
        }

        public static void LogException(string message)
        {
            Log.Error(message);
        }

        public static void TraceMessage(string message, string eventName)
        {
            if (Log.IsDebugEnabled)
                Log.Debug($" {Environment.NewLine}{Environment.NewLine} Message:{message}; {Environment.NewLine} {eventName}");
        }

        public static void TraceMessage(string message)
        {
            if (Log.IsDebugEnabled)
                Log.Debug($"Message:{Environment.NewLine} {message}");
        }

        public static void TraceMessage(string message, object dataset)
        {
            if (Log.IsDebugEnabled)
            {
                Log.Debug($"Message: {message}; {Environment.NewLine} Data: {JsonConvert.SerializeObject(dataset)}");
            }
        }
    }
}
