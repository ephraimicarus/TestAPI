using BaseAppPerla.Interfaces;
using BaseAppPerla.Models;
using Newtonsoft.Json;
using System.Text;

namespace BaseAppPerla.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;

        public TransactionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44398/api");
        }
        public async Task<List<StockDelivery>> CreateDeliveryAsync(Dictionary<int, int> inventories)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(inventories), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Transaction/delivery", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var createdItem = JsonConvert.DeserializeObject<List<StockDelivery>>(content);
                    return createdItem!;
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

        public async Task<List<StockReturn>> CreateReturnAsync(Dictionary<int, int> stockReturns)
        {
            try
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(stockReturns), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/Transaction/return", jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var createdItem = JsonConvert.DeserializeObject<List<StockReturn>>(content);
                    return createdItem!;
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

        public async Task<List<StockDelivery>> GetAllDeliveries()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Transaction/delivery");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var transactions = JsonConvert.DeserializeObject<List<StockDelivery>>(content);
                    return transactions!;
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

        public async Task<List<StockReturn>> GetAllReturns()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Transaction/return");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var transactions = JsonConvert.DeserializeObject<List<StockReturn>>(content);
                    return transactions!;
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

        public async Task<List<TransactionModel>> GetAllTransactions()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Transaction");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var transactions = JsonConvert.DeserializeObject<List<TransactionModel>>(content);
                    return transactions!;
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

        public async Task<List<StockDelivery>> GetStockDeliveriesByTransactionIdAsync(int transactionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Transaction/delivery/transactionid?id={transactionId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var deliveries = JsonConvert.DeserializeObject<List<StockDelivery>>(content);
                    return deliveries!;
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
        public async Task<List<StockReturn>> GetStockReturnsByDeliveryIdAsync(int deliveryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Transaction/return/transactionid?id={deliveryId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var stockReturns = JsonConvert.DeserializeObject<List<StockReturn>>(content);
                    return stockReturns!;
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
