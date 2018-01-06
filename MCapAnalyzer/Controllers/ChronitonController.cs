using Chroniton;
using Chroniton.Jobs;
using Chroniton.Schedules;
using MCapAnalyzer.Application.ExternalApi;
using MCapAnalyzer.DAL;
using MCapAnalyzer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCapAnalyzer.Controllers
{
    public class ChronitonController : Controller
    {
        readonly ISingularity _singularity;
        readonly private ITickerRepository _marketCapDataInfosRepository;
        private readonly ITickerApi _tickerApi;
        static List<string> _messages = new List<string>();

        public ChronitonController(ISingularity singularity, ITickerRepository marketCapDataInfosRepository, ITickerApi tickerApi)
        {
            _singularity = singularity;
            _marketCapDataInfosRepository = marketCapDataInfosRepository;
            _tickerApi = tickerApi;
        }

        public string Index()
        {
            if (_messages.Count == 0)
            {
                return "no messages yet";
            }
            return _messages.ToArray().Aggregate((s1, s2) => $"{s1}\r\n{s2}");
        }

        public string Start(int minutes)
        {
            var job = _singularity.ScheduleParameterizedJob(
                new EveryXTimeSchedule(TimeSpan.FromMinutes(minutes))
                , new SimpleParameterizedJob<string>(
                    async (msg, dt) => await AddTickersToDbAsync()), "", true);

            return "started";
        }

        private async Task AddTickersToDbAsync()
        {
            var tickerList = await _tickerApi.ReadAllAsync(2);
            foreach (Ticker ticker in tickerList)
            {
                _marketCapDataInfosRepository.Add(ticker);
            }
        }
    }

}
