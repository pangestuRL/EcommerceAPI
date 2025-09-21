using EcommerceAPI.Models;
using EcommerceAPI.Models.DTO;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepo;
        private readonly IOrderRepository _orderRepo;

        public PaymentService(IPaymentRepository paymentRepo, IOrderRepository orderRepo)
        {
            _paymentRepo = paymentRepo;
            _orderRepo = orderRepo;
        }

        public async Task<PaymentResponse> SimulatePaymentAsync(PaymentRequest request)
        {
            var order = await _orderRepo.GetByIdAsync(request.OrderID);
            if (order == null)
            {
                throw new Exception("Order tidak ditemukan");
            }

            var payment = new Payment
            {
                OrderID = request.OrderID,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                Status = request.IsSuccess ? "Completed" : "Failed",
                PaymentDate = DateTime.Now
            };

            await _paymentRepo.AddAsync(payment);

            // Update status order jika sukses
            if (request.IsSuccess)
            {
                order.Status = "paid";
                await _orderRepo.UpdateAsync(order);
            }

            await _paymentRepo.SaveChangesAsync();

            return new PaymentResponse
            {
                PaymentID = payment.PaymentID,
                OrderID = payment.OrderID,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}
