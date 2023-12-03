using System;
namespace PaymentGateway.Domain.Entities
{
    public class Currency
    {
        public string currencyISOCode { get; private set; }

        private Currency(string isoCode)
        {
            currencyISOCode = isoCode;
        }
    }
}

