using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MCapAnalyzer.Application.DAO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MCapAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> CapsAsync(int numberOfToken)
        {
            var marketCapInfoList = GetMarketCapInfos(numberOfToken);
            var table = new List<TableRow>();
            foreach (MarketCap mc in marketCapInfoList)
            {
                var volumeHistoryList = GetVolumeHistory(mc.symbol);
                var marketCap = float.Parse(mc.market_cap_eur.Replace(".", ","));
                double hourlyMarketCapVolume = marketCap / Math.Abs(volumeHistoryList[0].hourlyDiff);
                double volume24h;
                double marketCapVolume = 0;
                if (double.TryParse(mc._24h_volume_eur.Replace(".", ","), out volume24h))
                    marketCapVolume = double.Parse(mc.market_cap_eur.Replace(".", ",")) / double.Parse(mc._24h_volume_eur.Replace(".", ","));
                table.Add(new TableRow()
                {
                    Name = mc.name,
                    Symbol = mc.symbol,
                    ValueBtc = mc.price_btc,
                    ValueEur = mc.price_eur,
                    Volume24h = mc._24h_volume_eur,
                    MarketCap = mc.market_cap_eur,
                    Change1h = mc.percent_change_1h,
                    Change7d = mc.percent_change_7d,
                    Change24h = mc.percent_change_24h,
                    HourlyMarketCapVolume = hourlyMarketCapVolume.ToString().Replace(",", "."),
                    HourlyVolumeDiff = volumeHistoryList[0].hourlyDiff.ToString(),
                    MarketCapVolume = marketCapVolume.ToString().Replace(",", ".")
                });
            }


            return Json(table);
        }

        private List<MarketCap> GetMarketCapInfos(int numberOfToken)
        {
            var client = new HttpClient();
            var streamTask = client.GetStringAsync("https://api.coinmarketcap.com/v1/ticker/?convert=EUR&limit=" + numberOfToken);
            return JsonConvert.DeserializeObject<List<MarketCap>>(streamTask.Result);
        }

        private List<VolumeHistory> GetVolumeHistory(string symbol)
        {
            var client = new HttpClient();
            var streamTask = client.GetStringAsync("http://dierre.chickenkiller.com/" + symbol);
            return JsonConvert.DeserializeObject<List<VolumeHistory>>(streamTask.Result);
        }
    }
}
