using System;
namespace PaymentGateway.Domain.Entities
{
    public class CVV
    {
        public string Code { get; private set; }

        private CVV(string code)
        {
            Code = code;
        }
    }
}

