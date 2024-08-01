using BaseApi.Interfaces;
using BaseApi.Models;
using BaseApi.Utility;
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

        public async Task<TransactionModel> InitiateTransaction(string description)
        {
			if(description == TransactionCategory.Delivery.ToString())
			{
                TransactionModel transactionModel = new TransactionModel()
                {
                    Description = description,
                    DateDue = DateTime.Now.AddDays(7)
                };
                _context.Transactions.Add(transactionModel);
                await _context.SaveChangesAsync();
                return transactionModel;
            }
			else
			{
                TransactionModel transactionModel = new TransactionModel()
                {
                    Description = description,
                };
                _context.Transactions.Add(transactionModel);
                await _context.SaveChangesAsync();
                return transactionModel;
            }
            
        }
    }
}
