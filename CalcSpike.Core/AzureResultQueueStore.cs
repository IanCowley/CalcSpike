using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;

namespace CalcSpike.Core
{
    public class AzureResultQueueStore
    {
        readonly string _storageAccountName;
        readonly string _storageAccountKey;

        public AzureResultQueueStore(string storageAccountName, string storageAccountKey)
        {
            _storageAccountName = storageAccountName;
            _storageAccountKey = storageAccountKey;
        }

        public void AddToQueue(string resultKey, int result)
        {
            try
            {
                GetQueue().AddMessage(new CloudQueueMessage(new ResultMessage(resultKey, result).ToString()));
            }
            catch (Exception ex)
            {
                throw new CalcResultPersistenceException($"Error adding result {result}, result key {resultKey} to queue", ex);
            }
        }

        
        public ResultMessage GetNext()
        {
            try
            {
                var message = GetQueue().GetMessage();
                if (message == null)
                {
                    return null;
                }

                return ResultMessage.Parse(message.AsString, message.Id, message.PopReceipt);
            }
            catch (Exception ex)
            {
                throw new CalcResultPersistenceException($"Error getting message from queue", ex);
            }
        }

        public void DeleteMessage(string messageId, string popReceipt)
        {
            try
            {
                GetQueue().DeleteMessage(messageId, popReceipt);
            }
            catch (Exception ex)
            {
                throw new CalcResultPersistenceException($"Error deleting message {messageId} with pop receipt {popReceipt}");
            }
        }

        CloudQueue GetQueue()
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(_storageAccountName, _storageAccountKey), true);
            return storageAccount.CreateCloudQueueClient().GetQueueReference(ResultsConstants.CalcResultQueueName);

        }
    }
}
