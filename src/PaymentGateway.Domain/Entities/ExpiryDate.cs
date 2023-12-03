using System;
namespace PaymentGateway.Domain.Entities
{
    public class ExpiryDate
    {
        public int Year { get; private set; }
        public int Month { get; private set; }

        private ExpiryDate(int year, int month)
        {
            Year = year;
            Month = month;
        }
    }
}

