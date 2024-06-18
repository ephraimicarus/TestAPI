using TestAPI.Models;

namespace TestAPI.DTOs
{
	public class InventoryDTO
	{
        public Customer? Customer { get; set; }
        public Item? Item { get; set; }
        public int Quantity { get; set; }
    }
}
