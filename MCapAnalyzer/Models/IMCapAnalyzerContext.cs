using Microsoft.EntityFrameworkCore;

namespace MCapAnalyzer.Models
{
    public interface IMCapAnalyzerContext
    {
        DbSet<MarketCapInfo> MarketCapDataInfos { get; set; }
    }
}