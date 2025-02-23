using BaseAppPerla.DTOs;
using BaseAppPerla.ExceptionHandling;
using BaseAppPerla.Models;

namespace BaseAppPerla.Interfaces
{
    public interface ITransactionService
    {
        Task<ServiceResult<List<StockDelivery>>> CreateDeliveryAsync(List<InventoryDto> inventories);
        Task<ServiceResult<List<StockReturn>>> CreateReturnAsync(Dictionary<int, int> stockReturns);
        Task<List<TransactionModel>> GetAllTransactions();
        Task<List<StockDelivery>> GetAllDeliveries();
        Task<List<StockReturn>> GetAllReturns();
        Task<List<StockReturnDto>> GetStockDeliveriesByTransactionIdAsync(int transactionId);
        Task<List<StockDelivery>> GetStockDeliveriesByCustomerIdAsync(int transactionId);
        Task<List<StockReturn>> GetStockReturnsByDeliveryIdAsync(int deliveryId);
    }
}
