using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcSpike.Core
{
    public class InMemoryResultPersistence : ICalcResultPersistence
    {
        public bool Exists(int left, int right)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAsync(int left, int right)
        {
            throw new NotImplementedException();
        }

        public Task PersistAsync(int left, int right, int result)
        {
            throw new NotImplementedException();
        }
    }
}
