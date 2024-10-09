namespace BaseAppPerla.Models
{
    public class StockDelivery
    {
        public int StockDeliveryId;
        public Inventory? Inventory;
        public int QuantityDelivered;
        public int QuantityToReturn;
        public TransactionModel? TransactionInfo;
    }
}
