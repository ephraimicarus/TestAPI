using BaseApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Services
{
    public class ItemService : IItemService
	{
		private readonly ApplicationContext _context;
		private readonly IInventoryService _inventoryService;
		public ItemService(ApplicationContext context, IInventoryService inventoryService)
		{
			_context = context;
			_inventoryService = inventoryService;
		}

		public async Task<Item> CreateItemAsync(Item item)
		{
			try
			{
				Item itemCreated = new()
				{
					Description = item.Description
				};
				_context.Add(itemCreated);
				await _context.SaveChangesAsync();
				await _inventoryService.ExpandInventory(itemCreated.ItemId);
				return itemCreated;
			}
			catch
			{
				throw;
			}
		}

		public async Task<Item> DeleteItemAsync(int id)
		{
			var itemFound = _context.Items
			.Single(i => i.ItemId == id);
			try
			{
				_context.Remove(itemFound);
				await _context.SaveChangesAsync();
				return itemFound;
			}
			catch
			{
				throw;
			}
		}

		public async Task<List<Item>> GetAllItemsAsync() => await _context.Items.ToListAsync();

		public Task<Item> GetItemByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<Item> UpdateItemAsync(Item item)
		{
			var itemToUpdate = _context.Items.Single(i => i.ItemId == item.ItemId);
			try
			{
				if (itemToUpdate != null)
				{
					itemToUpdate.Description = item.Description;

					_context.Items.Update(itemToUpdate);
					await _context.SaveChangesAsync();

					return itemToUpdate;
				}
				else
				{
					return null;
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
