using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Services;

namespace TestAPI.Controllers
{
	[Route("api/[controller]")]
	public class CustomerController : Controller
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpPost]
		public async Task<ActionResult<Customer>> CreateCustomerAsync([FromBody] Customer customer)
		{
			try
			{
				var newCustomer = await _customerService.CreateCustomerAsync(customer);
				return Ok(newCustomer);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		public async Task<ActionResult<List<Customer>>> GetAllCustomersAsync()
		{
			try
			{
				var customers = await _customerService.GetAllCustomersAsync();
				return Ok(customers);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return BadRequest(ex.Message);
			}
		}

		[HttpGet]
		[Route("id")]
		public async Task<ActionResult<Customer>> GetCustomerByOib(string oib)
		{
			try
			{
				var customer = await _customerService.GetCustomerByOibAsync(oib);
				return Ok(customer);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<ActionResult<Customer>> UpdateCustomerAsync(Customer customer)
		{
			try
			{
				var updatedCustomer = await _customerService.UpdateCustomerAsync(customer);
				return Ok(updatedCustomer);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete]
		public async Task<ActionResult<Customer>> DeleteCustomerAsync(Customer customer)
		{
			try
			{
				var deletedCustomer = await _customerService.DeleteCustomerAsync(customer);
				return Ok(deletedCustomer);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return BadRequest(ex.Message);
			}
		}
	}
}
