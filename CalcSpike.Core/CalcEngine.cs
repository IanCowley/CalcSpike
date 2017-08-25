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

        public async Task<int> AddAsync(int left, int right)
        {
            _logger.Trace($"Adding {left}, {right}");

            return await _calcCache.GetAnswerOrCalcAsync(left, right, (l, r) => l + r);
        }
    }
}
