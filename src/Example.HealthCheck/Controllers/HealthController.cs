using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Example.HealthCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public HealthController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        [HttpGet("/change/readiness")]
        public IActionResult ChangeReady()
        {
            var isReadiness = _memoryCache.Get<bool>(Constants.READINESS);

            _memoryCache.Set(Constants.READINESS, !isReadiness);

            return Ok(new { isReadiness  = !isReadiness });
        }


        [HttpGet("/change/liveness")]
        public IActionResult ChangeLive()
        {
            var isLiveness = _memoryCache.Get<bool>(Constants.LIVENESS);

            _memoryCache.Set(Constants.LIVENESS, !isLiveness);

            return Ok(new { isLiveness = !isLiveness });
        }
    }
}
