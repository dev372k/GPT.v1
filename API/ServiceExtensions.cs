using Infrastructure.Services;

namespace API
{
    public static class ServiceExtensions
    {
        public static void ServicesRegistry(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IGPTService, GPTService>();
            services.AddSingleton<BatchService>();
            services.AddSingleton<ContentService>();
            services.AddSingleton<MotivationService>();
        }
    }
}
