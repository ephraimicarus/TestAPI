using BaseApi.Interfaces;
using BaseApi.Utility;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.DTOs;
using TestAPI.Models;

namespace TestAPI.Services
{
    public class StockDeliveryService : IStockDeliveryService
    {
        private readonly ApplicationContext _context;
        private readonly IInventoryService _inventoryService;
        private readonly ITransactionService _transactionService;
        public StockDeliveryService(ApplicationContext context,
            IInventoryService inventoryService,
            ITransactionService transactionService)
        {
            _context = context;
            _inventoryService = inventoryService;
            _transactionService = transactionService;
        }
        public async Task<List<StockDelivery>> CreateDeliveryAsync(Dictionary<int, int> inventories)
        {
            List<int> validateInventoryValues = new();
            List<StockDelivery> deliveryList = new();
            Dictionary<Inventory, int> inventoriesToUpdate = new();
            foreach (var item in inventories)
            {
                var inventory = await _context.Inventories
                    .Include(i => i.Item)
                    .SingleOrDefaultAsync(i => i.InventoryId == item.Key);
                var baseInventory = await _context.BaseInventory
                    .Include(i => i.Item)
                    .SingleOrDefaultAsync(bi => bi!.Item!.ItemId == inventory!.Item!.ItemId);
                if (baseInventory != null)
                    validateInventoryValues.Add(baseInventory.QuantityStored - item.Value);
                if (inventory != null)
                    inventoriesToUpdate.Add(inventory, item.Value);
            }
            if (validateInventoryValues.Min() < 0)
                throw new InvalidOperationException("One or more inventory items have insufficient quantity.");
            var transaction = await _transactionService.InitiateTransaction(TransactionCategory.Delivery.ToString());
            if (transaction == null)
            {
                throw new InvalidOperationException("Failed to initiate transaction.");
            }
            foreach (var item in inventoriesToUpdate)
            {
                StockDelivery transactionCreated = new()
                {
                    Inventory = item.Key,
                    QuantityDelivered = item.Value,
                    QuantityToReturn = item.Value,
                    TransactionInfo = transaction
                };
                _context.Add(transactionCreated);
                var updatedInventory = await _inventoryService.UpdateInventory(TransactionCategory.Delivery.ToString(),
                    transactionCreated.Inventory.InventoryId,
                    transactionCreated.QuantityDelivered);
                if (updatedInventory == null)
                {
                    throw new InvalidOperationException($"Failed to update inventory with ID {transactionCreated.Inventory.InventoryId}.");
                }
                var stockJournalRecordAdded = await _transactionService.AddStockJournalRecord(TransactionCategory.Delivery.ToString(),
                    transactionCreated.StockDeliveryId);
                if (stockJournalRecordAdded == null)
                {
                    throw new InvalidOperationException($"Failed to add stock journal record for delivery ID {transactionCreated.StockDeliveryId}.");
                }
                deliveryList.Add(transactionCreated);
                await _context.SaveChangesAsync();
            }
            return deliveryList;
        }

        public async Task<List<StockDelivery>> GetALlStockDeliveriesAsync()
        {
            var stockDelivery = await _context.Deliveries
                .Include(sd => sd.Inventory)
                    .ThenInclude(i => i.Item)
                .Include(sd => sd.Inventory!.Customer)
                .Include(r => r.TransactionInfo)
                .ToListAsync();
            return stockDelivery;
        }

        public async Task<List<StockDelivery>> GetStockDeliveriesByTransactionIdAsync(int transactionId)
        {
            var delivery = await _context.Deliveries
                .Include(d => d.Inventory)
                .Include(d => d.TransactionInfo)
                .Where(d => d.TransactionInfo!.TransactionId == transactionId).ToListAsync();
            return delivery;
        }

        public async Task<StockDelivery> UpdateDeliveryAsync(int deliveryId, int quantity)
        {
            var deliveryToUpdate = await _context.Deliveries.SingleOrDefaultAsync(d => d.StockDeliveryId == deliveryId);
            if (deliveryToUpdate != null)
                deliveryToUpdate.QuantityToReturn -= quantity;
            await _context.SaveChangesAsync();
            return deliveryToUpdate!;
        }



        private void SetOverdueCustomerFlag(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                customer.Overdue = true;
            }
            _context.SaveChanges();
        }
    }
}
