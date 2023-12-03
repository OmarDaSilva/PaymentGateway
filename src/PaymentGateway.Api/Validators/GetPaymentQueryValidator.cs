using FluentValidation;
using PaymentGateway.Api.Commands;

namespace PaymentGateway.Api.Validators
{
    public class GetPaymentQueryValidator : AbstractValidator<InitiatePaymentCommand>
    {
        public GetPaymentQueryValidator()
        {
        }
    }
}

