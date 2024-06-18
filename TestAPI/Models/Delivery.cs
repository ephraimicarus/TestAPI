using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
	public class Delivery
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int DeliveryId { get; set; }
        public Inventory? Inventory { get; set; }
		public int OrderTotal { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
		public DateTime DateDue { get; set; }
	}
}
