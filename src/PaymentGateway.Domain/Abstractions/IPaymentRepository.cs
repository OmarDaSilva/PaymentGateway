using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Domain.Abstractions
{
    public interface IPaymentRepository
    {
        public Task<Payment> GetPaymentAsync(Guid id);
        public Task CreatePaymentAsync(Payment payment);
    }
}

