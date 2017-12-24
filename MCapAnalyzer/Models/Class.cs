using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCapAnalyzer.Models
{
    public class MCapAnalyzerContext : DbContext, IMCapAnalyzerContext
    {
        public MCapAnalyzerContext(DbContextOptions<MCapAnalyzerContext> options) : base(options)
        { }

        public DbSet<MarketCapInfo> MarketCapDataInfos { get; set; }

    

    }

    public class MarketCapInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
