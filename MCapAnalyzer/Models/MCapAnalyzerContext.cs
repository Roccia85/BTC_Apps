using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCapAnalyzer.Models
{
    public class MCapAnalyzerContext : DbContext
    {
        public MCapAnalyzerContext(DbContextOptions<MCapAnalyzerContext> options) : base(options)
        { }

        public DbSet<Ticker> Tickers { get; set; }
        
    }
}
