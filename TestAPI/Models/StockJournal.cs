using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
	public class StockJournal
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StockJournalId { get; set; }
        public string? Category { get; set; }//Delivery, Return
		public int TransactionTypeId { get; set; }
	}
}
