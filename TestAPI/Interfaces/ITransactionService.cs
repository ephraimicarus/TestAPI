using TestAPI.Models;

namespace BaseApi.Interfaces
{
	public interface ITransactionService
	{
		Task<StockJournal> AddStockJournalRecord(string category, int transactionId);
	}
}
