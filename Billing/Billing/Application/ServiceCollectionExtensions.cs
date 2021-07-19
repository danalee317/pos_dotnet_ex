﻿
using MassTransit;

using Microsoft.Extensions.DependencyInjection;

namespace Billing.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddMediatR(typeof(ServiceCollectionExtensions));

        // Add Brighter.
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(typeof(ServiceCollectionExtensions).Assembly);
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        })
        .AddMassTransitHostedService()
        .AddGenericRequestClient();

        return services;
    }
}