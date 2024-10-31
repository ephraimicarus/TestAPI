using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IStockDeliveryService
    {
        Task<List<StockDelivery>> CreateDeliveryAsync(Dictionary<int, int> inventories);
        Task<List<StockDelivery>> GetStockDeliveriesByTransactionIdAsync(int transactionId);
        Task<List<StockDelivery>> GetALlStockDeliveriesAsync();
        Task<StockDelivery> UpdateDeliveryAsync(int deliveryId, int quantity);
        Task<List<StockDelivery>> GetStockDeliveriesByCustomerIdAsync(int customerId);
    }
}
