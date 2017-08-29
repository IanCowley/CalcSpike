using System;

namespace CalcSpike.Core
{
    public interface ICalcCache
    {
        int GetAnswerOrCalc(string accountName, int left, int right, Func<int, int, int> calc);
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

        public int GetAnswerOrCalc(string calcAccountName, int left, int right, Func<int, int, int> calc)
        {
            if (_calcResultPersistence.Exists(calcAccountName, left, right))
            {
                return _calcResultPersistence.Get(calcAccountName, left, right);
            }
            else
            {
                var result = calc(left, right);
                _logger.Trace($"Storing result for {left}, {right}, result was {result}");
                _calcResultPersistence.Persist(calcAccountName, left, right, result);
                return result;
            }
        }
    }
}
