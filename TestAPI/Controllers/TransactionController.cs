using BaseApi.DTOs;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestAPI.DTOs;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
	public class TransactionController : Controller
	{
		private readonly IStockDeliveryService _deliveryService;
		private readonly IStockReturnService _returnService;
		public TransactionController(IStockDeliveryService deliveryService, IStockReturnService returnService)
		{
			_deliveryService = deliveryService;
			_returnService = returnService;
		}

		[Route("api/[controller]/delivery")]
		[HttpPost]
		public async Task<ActionResult<StockDelivery>> CreateDelivery([FromBody] TransactionDTO delivery)
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

		[Route("api/[controller]/return")]
		[HttpPost]
		public async Task<ActionResult<StockReturn>> CreateReturn([FromBody] StockReturnDTO stockReturn)
		{
			try
			{
				var newReturn = await _returnService.CreateReturnAsync(stockReturn.DeliveryId, stockReturn.Quantity);
				return Ok(newReturn);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
