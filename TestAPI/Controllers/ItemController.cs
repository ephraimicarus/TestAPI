using BaseApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpPut]
        public async Task<ActionResult<Item>> UpdateItemAsync([FromBody] Item item)
        {
            try
            {
                var updatedItem = await _itemService.UpdateItemAsync(item);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        public async Task<ActionResult<Item>> CreateItemAsync([FromBody] Item item)
        {
            var newItem = await _itemService.CreateItemAsync(item);
            return Ok(newItem);
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItemsAsync()
        {
            try
            {
                var items = await _itemService.GetAllItemsAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("id")]
        public async Task<ActionResult<List<Item>>> GetItemById([FromQuery] int id)
        {
            try
            {
                var items = await _itemService.GetItemByIdAsync(id);
                return Ok(items);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Item>> DeleteItemAsync(int id)
        {
            try
            {
                var deletedItem = await _itemService.DeleteItemAsync(id);
                return Ok(deletedItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
