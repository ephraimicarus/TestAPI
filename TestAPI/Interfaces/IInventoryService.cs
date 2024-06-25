using TestAPI.DTOs;
using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IInventoryService
    {
        Task<List<Inventory>> CreateInventory(int customerId);
        Task<Inventory> UpdateInventory(string category, int inventoryId, int quantity);
        Task<Item> ExpandInventory(int itemId);

	}
}
