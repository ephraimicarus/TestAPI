using BaseApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.DTOs;
using TestAPI.Models;

namespace TestAPI.Services
{
	public class InventoryService : IInventoryService
	{
		private readonly ApplicationContext _context;
		public InventoryService(ApplicationContext context)
		{
			_context = context;
		}
		public async Task<List<Inventory>> CreateInventory(int customerId)
		{
			var inventoryList = new List<Inventory>();
			var itemsList = await _context.Items.ToListAsync();
			if (itemsList.Count <= 0)
				return null;

			foreach (var item in itemsList)
			{
				Inventory inventoryToInitiate = new()
				{
					Customer = await _context.Customers.SingleAsync(c => c.CustomerId == customerId),
					Item = await _context.Items.SingleAsync(i => i.ItemId == item.ItemId),
					Quantity = 0
				};
				inventoryList.Add(inventoryToInitiate);
			}
			_context.AddRange(inventoryList);
			await _context.SaveChangesAsync();
			return inventoryList;
		}

		public async Task<Inventory> UpdateInventory(int inventoryId, int quantity)
		{
			var inventories = await _context.Inventories
				.SingleOrDefaultAsync(inv => inv.InventoryId == inventoryId);

			if (inventories != null)
			{
				if ((inventories.Quantity + quantity) < 0)
					return inventories;
				inventories.Quantity += quantity;
				await _context.SaveChangesAsync();
				return inventories;
			}
			else
			{
				return null;
			}
		}

		public async Task<Item> AddToInventory(int itemId)
		{
			var item = await _context.Items.SingleOrDefaultAsync(i => i.ItemId == itemId);
			if (item == null)
				return null;
			var customerList = _context.Customers.ToList();
			foreach (var customer in customerList)
			{
				Inventory inventory = new()
				{
					Customer = customer,
					Item = item,
					Quantity = 0
				};
				await _context.AddAsync(inventory);
			}
			await _context.SaveChangesAsync();
			return item;
		}
	}
}
