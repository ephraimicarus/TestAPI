namespace BaseAppPerla.Models
{
    public class StockReturn
    {
        public int StockReturnId;
        public StockDelivery? Delivery;
        public int QuantityReturned;
        public DateTime TransactionDate;
    }
}
