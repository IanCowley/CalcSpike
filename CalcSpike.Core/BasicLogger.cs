using System.Collections.Concurrent;

namespace CalcSpike.Core
{
    public class BasicLogger : ILogger
    {
        ConcurrentBag<string> _logs;

        public BasicLogger()
        {
            _logs = new ConcurrentBag<string>();
        }

        public void Trace(string message)
        {
            _logs.Add(message);
        }
    }
}
