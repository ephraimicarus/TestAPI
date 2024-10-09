namespace BaseAppPerla.Models
{
    public class TransactionModel
    {
        public int TransactionId;
        public string? Description;
        public DateTime TransactionDate = DateTime.Now;
        public DateTime DateDue;
    }
}
