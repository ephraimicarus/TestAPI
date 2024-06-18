using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IItemService
    {
        Task<Item> CreateItemAsync(Item item);
        Task<Item> UpdateItemAsync(Item item);
        Task<Item> DeleteItemAsync(int id);
        Task<Item> GetItemByIdAsync(int id);
        Task<List<Item>> GetAllItemsAsync();
    }
}
