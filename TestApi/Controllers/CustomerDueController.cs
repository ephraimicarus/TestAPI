using BaseApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;

namespace BaseApi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerDueController : Controller
    {
        private readonly ICustomerDueService _customerDueService;
        public CustomerDueController(ICustomerDueService customerDueService)
        {
            _customerDueService = customerDueService;
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<List<Customer>>> GetOverdueCustomers()
        {
            try
            {
                var overdueCustomers = await _customerDueService.GetOverdueCustomers();
                return Ok(overdueCustomers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("items")]
        public async Task<ActionResult<List<StockDelivery>>> GetCustomerDueItems([FromQuery] string oib)
        {
            try
            {
                var customerDueItems = await _customerDueService.GetCustomerDueItems(oib);
                return Ok(customerDueItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
