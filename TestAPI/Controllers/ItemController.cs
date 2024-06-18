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
		[HttpPost]
		public async Task<ActionResult<Item>> CreateItemAsync([FromBody] Item item)
		{
			var newItem= await _itemService.CreateItemAsync(item);
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
