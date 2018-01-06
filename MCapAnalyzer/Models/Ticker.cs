using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MCapAnalyzer.Application.ExternalApi;

namespace MCapAnalyzer.Models
{
    public class Ticker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }
        public DateTime Created { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Rank { get; set; }
        public string Price_usd { get; set; }
        public string Price_btc { get; set; }
        public string _24h_volume_usd { get; set; }
        public string Market_cap_usd { get; set; }
        public string Available_supply { get; set; }
        public string Total_supply { get; set; }
        public string Max_supply { get; set; }
        public string Percent_change_1h { get; set; }
        public string Percent_change_24h { get; set; }
        public string Percent_change_7d { get; set; }

        internal static Ticker FromTickerDto(TickerDto tickerDto)
        {
            throw new NotImplementedException();
        }

        internal static Ticker FromTickerObj(TickerObj tickerObj)
        {
            throw new NotImplementedException();
        }

        public string Last_updated { get; set; }
        public string Price_eur { get; set; }
        public string _24h_volume_eur { get; set; }
        public string Market_cap_eur { get; set; }
    }
}
