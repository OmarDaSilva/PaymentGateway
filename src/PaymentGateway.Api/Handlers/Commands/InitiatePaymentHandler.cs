using MediatR;
using PaymentGateway.Api.Commands;
using PaymentGateway.Api.Responses;

namespace PaymentGateway.Api.Handlers.Commands
{
    public class InitiatePaymentHandler : IRequestHandler<InitiatePaymentCommand, ApiResponse<InitiatePaymentResponse>>
    {
        public InitiatePaymentHandler()
        {
        }

        public async Task<ApiResponse<InitiatePaymentResponse>> Handle(InitiatePaymentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

