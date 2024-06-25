using TestAPI.DTOs;
using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IStockReturnService
    {
        Task<StockReturn> CreateReturnAsync(int transactionId, int quantityReturned);
        Task<StockReturn> UpdateReturnAsync(StockReturn transaction);
    }
}
