using BaseApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.DTOs;
using TestAPI.Models;

namespace TestAPI.Services
{
    public class DeliveryService : IDeliveryService
	{
		private readonly ApplicationContext _context;
		public DeliveryService(ApplicationContext context)
		{
			_context = context;
		}
		public async Task<Delivery> CreateDeliveryAsync(DeliveryDTO delivery)
		{
			try
			{
				Delivery transactionCreated = new()
				{
					//Inventory = delivery.Inventory,
					//OrderTotal = delivery.OrderTotal,
					DateDue = delivery.TransactionDate.AddDays(7),
				};
				_context.Add(transactionCreated);
				await _context.SaveChangesAsync();
				return transactionCreated;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public Task<Delivery> UpdateTransactionAsync(DeliveryDTO transaction)
		{
			throw new NotImplementedException();
		}
	}
}
