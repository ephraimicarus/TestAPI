using BaseAppPerla.Models;

namespace BaseAppPerla.Interfaces
{
    public interface ITransactionService
    {
        Task<List<StockDelivery>> CreateDeliveryAsync(Dictionary<int, int> inventories);
        Task<List<StockReturn>> CreateReturnAsync(Dictionary<int, int> stockReturns);
        Task<List<TransactionModel>> GetAllTransactions();
        Task<List<StockDelivery>> GetAllDeliveries();
        Task<List<StockReturn>> GetAllReturns();
        Task<List<StockDelivery>> GetStockDeliveriesByTransactionIdAsync(int transactionId);
        Task<List<StockReturn>> GetStockReturnsByDeliveryIdAsync(int deliveryId);
    }
}
