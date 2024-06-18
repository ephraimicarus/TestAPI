using TestAPI.DTOs;
using TestAPI.Models;

namespace BaseApi.Interfaces
{
    public interface IReturnService
    {
        Task<Delivery> CreateReturnAsync(Return transaction);
        Task<Delivery> UpdateReturnAsync(Return transaction);
    }
}
