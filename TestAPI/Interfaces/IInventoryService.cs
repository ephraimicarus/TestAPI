using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IInventoryService
    {
        Task<List<Inventory>> GetInventoriesAsync(int id);
        Task<List<Inventory>> CreateInventory(int customerId);
        Task<Inventory> UpdateInventory(string category, int inventoryId, int quantity);
        Task<Item> ExpandInventory(int itemId);
        Task<BaseInventory> AddItemToBaseInventory(Item item);
        Task<List<BaseInventory>> GetBaseInventoryAsync();
        Task<BaseInventory> UpdateBaseInventoryAsync(BaseInventory inventory);
    }
}
