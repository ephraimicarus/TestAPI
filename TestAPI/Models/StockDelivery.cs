using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
	public class StockDelivery
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StockDeliveryId { get; set; }
        public Inventory? Inventory { get; set; }
        public int QuantityDelivered { get; set; }
        public int QuantityToReturn { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
		public DateTime DateDue { get; set; }

	}
}
