using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCapAnalyzer.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MCapAnalyzer.Models.MCapAnalyzerContext context)
        {
            context.Database.EnsureCreated();
                     
        }

      

    }

}
