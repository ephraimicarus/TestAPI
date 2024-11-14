using BaseAppPerla.DTOs;
using BaseAppPerla.ExceptionHandling;
using BaseAppPerla.Models;

namespace BaseAppPerla.Interfaces
{
    public interface IInventoryService
    {
        Task<List<InventoryDto>> GetInventoriesAsync(int id);
        Task<Inventory> UpdateInventory(string category, int inventoryId, int quantity);
        Task<List<BaseInventory>> GetBaseInventoryAsync();
        Task<ServiceResult<BaseInventory>> UpdateBaseInventoryAsync(BaseInventory inventory);
    }
}
