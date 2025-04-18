﻿using BaseAppPerla.ExceptionHandling;
using BaseAppPerla.Models;

namespace BaseAppPerla.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResult<Customer>> CreateCustomerAsync(Customer customer);
        Task <ServiceResult<Customer>> UpdateCustomerAsync(Customer customer);
        Task<ServiceResult<Customer>> DeleteCustomerAsync(int customerId);
        Task<Customer> GetCustomerByOibAsync(string oib);
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetAllCustomersAsync();
    }
}
