using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BaseApi.Models
{
    public class TransactionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public DateTime DateDue { get; set; }
    }
}
