using MediatR;
using PaymentGateway.Api.Commands;
using PaymentGateway.Api.Responses;

namespace PaymentGateway.Api.Handlers.Queries
{
    public class GetPaymentHandler : IRequestHandler<InitiatePaymentCommand, ApiResponse<InitiatePaymentResponse>>
    {
        public GetPaymentHandler()
        {
        }

        public async Task<ApiResponse<InitiatePaymentResponse>> Handle(InitiatePaymentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

