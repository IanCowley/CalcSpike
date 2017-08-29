namespace CalcSpike.Core
{
    public class AzureResultPersistence : ICalcResultPersistence
    {
        readonly AzureResultTableStore _tableStore;
        readonly AzureResultPublsher _resultPublisher;

        public AzureResultPersistence(AzureResultTableStore tableStore, AzureResultPublsher resultPublIsher)
        {
            _tableStore = tableStore;
            _resultPublisher = resultPublIsher;
        }

        public bool Exists(string calcAccountName, int left, int right)
        {
            return _tableStore.Exists(calcAccountName, GetResultKey(left, right));
        }

        public int Get(string calcAccountName, int left, int right)
        {
            var resultKey = GetResultKey(left, right);
            var result = _tableStore.Get(calcAccountName, resultKey);

            if (result == null)
            {
                throw new CalcResultPersistenceException($"Could not find result key {resultKey} for calc account {calcAccountName}");
            }

            return result.Value;
        }

        public void Persist(string calcAccountName, int left, int right, int result)
        {
            _resultPublisher.PublishResult(GetResultKey(left, right), result);
        }

        string GetResultKey(int left, int right)
        {
            return $"{left}-{right}";
        }
    }
}