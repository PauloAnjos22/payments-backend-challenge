using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payments.Backend.Challenge.Application.DTOs;
using Payments.Backend.Challenge.Application.Interfaces;

namespace Payments.Backend.Challenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IRegisterUser registerUser) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OperationResultDto<RegisterUserResponseDto>>> ExecuteAsync([FromBody] RegisterUserRequestDto request)
        {
            var response = await registerUser.ExecuteAsync(request);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
