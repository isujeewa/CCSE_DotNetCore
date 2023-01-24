using CCSE.StockApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace CCSE.StockApi.Data
{
    public class StockDBContext : DbContext
    {
        public DbSet<Stock> Stock { get; set; }
        public StockDBContext(DbContextOptions<StockDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>().ToTable("Stock").HasKey(o => o.id);
          
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
