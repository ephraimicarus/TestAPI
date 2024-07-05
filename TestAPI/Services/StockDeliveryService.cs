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
		public async Task<StockDelivery> CreateDeliveryAsync(TransactionDTO delivery)
		{
			var inventory = await _context.Inventories
				.Include(c => c.Customer)
				.Include(i => i.Item)
				.SingleOrDefaultAsync(i => i.InventoryId == delivery.InventoryId);
			try
			{
				StockDelivery transactionCreated = new()
				{
					Inventory = inventory,
					QuantityDelivered = delivery.OrderTotal,
					QuantityToReturn = delivery.OrderTotal,
					DateDue = delivery.TransactionDate.AddDays(7),
				};
				_context.Add(transactionCreated);
				var updatedInventory = await _inventoryService.UpdateInventory(TransactionCategory.Delivery.ToString(),
					delivery.InventoryId,
					transactionCreated.QuantityDelivered);
				if (updatedInventory == null)
					return null;
				var stockJournalRecordAdded = await _transactionService.AddStockJournalRecord(TransactionCategory.Delivery.ToString(),
					transactionCreated.StockDeliveryId);
				if (stockJournalRecordAdded == null)
					return null;
				await _context.SaveChangesAsync();
				return transactionCreated;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<StockDelivery> UpdateDeliveryAsync(int deliveryId, int quantity)
		{
			var deliveryToUpdate = await _context.Deliveries.SingleOrDefaultAsync(d => d.StockDeliveryId == deliveryId);
			if (deliveryToUpdate != null)
				deliveryToUpdate.QuantityToReturn -= quantity;
			await _context.SaveChangesAsync();
			return deliveryToUpdate!;
		}
	}
}
