namespace CalcSpike.Core
{
    public class AzureResultPublsher
    {
        readonly AzureResultQueueStore _resultQueueStore;

        public AzureResultPublsher(AzureResultQueueStore resultQueueStore)
        {
            _resultQueueStore = resultQueueStore;
        }

        public void PublishResult(string resultKey, int result)
        {
            _resultQueueStore.AddToQueue(resultKey, result);
        }
    }
}