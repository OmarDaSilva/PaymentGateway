using FluentValidation;
using PaymentGateway.Api.Commands;

namespace PaymentGateway.Api.Validators
{
    public class InitiatePaymentCommandValidator : AbstractValidator<InitiatePaymentCommand>
    {
        public InitiatePaymentCommandValidator()
        {
        }
    }
}

