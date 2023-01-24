using CCSE.StockApi.Data;
using CCSE.StockApi.IRepositories;
using CCSE.StockApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CCSE.StockApi.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockDBContext _context;
        public void Add(Stock stock)
        {
            _context.Stock.Add(stock);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var stock = Get(id);
            _context.Stock.Remove(stock.Result);
            _context.SaveChanges();
        }

        public void Edit(Stock stock)
        {
            _context.Stock.Update(stock);
            _context.SaveChanges();
        }

        public async Task<Stock> Get(string id)
        {
            Stock item = await _context.Stock.FindAsync( id);
            return item;
        }
    }
}
