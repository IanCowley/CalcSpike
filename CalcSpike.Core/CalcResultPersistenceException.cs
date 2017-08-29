using System;

namespace CalcSpike.Core
{
    public class CalcResultPersistenceException : Exception
    {
        public CalcResultPersistenceException(string message) : base(message)
        {
        }

        public CalcResultPersistenceException(string message, Exception exception) : base(message, exception)
        {
            
        }
    }
}