using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
	public class Item
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ItemId { get; set; }

		[Required]
		[StringLength(50)]
		public string? Description { get; set; }
	}
}
