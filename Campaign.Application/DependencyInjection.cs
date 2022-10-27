using Campaign.Application.Commands.AddCampaign;
using Campaign.Application.Commands.SetCampaignInActive;
using Campaign.Application.Helpers;
using Campaign.Application.Helpers.Interfaces;
using Campaign.Application.PipelineBehaviours;
using Campaign.Application.Services;
using Campaign.Application.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Campaign.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(AddCampaignCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(SetCampaignInActiveCommand).GetTypeInfo().Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<ICampaignHelpers, CampaignHelpers>();

            return services;
        }
    }
}