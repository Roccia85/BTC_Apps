using MCapAnalyzer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MCapAnalyzer.Application.ExternalApi
{
    public interface ITickerApi
    {
        Task<List<Ticker>> ReadAllAsync(int numberOfToken);
    }

    public class TickerApi : ITickerApi
    {
        string baseUrl = "https://api.coinmarketcap.com/v1/ticker/?";

        public async Task<List<Ticker>> ReadAllAsync(int numberOfToken)
        {
            TickerDto tickerDto = null;
            List<Ticker> tickers = new List<Ticker>();
            using (HttpClient client = new HttpClient())

            using (HttpResponseMessage res = await client.GetAsync(baseUrl))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    tickerDto = JsonConvert.DeserializeObject<TickerDto>(data);
                }
            }

            foreach (TickerObj tickerObj in tickerDto.Property1)
            {
                Ticker ticker = Ticker.FromTickerObj(tickerObj);
                tickers.Add(ticker);
            }
            

            return tickers;
        }


    }
}
