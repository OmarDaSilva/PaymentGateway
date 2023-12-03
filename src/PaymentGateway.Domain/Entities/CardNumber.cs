using System;
namespace PaymentGateway.Domain.Entities
{
    public class CardNumber
    {
        public string Number { get; private set; }

        private CardNumber(string number)
        {
            Number = number;
        }
    }
}

