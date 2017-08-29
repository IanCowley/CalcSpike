using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace CalcSpike.Core
{
    public class InMemoryResultPersistence : ICalcResultPersistence
    {
        readonly ConcurrentDictionary<string, int> _persistence;

        public InMemoryResultPersistence()
        {
            _persistence = new ConcurrentDictionary<string, int>();    
        }

        public bool Exists(string calcAccountName, int left, int right)
        {
            string resultKey = GetResultKey(left, right);
            return _persistence.ContainsKey(resultKey);
        }

        public int Get(string calcAccountName, int left, int right)
        {
            if (!Exists(calcAccountName, left, right))
            {
                throw new CalcResultPersistenceException($"Have not stored {GetResultKey(left, right)}");
            }

            return _persistence[GetResultKey(left, right)];
        }

        public void Persist(string calcAccountName, int left, int right, int result)
        {
            _persistence.AddOrUpdate(GetResultKey(left, right), result, (key, value) => result);
        }

        string GetResultKey(int left, int right)
        {
            return $"{left}-{right}";
        }
    }
}
