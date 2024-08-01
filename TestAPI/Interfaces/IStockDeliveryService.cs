using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IStockDeliveryService
    {
        Task<List<StockDelivery>> CreateDeliveryAsync(Dictionary<int, int> inventories);
        Task<StockDelivery> UpdateDeliveryAsync(int deliveryId, int quantity);
    }
}
