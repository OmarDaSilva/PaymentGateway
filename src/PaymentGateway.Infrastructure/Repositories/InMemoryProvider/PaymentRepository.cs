using PaymentGateway.Domain.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Exceptions;

namespace PaymentGateway.Infrastructure.Repositories.InMemoryProvider
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IMemoryCache _memory;

        public PaymentRepository(IMemoryCache memory)
        {
            _memory = memory;
        }

        public Task CreatePaymentAsync(Payment payment)
        {
            if (_memory.TryGetValue<Payment>(payment.PaymentId, out var existingPayment))
            {
                throw new DuplicatePaymentException(payment.PaymentId);
            }

            _memory.Set(payment.PaymentId, payment);

            return Task.CompletedTask;
        }

        public Task<Payment> GetPaymentAsync(Guid paymentId)
        {
            var payment = _memory.Get<Payment?>(paymentId);

            if (payment == null)
            {
                throw new PaymentNotFoundException(paymentId);
            }

            return Task.FromResult(payment);
        }
    }
}

