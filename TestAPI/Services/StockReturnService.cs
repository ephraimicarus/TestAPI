using BaseApi.Interfaces;
using BaseApi.Utility;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

namespace BaseApi.Services
{
    public class StockReturnService : IStockReturnService
    {
        private readonly ApplicationContext _context;
        private readonly IInventoryService _inventoryService;
        private readonly ITransactionService _transactionService;
        private readonly IStockDeliveryService _stockDeliveryService;
        public StockReturnService(ApplicationContext context,
            IInventoryService inventoryService,
            ITransactionService transactionService,
            IStockDeliveryService stockDeliveryService)
        {
            _context = context;
            _inventoryService = inventoryService;
            _transactionService = transactionService;
            _stockDeliveryService = stockDeliveryService;
        }
        public async Task<List<StockReturn>> CreateReturnAsync(Dictionary<int, int> stockReturns)
        {
            List<StockReturn> stockReturnList = new();
            List<int> validateInventoryValues = new();
            List<StockDelivery> deliveryList = new();
            foreach (var item in stockReturns)
            {
                var delivery = await _context.Deliveries
                    .Include(i => i.Inventory)
                    .SingleOrDefaultAsync(d => d.StockDeliveryId == item.Key);
                StockReturn transactionCreated = new()
                {
                    Delivery = delivery,
                    QuantityReturned = item.Value,
                };
                stockReturnList.Add(transactionCreated);
                await _context.AddAsync(transactionCreated);
                validateInventoryValues.Add(delivery!.QuantityToReturn - item.Value);
                deliveryList.Add(delivery);
            }
            if (validateInventoryValues.Min() < 0)
                return null;
            var transaction = await _transactionService.InitiateTransaction(TransactionCategory.Return.ToString());

            foreach (var item in stockReturnList)
            {
                await _inventoryService.UpdateInventory(TransactionCategory.Return.ToString(),
                    item!.Delivery!.Inventory!.InventoryId,
                    item.QuantityReturned);
                await _transactionService.AddStockJournalRecord(TransactionCategory.Return.ToString(),
                    item.StockReturnId);
                await _stockDeliveryService.UpdateDeliveryAsync(item.Delivery.StockDeliveryId, item.QuantityReturned);
                await _context.SaveChangesAsync();
            }
            return stockReturnList;
        }

        public Task<StockReturn> UpdateReturnAsync(StockReturn transaction)
        {
            throw new NotImplementedException();
        }

        private void ResetOverdues(StockDelivery stockDelivery)
        {
            var customer = stockDelivery.Inventory!.Customer;
            if (customer != null)
            {
                customer.Overdue = false;
                _context.SaveChanges();
            }
        }
    }
}
