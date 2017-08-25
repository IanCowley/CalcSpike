using System.Threading.Tasks;

namespace CalcSpike.Core
{
    public interface ICalcResultPersistence
    {
        bool Exists(int left, int right);
        Task<int> GetAsync(int left, int right);
        Task PersistAsync(int left, int right, int result);
    }
}