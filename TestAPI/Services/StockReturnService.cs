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
		public async Task<StockReturn> CreateReturnAsync(int deliveryId, int quantity)
		{
			var delivery = await _context.Deliveries.Include(i=>i.Inventory).SingleOrDefaultAsync(d => d.StockDeliveryId == deliveryId);
			try
			{
				StockReturn transactionCreated = new()
				{
					Delivery = delivery,
					QuantityReturned = quantity,
				};
				_context.Add(transactionCreated);
				if ((delivery!.QuantityToReturn-quantity)<0)
					return null;
				await _inventoryService.UpdateInventory(TransactionCategory.Return.ToString(), 
					delivery!.Inventory!.InventoryId, 
					transactionCreated.QuantityReturned);
				await _transactionService.AddStockJournalRecord(TransactionCategory.Return.ToString(), 
					transactionCreated.StockReturnId);
				await _stockDeliveryService.UpdateDeliveryAsync(delivery.StockDeliveryId, transactionCreated.QuantityReturned);
				await _context.SaveChangesAsync();
				return transactionCreated;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public Task<StockReturn> UpdateReturnAsync(StockReturn transaction)
		{
			throw new NotImplementedException();
		}
	}
}
