using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCapAnalyzer.Application.DAO
{

    public class VolumeHistory
    {
        public double? hourlyDiff { get; set; }
        public string symbol { get; set; }
        public double? volume { get; set; }
        public DateTime? when { get; set; }
    }

}
