using System.Transactions;
using TestAPI.DTOs;
using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IDeliveryService
    {
        Task<Delivery> CreateDeliveryAsync(DeliveryDTO transaction);
        Task<Delivery> UpdateTransactionAsync(DeliveryDTO transaction);
    }
}
