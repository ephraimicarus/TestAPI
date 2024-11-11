using BaseAppPerla.ExceptionHandling;
using BaseAppPerla.Interfaces;
using BaseAppPerla.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace BaseAppPerla.Services
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _httpClient;

        public ItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/api");
        }

        public async Task<ServiceResult<Item>> CreateItemAsync(Item item)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Item", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var createdItem = JsonConvert.DeserializeObject<Item>(content);
                    return new ServiceResult<Item> { Data = createdItem!, ErrorMessage = string.Empty };
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(errorResponse)?.Message;
                    return new ServiceResult<Item> { ErrorMessage = errorMessage! };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<Item>{ ErrorMessage = ex.Message };
            }
        }

        public async Task<Item> DeleteItemAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Item/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var deletedItem = JsonConvert.DeserializeObject<Item>(content);
                    return deletedItem!;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Item>> GetAllItemsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Item");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<List<Item>>(content);
                    return items!;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Item> GetItemByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Item/id?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var item = JsonConvert.DeserializeObject<Item>(content);
                    return item!;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ServiceResult<Item>> UpdateItemAsync(Item item)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/Item", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var updatedItem = JsonConvert.DeserializeObject<Item>(content);
                    return new ServiceResult<Item> { Data = updatedItem!, ErrorMessage = string.Empty };
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    var errorMessage = JsonConvert.DeserializeObject<ErrorResponse>(errorResponse)?.Message;
                    return new ServiceResult<Item> { ErrorMessage = errorMessage! };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<Item> { ErrorMessage = ex.Message };
            }
        }

    }
}
