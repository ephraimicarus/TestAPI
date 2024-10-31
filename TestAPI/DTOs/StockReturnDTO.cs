using TestAPI.Models;

namespace BaseApi.DTOs
{
	public class StockReturnDTO
	{
        public StockDelivery? Delivery { get; set; }
        public int TotalQuantity { get; set; }
    }
}
