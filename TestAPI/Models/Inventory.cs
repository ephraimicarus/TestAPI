using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
	[PrimaryKey(nameof(Customer.CustomerId), nameof(Item.ItemId))]
	public class Inventory
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int InventoryId { get; set; }
        public Customer? Customer { get; set; }
        public Item? Item { get; set; }
        public int Quantity { get; set; }
    }
}
