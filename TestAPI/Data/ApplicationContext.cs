using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Data
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
		}
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Inventory> Inventories { get; set; }
		public DbSet<StockDelivery> Deliveries { get; set; }
		public DbSet<StockReturn> Returns { get; set; }
		public DbSet<StockJournal> StockJournals { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>(entity =>
			{
				entity.HasIndex(c => c.Oib).IsUnique();
			});
		}
	}
}
