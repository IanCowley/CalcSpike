namespace CalcSpike.Core
{
    public interface ICalcResultPersistence
    {
        bool Exists(string calcAccountName, int left, int right);
        int Get(string calcAccountName, int left, int right);
        void Persist(string calcAccountName, int left, int right, int result);
    }
}