namespace BaseAppPerla.Models
{
    public class TransactionModel
    {
        public int TransactionId;
        public string? Description;
        public DateTime TransactionDate;
        public DateTime DateDue;
        public bool IsActive;
    }
}
