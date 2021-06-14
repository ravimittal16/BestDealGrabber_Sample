using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AssignmentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShippingServiceController : ControllerBase
    {
        private readonly ILogger<ShippingServiceController> _logger;

        public ShippingServiceController(ILogger<ShippingServiceController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> FetchShippingCost1([FromBody] object data)
        {
            return Ok(await Task.FromResult(new { total = new Random().Next(100, 20000) }));
        }

        [HttpPost]
        public async Task<IActionResult> FetchShippingCost2([FromBody] object data)
        {
            return Ok(await Task.FromResult(new { amount = new Random().Next(100, 20000) }));
        }
    }
}
