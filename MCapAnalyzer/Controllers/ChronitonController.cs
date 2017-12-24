using Chroniton;
using Chroniton.Jobs;
using Chroniton.Schedules;
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
        readonly MCapAnalyzerContext _context;
        static List<string> _messages = new List<string>();

        public ChronitonController(ISingularity singularity, MCapAnalyzerContext context)
        {
            _singularity = singularity;
            _context = context;
        }

        public string Index()
        {
            if (_messages.Count == 0)
            {
                return "no messages yet";
            }
            return _messages.ToArray().Aggregate((s1, s2) => $"{s1}\r\n{s2}");
        }

        //public string Start(int minutes)
        //{
        //    var job = _singularity.ScheduleParameterizedJob(
        //        new EveryXTimeSchedule(TimeSpan.FromMinutes(minutes))
        //        , new SimpleParameterizedJob<string>(
        //            (msg, dt) => AddDateToDb(DateTime.Now)), "", true);

        //    return "started";
        //}

        private void AddDateToDb(DateTime now)
        {
            _context.MarketCapDataInfos.Add(new MarketCapInfo() { CreateDate = DateTime.Now });
        }
    }

}
