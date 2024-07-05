namespace BaseApp.Models
{
	public class Customer
	{
		public int CustomerId { get; set; }
		public string? Oib { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public bool Overdue { get; set; } = false;
	}
}
