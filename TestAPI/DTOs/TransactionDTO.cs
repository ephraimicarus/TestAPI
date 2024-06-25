namespace TestAPI.DTOs
{
	public class TransactionDTO
	{
        public int InventoryId { get; set; }
        public int OrderTotal { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
		public DateTime DateDue { get; set; }
	}
}
