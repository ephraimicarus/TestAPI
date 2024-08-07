﻿using BaseApp.Models;

namespace BaseApp.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<Customer> DeleteCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByOibAsync(string oib);
        Task<List<Customer>> GetAllCustomersAsync();
    }
}
