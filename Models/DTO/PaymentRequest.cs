namespace EcommerceAPI.Models.DTO
{
    public class PaymentRequest
    {
        public int OrderID { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "Cash";
        public bool IsSuccess { get; set; } 
    }
}
