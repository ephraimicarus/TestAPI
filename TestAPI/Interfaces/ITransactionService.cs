using BaseApi.Models;
using TestAPI.Models;

namespace BaseApi.Interfaces
{
	public interface ITransactionService
	{
		Task<TransactionModel> InitiateTransaction(string description);
		Task<StockJournal> AddStockJournalRecord(string category, int transactionId);
		Task<List<TransactionModel>> GetAllTransactions();
		Task<bool> IsTransactionActive(int transactionId);
        Task<TransactionModel> SetTransactionNotActive(int transactionId);
        Task<List<StockDelivery>> GetAllDeliveries();
		Task<List<StockReturn>> GetAllReturns();
		Task<List<StockDelivery>> GetStockDeliveriesByTransactionIdAsync(int transactionId);
        Task<List<StockReturn>> GetStockReturnsByDeliveryIdAsync(int deliveryId);
    }
}
