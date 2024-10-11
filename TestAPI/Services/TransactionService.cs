using BaseApi.Interfaces;
using BaseApi.Models;
using BaseApi.Utility;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<TransactionModel>> GetAllTransactions()
        {
            var transactions = await _context.Transactions
                .Where(t => t.TransactionDate > DateTime.Now.AddYears(-2))
                .ToListAsync();
            return transactions;
        }

        public async Task<List<StockDelivery>> GetAllDeliveries()
        {
            var stockDelivery = await _context.Deliveries
                .Include(sd => sd.Inventory)
                    .ThenInclude(i => i.Item)
                .Include(sd => sd.Inventory!.Customer)
                .Include(r => r.TransactionInfo)
                .OrderByDescending(sd => sd.TransactionInfo!.TransactionDate)
                .ToListAsync();
            return stockDelivery;
        }
        public async Task<List<StockReturn>> GetAllReturns()
        {
            var stockReturn = await _context.Returns
                .Include(r => r.Delivery)
                    .ThenInclude(d => d.TransactionInfo)
                .Include(r => r.Delivery!.Inventory!.Customer)
                .OrderByDescending(r => r.Delivery!.TransactionInfo!.TransactionDate)
                .ToListAsync();
            return stockReturn;
        }

        public async Task<TransactionModel> InitiateTransaction(string description)
        {
            if (description == TransactionCategory.Delivery.ToString())
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

        public async Task<bool> IsTransactionActive(int transactionId)
        {
            var deliveries = await _context.Deliveries
                .Where(d => d.TransactionInfo!.TransactionId == transactionId).ToListAsync();
            if (deliveries.All(d => d.QuantityToReturn == 0))
            {
                return false;
            }
            return true;
        }

        public async Task<TransactionModel> SetTransactionNotActive(int transactionId)
        {
            var transaction = _context.Transactions.SingleOrDefaultAsync(t => t.TransactionId == transactionId);
            if (transaction != null)
            {
                transaction.Result!.IsActive = false;
                await _context.SaveChangesAsync();
                return transaction.Result;
            }
            return null;
        }

        public async Task<List<StockDelivery>> GetStockDeliveriesByTransactionIdAsync(int transactionId)
        {
            var delivery = await _context.Deliveries
                .Include(d => d.Inventory)
                    .ThenInclude(c => c!.Customer)
                .Include(i => i.Inventory!.Item)
                .Include(d => d.TransactionInfo)
                .Where(d => d.TransactionInfo!.TransactionId == transactionId).ToListAsync();
            return delivery;
        }

        public async Task<List<StockReturn>> GetStockReturnsByDeliveryIdAsync(int deliveryId)
        {
            var stockReturn = await _context.Returns
                .Include(r => r.Delivery)
                    .ThenInclude(d => d!.TransactionInfo)
                .Include(r => r.Delivery!.Inventory!.Customer)
                .Include(r => r.Delivery!.Inventory!.Item)
                .Where(r => r.Delivery!.StockDeliveryId == deliveryId).ToListAsync();
            return stockReturn;
        }
    }
}
