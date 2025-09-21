using EcommerceAPI.Models;

namespace EcommerceAPI.Repositories
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);
        Task<Payment?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
