using EcommerceAPI.Models.DTO;

namespace EcommerceAPI.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> SimulatePaymentAsync(PaymentRequest request);
    }
}
