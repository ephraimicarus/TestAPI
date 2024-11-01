using BaseAppPerla.Models;

namespace BaseAppPerla.DTOs
{
    public class StockReturnDto : StockDelivery
    {
        public int QuantityShuttle { get; set; } = 0;
    }
}
