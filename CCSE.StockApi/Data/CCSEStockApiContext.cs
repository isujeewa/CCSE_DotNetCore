using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CCSE.StockApi.Models;

namespace CCSE.StockApi.Data
{
    public class CCSEStockApiContext : DbContext
    {
        public CCSEStockApiContext (DbContextOptions<CCSEStockApiContext> options)
            : base(options)
        {
        }

        public DbSet<CCSE.StockApi.Models.Stock> Stock { get; set; } = default!;
    }
}
