using BaseApi.Interfaces;
using BaseApi.Models;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
	public class TransactionController : Controller
	{
		private readonly IStockDeliveryService _deliveryService;
		private readonly IStockReturnService _returnService;
		private readonly ITransactionService _transactionService;
        public TransactionController(IStockDeliveryService deliveryService, 
			IStockReturnService returnService,
			ITransactionService transactionService)
		{
			_deliveryService = deliveryService;
			_returnService = returnService;
            _transactionService = transactionService;
        }

		[HttpPost]
        [Route("delivery")]
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

		[HttpPost]
        [Route("return")]
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
		[HttpGet]
		public async Task<ActionResult<List<TransactionModel>>> GetAllTransactions()
        {
            try
            {
                var transactions = await _transactionService.GetAllTransactions();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("delivery")]
        public async Task<ActionResult<List<StockDelivery>>> GetAllDeliveries()
        {
            try
            {
                var transactions = await _transactionService.GetAllDeliveries();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("return")]
        public async Task<ActionResult<List<StockDelivery>>> GetAllReturns()
        {
            try
            {
                var transactions = await _transactionService.GetAllReturns();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("delivery/transactionid")]
        public async Task<ActionResult<StockDelivery>> GetStockDeliveriesByTransactionIdAsync([FromQuery] int id)
        {
            try
            {
                var deliveries = await _transactionService.GetStockDeliveriesByTransactionIdAsync(id);
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("delivery/customer/transactionid")]
        public async Task<ActionResult<StockDelivery>> GetStockDeliveriesByCustomerIdAsync([FromQuery] int id)
        {
            try
            {
                var deliveries = await _deliveryService.GetStockDeliveriesByCustomerIdAsync(id);
                return Ok(deliveries);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("return/transactionid")]
        public async Task<ActionResult<StockReturn>> GetStockReturnsByDeliveryIdAsync([FromQuery] int id)
        {
            try
            {
                var stockReturns = await _transactionService.GetStockReturnsByDeliveryIdAsync(id);
                return Ok(stockReturns);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
