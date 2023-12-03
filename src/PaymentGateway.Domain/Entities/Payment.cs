

namespace PaymentGateway.Domain.Entities
{
    public class Payment
    {
        public Guid PaymentId { get; private set; }
        public Card Card { get; private set; }
        public long Amount { get; private set; }
        public Currency Currency { get; private set; }
        public string Description { get; private set; }
        public DateTime PaymentDate { get; private set; }
        
        public Payment()
        {
        }
    }
}

