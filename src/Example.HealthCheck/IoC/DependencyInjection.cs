using Example.HealthCheck.Checks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Example.HealthCheck.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIoC(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHealthChecks()
                    .AddCheck<ReadinessCheck>(Constants.READINESS, tags: new List<string> { Constants.READINESS })
                    .AddCheck<LivenessCheck>(Constants.LIVENESS, tags: new List<string> { Constants.LIVENESS });

            return services;
        }


        public static WebApplication UseIoC(this WebApplication app)
        {
            app.MapHealthChecks("/healthz/ready", 
                new HealthCheckOptions() { Predicate = healthCheck => healthCheck.Tags.Contains(Constants.READINESS) });
            app.MapHealthChecks("/healthz/status",
    new HealthCheckOptions() { Predicate = healthCheck => healthCheck.Tags.Contains(Constants.LIVENESS) });
            return app;
        }
    }
}
