using BaseAppPerla.DTOs;
using BaseAppPerla.Interfaces;
using BaseAppPerla.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace BaseAppPerla.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _httpClient;

        public InventoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/api");
        }

        public async Task<List<BaseInventory>> GetBaseInventoryAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Inventory/baseinventory");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var baseInventories = JsonConvert.DeserializeObject<List<BaseInventory>>(content);
                    return baseInventories!;
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

        public async Task<List<InventoryDto>> GetInventoriesAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Inventory?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var inventories = JsonConvert.DeserializeObject<List<InventoryDto>>(content);
                    return inventories!;
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

        public async Task<BaseInventory> UpdateBaseInventoryAsync(BaseInventory inventory)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(inventory), Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/Inventory/baseinventory", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var baseInventory = JsonConvert.DeserializeObject<BaseInventory>(content);
                    return baseInventory!;
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

        public async Task<Inventory> UpdateInventory(string category, int inventoryId, int quantity)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}/Inventory/{inventoryId}", new { category, quantity });
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var inventory = JsonConvert.DeserializeObject<Inventory>(content);
                    return inventory!;
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
    }
}
