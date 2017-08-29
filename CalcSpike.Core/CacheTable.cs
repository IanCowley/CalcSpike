using Microsoft.WindowsAzure.Storage.Table;

namespace CalcSpike.Core
{
    public class CacheTable : TableEntity
    {
        public int Result { get; set; }
    }
}
