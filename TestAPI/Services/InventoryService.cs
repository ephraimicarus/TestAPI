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
            var inventory = await _context.Inventories
                .Include(i => i.Item)
                .SingleOrDefaultAsync(inv => inv.InventoryId == inventoryId);

            if (inventory == null)
            {
                throw new KeyNotFoundException($"Inventory with ID {inventoryId} not found.");
            }

            if(category == TransactionCategory.Return.ToString() && inventory.Quantity - quantity < 0)
            {
                throw new InvalidOperationException("Invalid quantity.");
            }

            var baseInventory = await _context.BaseInventory
                .Include(bi => bi.Item)
                .SingleOrDefaultAsync(bi => bi.Item!.ItemId == inventory!.Item!.ItemId);

            if (baseInventory == null)
            {
                throw new KeyNotFoundException($"Base inventory for item with ID {inventory.Item!.ItemId} not found.");
            }

            if (category == TransactionCategory.Return.ToString() && baseInventory.QuantityStored - quantity < 0)
            {
                throw new InvalidOperationException($"Invalid quantity for base inventory item with ID {baseInventory.Item!.ItemId}.");
            }

            if (category == TransactionCategory.Delivery.ToString())
            {
                baseInventory.QuantityStored -= quantity;
                baseInventory.QuantityRented += quantity;
                inventory.Quantity += quantity;
            }
            else
            {
                baseInventory.QuantityStored += quantity;
                baseInventory.QuantityRented -= quantity;
                inventory.Quantity -= quantity;
            }
            await _context.SaveChangesAsync();
            return inventory;

        }
        public async Task<BaseInventory> AddItemToBaseInventory(Item item)
        {
            BaseInventory baseInventory = new()
            {
                Item = item,
                QuantityRented = 0,
                QuantityStored = 0,
            };
            _context.Add(baseInventory);
            await _context.SaveChangesAsync();
            return baseInventory;
        }
        public async Task<Item> ExpandInventory(int itemId)
        {
            var item = await _context.Items.SingleOrDefaultAsync(i => i.ItemId == itemId);
            if (item == null)
                return null;
            var customerList = await _context.Customers.ToListAsync();
            if (customerList.Count == 0)
                return null;
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
            .Include(i => i.Customer)
            .Include(i => i.Item)
            .Where(i => i.Customer!.CustomerId == customerId).ToListAsync();
    }
}
