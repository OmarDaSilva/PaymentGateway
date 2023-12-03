using MediatR;
using PaymentGateway.Api.Responses;

namespace PaymentGateway.Api.Commands
{
    public class InitiatePaymentCommand : IRequest<ApiResponse<InitiatePaymentResponse>>
    {

    }
}

