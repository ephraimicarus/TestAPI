using BaseAppPerla.Models;

namespace BaseAppPerla.DTOs
{
    public class InventoryDto : Inventory
    {
        public int QuantityShuttle { get; set; } = 0;
    }
}
