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
            var volumeHistoryList = GetVolumeHistory();
            var table = new List<TableRow>();
            foreach (MarketCap mc in marketCapInfoList)
            {
                try
                {
                    var volumeHistory = volumeHistoryList.Where(c => c.symbol == mc.symbol).First();
                    if (volumeHistory == null)
                        continue;
                    var marketCap = float.Parse(mc.market_cap_usd.Replace(".", ","));
                    double hourlyMarketCapVolume = 0;
                    if (volumeHistory.hourlyDiff.HasValue)
                        hourlyMarketCapVolume = marketCap / Math.Abs(volumeHistory.hourlyDiff.Value);
                    double volume24h;
                    double marketCapVolume = 0;
                    if (double.TryParse(mc._24h_volume_usd.Replace(".", ","), out volume24h))
                        marketCapVolume = double.Parse(mc.market_cap_usd.Replace(".", ",")) / double.Parse(mc._24h_volume_usd.Replace(".", ","));
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
                        HourlyVolumeDiff = volumeHistory.hourlyDiff.ToString().Replace(",", "."),
                        MarketCapVolume = marketCapVolume.ToString().Replace(",", ".")
                    });
                }
                catch (Exception ex)
                {


                }
            }


            return Json(table);
        }

        private List<MarketCap> GetMarketCapInfos(int numberOfToken)
        {
            var client = new HttpClient();
            var streamTask = client.GetStringAsync("https://api.coinmarketcap.com/v1/ticker/?convert=EUR&limit=" + numberOfToken);
            return JsonConvert.DeserializeObject<List<MarketCap>>(streamTask.Result);
        }

        private List<VolumeHistory> GetVolumeHistory()
        {
            try
            {
                var client = new HttpClient();
                var streamTask = client.GetStringAsync("http://dierre.chickenkiller.com/");
                return JsonConvert.DeserializeObject<List<VolumeHistory>>(streamTask.Result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
