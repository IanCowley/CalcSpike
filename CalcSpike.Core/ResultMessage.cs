namespace CalcSpike.Core
{
    public class ResultMessage
    {
        public ResultMessage(string resultKey, int result)
        {
            ResultKey = resultKey;
            Result = result;
        }

        public ResultMessage(string resultKey, int result, string messageId, string messagePopReceipt)
        {
            ResultKey = resultKey;
            Result = result;
            MessageId = messageId;
            PopReceipt = messagePopReceipt;
        }

        public static ResultMessage Parse(string message, string messageId, string messagePopReceipt)
        {
            if (!message.Contains("-"))
            {
                throw new CalcResultPersistenceException($"Message was not valid result message {message}");
            }

            var parts = message.Split('-');
            var key = parts[0];
            int result;

            if (!int.TryParse(parts[1], out result))
            {
                throw new CalcResultPersistenceException($"Message contained invalid result {message}");
            }

            return new ResultMessage(key, result, messageId, messagePopReceipt);
        }

        public string PopReceipt { get; set; }
        public string MessageId { get; set; }
        public string ResultKey { get; }
        public int Result { get; }

        public override string ToString()
        {
            return $"{ResultKey}-{Result}";
        }
    }
}