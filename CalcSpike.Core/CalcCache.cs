using System;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace CalcSpike.Core
{
    public interface ICalcCache
    {
        Task<int> GetAnswerOrCalcAsync(int left, int right, Func<int, int, int> calc);
    }

    public class CalcCache : ICalcCache
    {
        readonly ICalcResultPersistence _calcResultPersistence;
        readonly ILogger _logger;

        public CalcCache(ICalcResultPersistence calcResultPersistence, ILogger logger)
        {
            _calcResultPersistence = calcResultPersistence;
            _logger = logger;
        }

        public async Task<int> GetAnswerOrCalcAsync(int left, int right, Func<int, int, int> calc)
        {
            if (_calcResultPersistence.Exists(left, right))
            {
                return await _calcResultPersistence.GetAsync(left, right);
            }
            else
            {
                var result = calc(left, right);
                _logger.Trace($"Storing result for {left}, {right}, result was {result}");
                await _calcResultPersistence.PersistAsync(left, right, result);
                
                return result;
            }
        }
    }
}
