using BaseAppPerla.ExceptionHandling;
using BaseAppPerla.Models;

namespace BaseAppPerla.Interfaces
{
    public interface IItemService
    {
        Task<ServiceResult<Item>> CreateItemAsync(Item item);
        Task<ServiceResult<Item>> UpdateItemAsync(Item item);
        Task<Item> DeleteItemAsync(int id);
        Task<Item> GetItemByIdAsync(int id);
        Task<List<Item>> GetAllItemsAsync();
    }
}
