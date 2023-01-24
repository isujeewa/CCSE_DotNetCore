using CCSE.StockApi.Models;
using System.Text.RegularExpressions;

namespace CCSE.StockApi.IRepositories
{
    public interface IStockRepository
    {
        Task<Stock> Get(string id);
        void Add(Stock stock);
        void Edit(Stock stock);
        void Delete(string id);




    }
}
