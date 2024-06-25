using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
	public class Customer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CustomerId { get; set; }
        [Required]
        [StringLength(12)]
        public string? Oib { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [StringLength(100)]
        public string? Address { get; set; }
        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(20)]
        public string? Phone { get; set; }
        public bool Overdue { get; set; } = false;
    }
}
