using BaseApi.Interfaces;
using TestAPI.Data;
using TestAPI.Models;

namespace BaseApi.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly ApplicationContext _context;
		public TransactionService(ApplicationContext context) 
		{  
			_context = context; 
		}

		public async Task<StockJournal> AddStockJournalRecord(string category, int transactionId)
		{
			StockJournal stockJournal = new StockJournal()
			{
				Category = category,
				TransactionTypeId = transactionId
			};
			_context.StockJournals.Add(stockJournal);
			await _context.SaveChangesAsync();
			return stockJournal;
		}
	}
}
