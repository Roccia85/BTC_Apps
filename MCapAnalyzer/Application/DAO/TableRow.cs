using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCapAnalyzer.Application.DAO
{
    public class TableRow
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string ValueEur { get; set; }
        public string ValueBtc { get; set; }
        public string Volume24h { get; set; }
        public string MarketCap { get; set; }
        public string Change1h { get; set; }
        public string Change24h { get; set; }
        public string Change7d { get; set; }
        public string HourlyVolumeDiff { get; set; }
        public string HourlyMarketCapVolume { get; set; }
        public string MarketCapVolume { get; set; }
    }
}
