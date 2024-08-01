using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
    public class BaseInventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BaseInventoryId { get; set; }
        public Item? Item { get; set; }
        public int QuantityStored { get; set; }
        public int QuantityRented { get; set; }
    }
}
