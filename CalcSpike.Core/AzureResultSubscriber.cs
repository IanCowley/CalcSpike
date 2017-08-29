using System;
using System.Threading;

namespace CalcSpike.Core
{
    public class AzureResultSubscriber
    {
        readonly AzureResultQueueStore _resultQueueStore;
        readonly AzureResultTableStore _resultTableStore;

        public AzureResultSubscriber(AzureResultQueueStore resultQueueStore, AzureResultTableStore resultTableStore)
        {
            _resultQueueStore = resultQueueStore;
            _resultTableStore = resultTableStore;
        }

        public void StartPolling(string calcAccountName)
        {
            while (true)
            {
                // depending on the use pattern, I might choose to do these in blocks rather that one record
                // would depend on what the processing time of the message is and the actual scenario etc...

                var message = _resultQueueStore.GetNext();

                if (message == null)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(3));
                    continue;
                }

                // check for idempotency            
                if (!_resultTableStore.Exists(calcAccountName, message.ResultKey))
                {
                    _resultTableStore.Store(calcAccountName, message.ResultKey, message.Result);
                    // DO other stuff you'd do at this time, clearly in the calc example there really isn't
                    // a ton to do, however if we did want other heuristics, totalling etc.. etc... this would
                    // be the time to do it, also enables us to do batching of operations etc... 
                }

                _resultQueueStore.DeleteMessage(message.MessageId, message.PopReceipt);
            }
        }
    }
}