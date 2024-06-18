using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
	public class Transaction
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TransactionId { get; set; }
		public Delivery? Delivery { get; set; }
        public bool DueStatus { get; set; }
		public bool SettledStatus { get; set; }
        public DateTime DateSettled { get; set; }

    }
}
