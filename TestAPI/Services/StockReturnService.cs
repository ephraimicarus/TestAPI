﻿using BaseApi.Interfaces;
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
        private readonly ICustomerDueService _utilityService;
        public StockReturnService(ApplicationContext context,
            IInventoryService inventoryService,
            ITransactionService transactionService,
            IStockDeliveryService stockDeliveryService,
            ICustomerDueService utilityService)
        {
            _context = context;
            _inventoryService = inventoryService;
            _transactionService = transactionService;
            _stockDeliveryService = stockDeliveryService;
            _utilityService = utilityService;
        }
        public async Task<List<StockReturn>> CreateReturnAsync(Dictionary<int, int> stockReturns)
        {
            bool isCustomerDue = false;
            List<StockReturn> stockReturnList = new();
            List<int> validateInventoryValues = new();
            foreach (var item in stockReturns)
            {
                var delivery = await _context.Deliveries
                    .Include(i => i.Inventory)
                    .SingleOrDefaultAsync(d => d.StockDeliveryId == item.Key);
                if (delivery == null)
                {
                    throw new KeyNotFoundException($"StockDelivery with ID {item.Key} not found.");
                }
                StockReturn transactionCreated = new()
                {
                    Delivery = delivery,
                    QuantityReturned = item.Value,
                };
                stockReturnList.Add(transactionCreated);
                await _context.AddAsync(transactionCreated);
                validateInventoryValues.Add(delivery!.QuantityToReturn - item.Value);
            }
            if (validateInventoryValues.Min() < 0)
            {
                throw new InvalidOperationException("One or more stock returns have invalid quantities.");
            }
            var transaction = await _transactionService.InitiateTransaction(TransactionCategory.Return.ToString());

            foreach (var item in stockReturnList)
            {
                await _inventoryService.UpdateInventory(TransactionCategory.Return.ToString(),
                    item!.Delivery!.Inventory!.InventoryId,
                    item.QuantityReturned);
                await _transactionService.AddStockJournalRecord(TransactionCategory.Return.ToString(),
                    item.StockReturnId);
                await _stockDeliveryService.UpdateDeliveryAsync(item.Delivery.StockDeliveryId, item.QuantityReturned);
                if (await _utilityService.IsCustomerDue(item.Delivery.Inventory.Customer!))
                    isCustomerDue = true;
            }
            var cus = stockReturnList[0].Delivery!.Inventory!.Customer;
            if (isCustomerDue)
                await _utilityService.SetACustomerAsDue(cus!);
            await _context.SaveChangesAsync();
            
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
