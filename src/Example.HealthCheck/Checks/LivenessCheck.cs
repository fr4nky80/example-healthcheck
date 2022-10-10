using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Example.HealthCheck.Checks
{
    internal class LivenessCheck : IHealthCheck
    {
        private readonly IMemoryCache _memoryCache;

        public LivenessCheck(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var isLiveness = _memoryCache.GetOrCreate(Constants.LIVENESS, (entry) => true);

            return isLiveness ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
        }
    }
}