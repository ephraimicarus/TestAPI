using TestAPI.Models;

namespace BaseApi.DTOs
{
    public class CustomerDto : Customer
    {
        int DaysOverdue { get; set; } 
    }
}
