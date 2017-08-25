using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool Exists(int left, int right)
        {
            string storageKey = GetStorageKey(left, right);
            return _persistence.ContainsKey(storageKey);
        }

        public Task<int> GetAsync(int left, int right)
        {
            if (!Exists(left, right))
            {
                throw new CalcResultPersistenceException($"Have not stored {GetStorageKey(left, right)}");
            }

            return Task.FromResult(_persistence[GetStorageKey(left, right)]);
        }

        public async Task PersistAsync(int left, int right, int result)
        {
            await Task.Run( () => _persistence.AddOrUpdate(GetStorageKey(left, right), result, (key, value) => result));
        }

        string GetStorageKey(int left, int right)
        {
            return $"{left}-{right}";
        }
    }
}
