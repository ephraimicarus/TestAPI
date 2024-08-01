using BaseApi.Models;
using TestAPI.Models;

namespace BaseApi.Interfaces
{
	public interface ITransactionService
	{
		Task<TransactionModel> InitiateTransaction(string description);
		Task<StockJournal> AddStockJournalRecord(string category, int transactionId);
	}
}
