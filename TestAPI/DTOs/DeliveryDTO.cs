namespace TestAPI.DTOs
{
	public class DeliveryDTO : InventoryDTO
	{
        public int MyProperty { get; set; }
		public DateTime TransactionDate { get; set; } = DateTime.Now;
		public DateTime DateDue { get; set; }
	}
}
