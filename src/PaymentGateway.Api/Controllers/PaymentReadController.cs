using System;
using Microsoft.AspNetCore.Mvc;

using PaymentGateway.Api.Queries;
using PaymentGateway.Api.Responses;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("Payments")]
    public class PaymentReadController
    {

        public PaymentReadController()
        {
        }

        [HttpGet("GetPayment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InitiatePaymentResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPayment([FromBody] GetPaymentQuery query)
        {
            throw new NotImplementedException();
        }
    }
}

