using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Example.HealthCheck.Checks
{
    internal class ReadinessCheck : IHealthCheck
    {
        private readonly IMemoryCache _memoryCache;

        public ReadinessCheck(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isReadiness = _memoryCache.GetOrCreate(Constants.READINESS, (entry) => true);

            return isReadiness ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
    }
}