using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Api.Commands;
using PaymentGateway.Api.Responses;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("Payment")]
    public class PaymentWriteController
    {

        public PaymentWriteController()
        {
        }

        [HttpPost("InitiatePayment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InitiatePaymentResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InitiatePayment([FromBody] InitiatePaymentCommand command)
        {
            throw new NotImplementedException();
        }
    }
}

