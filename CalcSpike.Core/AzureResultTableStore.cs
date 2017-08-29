using System;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace CalcSpike.Core
{
    public class AzureResultTableStore 
    {
        readonly string _storageAccountName;
        readonly string _storageAccountKey;

        public AzureResultTableStore(string storageAccountName, string storageAccountKey)
        {
            _storageAccountName = storageAccountName;
            _storageAccountKey = storageAccountKey;
        }

        public bool Exists(string calcAccountName, string resultKey)
        {
            try
            {
                return Get(calcAccountName, resultKey) != null;
            }
            catch (Exception ex)
            {
                throw new CalcResultPersistenceException($"Error checking if accountName {calcAccountName}, result key {resultKey} exists", ex);
            }
        }

        public int? Get(string calcAccountName, string resultKey)
        {
            try
            {
                var tableQuery = new TableQuery<CacheTable>().Where(
                    TableQuery.CombineFilters(
                        TableQuery.GenerateFilterCondition(nameof(CacheTable.PartitionKey), QueryComparisons.Equal, calcAccountName),
                        TableOperators.And,
                        TableQuery.GenerateFilterCondition(nameof(CacheTable.RowKey), QueryComparisons.Equal, resultKey)));

                var result = GetTableReference().ExecuteQuery(tableQuery).SingleOrDefault();

                return result?.Result;
            }
            catch (Exception ex)
            {
                throw new CalcResultPersistenceException($"Error Getting account name {calcAccountName}, result key {resultKey}", ex);
            }
        }

        public void Store(string cloudAccountName, string resultKey, int result)
        {
            // I would more than likely code in some concurrency checking here, for the sake of idempotency
            GetTableReference().Execute(TableOperation.Insert(new CacheTable
            {
                PartitionKey = cloudAccountName,
                RowKey = resultKey,
                Result = result
            }));
        }

        CloudTable GetTableReference()
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(_storageAccountName, _storageAccountKey), true);
            return storageAccount.CreateCloudTableClient().GetTableReference(ResultsConstants.CalResultsTableName);

        }
    }
}
