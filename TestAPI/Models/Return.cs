using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestAPI.DTOs;

namespace TestAPI.Models
{
	public class Return
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ReturnId { get; set; }
        public Delivery? Delivery { get; set; }
        public int Quantity { get; set; }
		public DateTime ReturnDate { get; set; }
	}
}
