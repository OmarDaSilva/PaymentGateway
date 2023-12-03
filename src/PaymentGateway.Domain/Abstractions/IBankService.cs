using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Domain.Abstractions
{
    public interface IBankService
    {
        public Task<BankPaymentResult> InitiatePaymentAsync(Payment payment);
    }
}

