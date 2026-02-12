using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProbabilityCalculator.Api.Models;
using ProbabilityCalculator.Api.Services.Interface;

namespace ProbabilityCalculator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProbabilityController : ControllerBase
    {
        private readonly IProbabilityService _service;

        public ProbabilityController(IProbabilityService service)
        {
            _service = service;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] ProbabilityRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid request payload.");

            var result = _service.Calculate(request.Type!, request.A!, request.B!);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(new ProbabilityResponse { Result = result.Result });
        }
    }
}
