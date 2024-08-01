using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BaseApi.Models;

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
		public TransactionModel? TransactionInfo { get; set; }

	}
}
