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

        [HttpGet]
        public async Task<ActionResult<List<StockDelivery>>> GetCustomerDueItems(int customerId)
        {
            try
            {
                var customerDueItems = await _customerDueService.GetCustomerDueItems(customerId);
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
