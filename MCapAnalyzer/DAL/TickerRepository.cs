using MCapAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCapAnalyzer.DAL
{
    public class TickerRepository : ITickerRepository
    {
        private readonly MCapAnalyzerContext _context;

        public TickerRepository(MCapAnalyzerContext context)
        {
            _context = context;
        }

        public void Add(Ticker ticker)
        {
            _context.Tickers.Add(ticker);
            _context.SaveChanges();
        }
    }
}
