using TestAPI.DTOs;
using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IStockReturnService
    {
        Task<List<StockReturn>> CreateReturnAsync(Dictionary<int, int> stockReturns);
        Task<StockReturn> UpdateReturnAsync(StockReturn transaction);
        Task<List<StockReturn>> GetAllStockReturnsAsync();
    }
}
