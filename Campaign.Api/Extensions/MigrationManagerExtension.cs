using Campaign.Infrastructure.Contexts;
using Campaign.Infrastructure.Contexts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Campaign.Api.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var campaignContext = scope.ServiceProvider.GetRequiredService<ICampaignContext>();

                    CampaignContextSeed.SeedAsync(campaignContext.Campaigns).Wait();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Database migration failed! Message: {ex.Message}");
                }
            }

            return host;
        }
    }
}