using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestAPI.DTOs;

namespace TestAPI.Models
{
	public class StockReturn
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StockReturnId { get; set; }
        public StockDelivery? Delivery { get; set; }
        public int QuantityReturned { get; set; }
	}
}
