using BaseApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<ActionResult<List<BaseInventory>>> GetAllInventories([FromQuery] int id)
        {
            var inventories = await _inventoryService.GetInventoriesAsync(id);
            return Ok(inventories);
        }

        [HttpGet]
        [Route("baseinventory")]
        public async Task<ActionResult<List<Inventory>>> GetBaseInventory()
        {
            var baseInventory = await _inventoryService.GetBaseInventoryAsync();
            return Ok(baseInventory);
        }

        [HttpPut]
        public async Task<ActionResult<Inventory>> UpdateInventory(string category, int inventoryId, int quantity)
        {
            var newInventory = await _inventoryService.UpdateInventory(category, inventoryId, quantity);
            return Ok(newInventory);
        }

        [HttpPut]
        [Route("baseinventory")]
        public async Task<ActionResult<Inventory>> UpdateBaseInventory(int itemId, int quantity)
        {
            var newBaseInventory = await _inventoryService.UpdateBaseInventoryAsync(itemId, quantity);
            return Ok(newBaseInventory);
        }
    }
}
