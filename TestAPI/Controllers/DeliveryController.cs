using BaseApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestAPI.DTOs;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
	public class DeliveryController : Controller
	{
		private readonly IDeliveryService _deliveryService;
		public DeliveryController(IDeliveryService deliveryService)
		{
			_deliveryService = deliveryService;
		}

		[HttpPost]
		public async Task<ActionResult<Delivery>> CreateDelivery([FromBody] DeliveryDTO delivery)
		{
			try
			{
				var newDelivery = await _deliveryService.CreateDeliveryAsync(delivery);
				return Ok(newDelivery);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
