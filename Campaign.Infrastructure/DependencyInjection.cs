using Campaign.Infrastructure.Contexts;
using Campaign.Infrastructure.Contexts.Interfaces;
using Campaign.Infrastructure.Repositories;
using Campaign.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Campaign.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICampaignContext, CampaignContext>();

            services.AddTransient<ICampaignRepository, CampaignRepository>();

            return services;
        }
    }
}