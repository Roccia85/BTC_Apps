using MCapAnalyzer.Models;
using System;

namespace MCapAnalyzer.DAL
{
    public interface ITickerRepository
    {
        void Add(Ticker ticker);
    }
}