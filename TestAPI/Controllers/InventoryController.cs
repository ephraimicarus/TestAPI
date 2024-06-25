using BaseApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestAPI.DTOs;
using TestAPI.Models;

namespace TestAPI.Controllers
{
	[Route("api/[controller]")]
	public class InventoryController : Controller
	{
		private readonly IInventoryService _inventoryService;
		public InventoryController(IInventoryService inventoryService)
		{
			_inventoryService = inventoryService;
		}

		[HttpPost]
		public async Task<ActionResult<Inventory>> CreateInventory([FromBody] int customerId)
		{
			var newInventory = await _inventoryService.CreateInventory(customerId);
			return Ok(newInventory);
		}

		[HttpPut]
		public async Task<ActionResult<Inventory>> UpdateInventory(string category, int inventoryId, int quantity)
		{
			var newInventory = await _inventoryService.UpdateInventory(category, inventoryId, quantity);
			return Ok(newInventory);
		}
	}
}
