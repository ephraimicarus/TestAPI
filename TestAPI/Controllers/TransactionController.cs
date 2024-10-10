﻿using BaseApi.Interfaces;
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

		[HttpPost]
        [Route("api/[controller]/return")]
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
    }
}
