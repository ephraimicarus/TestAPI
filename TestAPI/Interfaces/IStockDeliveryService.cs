using System.Transactions;
using TestAPI.DTOs;
using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IStockDeliveryService
    {
        Task<StockDelivery> CreateDeliveryAsync(TransactionDTO transaction);
        Task<StockDelivery> UpdateDeliveryAsync(int deliveryId, int quantity);
    }
}
