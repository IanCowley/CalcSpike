using System.Threading.Tasks;

namespace CalcSpike.Core
{
    public class CalcEngine
    {
        readonly CalcCache _calcCache;
        readonly ILogger _logger;

        public CalcEngine(CalcCache calcCache, ILogger logger)
        {
            _calcCache = calcCache;
            _logger = logger;
        }

        public int AddAsync(string calcAccountName, int left, int right)
        {
            _logger.Trace($"Adding {left}, {right}");
            return _calcCache.GetAnswerOrCalc(calcAccountName, left, right, (l, r) => l + r);
        }
    }
}
