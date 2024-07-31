using BaseApi.Interfaces;
using BaseApi.Utility;
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
                return inventoryList;

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

        public async Task<Inventory> UpdateInventory(string category, int inventoryId, int quantity)
        {
            if (category == TransactionCategory.Return.ToString())
                quantity *= -1;
            var inventory = await _context.Inventories
                .SingleOrDefaultAsync(inv => inv.InventoryId == inventoryId);

            if (inventory != null)
            {
                if ((inventory.Quantity + quantity) < 0)
                    return inventory;//TODO: Error handling for case
                inventory.Quantity += quantity;
                await _context.SaveChangesAsync();
                return inventory;
            }
            else
            {
                return null;//TODO: All null reference error/exception handling
            }
        }

        public async Task<Item> ExpandInventory(int itemId)
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

        public async Task<List<Inventory>> GetInventoriesAsync(int customerId) => await _context.Inventories
            .Include(i=>i.Customer)
            .Include(i=>i.Item)
            .Where(i => i.Customer!.CustomerId == customerId).ToListAsync();
    }
}
