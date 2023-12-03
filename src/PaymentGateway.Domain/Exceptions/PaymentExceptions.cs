
namespace PaymentGateway.Domain.Exceptions
{
    public class PaymentNotFoundException : Exception
    {
        public PaymentNotFoundException(Guid paymentId)
            : base($"Payment not found for paymentId: {paymentId}")
        {
        }
    }

    public class DuplicatePaymentException : Exception
    {
        public DuplicatePaymentException(Guid paymentId)
            : base($"A payment with ID {paymentId} already exists.")
        {
        }
    }
}

