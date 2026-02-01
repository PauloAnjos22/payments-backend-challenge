using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payments.Backend.Challenge.Application.DTOs;
using Payments.Backend.Challenge.Application.Interfaces;

namespace Payments.Backend.Challenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentCoordinator paymentCoordinator) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> PaymentAsync([FromBody] PaymentRequestDto request)
        {
            var response = await paymentCoordinator.ExecuteAsync(request);
            if (!response.Success)
                return BadRequest(response.Error);

            return Ok();
        }
    }
}
