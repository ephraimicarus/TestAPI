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
		public async Task<ActionResult<StockDelivery>> CreateDelivery([FromBody] Dictionary<int, int> inventories)
		{
			try
			{
				var newDelivery = await _deliveryService.CreateDeliveryAsync(inventories);
				return Ok(newDelivery);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Route("api/[controller]/return")]
		[HttpPost]
		public async Task<ActionResult<StockReturn>> CreateReturn([FromBody] Dictionary<int, int> stockReturns)
		{
			try
			{
				var newReturn = await _returnService.CreateReturnAsync(stockReturns);
		
				return Ok(newReturn);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//[Route("api/[controller]/duecustomers")]
		//[HttpGet]
		//public async Task<ActionResult<List<Customer>>> GetCustomersInViolation()
		//{
  //          try
  //          {
		//		var overdueStockDeliveryReturns = await _deliveryService.();
  //              return Ok(overdueStockDeliveryReturns);
  //          }
  //          catch (Exception ex)
  //          {
  //              return BadRequest(ex.Message);
  //          }
  //      }
	}
}
