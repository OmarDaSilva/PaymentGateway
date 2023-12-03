using MediatR;
using PaymentGateway.Api.Responses;

namespace PaymentGateway.Api.Queries
{
    public class GetPaymentQuery : IRequest<ApiResponse<GetPaymentResponse>>
    {
        public GetPaymentQuery()
        {
        }
    }
}

