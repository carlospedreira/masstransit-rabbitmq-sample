using System.Reflection;
using MassTransit;
using GettingStarted;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    // By default, sagas are in-memory, but should be changed to a durable
    // saga repository.
    x.SetInMemorySagaRepositoryProvider();

    var entryAssembly = Assembly.GetEntryAssembly();

    x.AddConsumers(entryAssembly);
    x.AddSagaStateMachines(entryAssembly);
    x.AddSagas(entryAssembly);
    x.AddActivities(entryAssembly);

    // x.UsingInMemory((context, cfg) =>
    // {
    //     cfg.ConfigureEndpoints(context);
    // });

    x.UsingRabbitMq((context, config) =>
    {
        config.Host("rabbitmq", "/", rabbitMqHostConfigurator =>
        {
            rabbitMqHostConfigurator.Username("guest");
            rabbitMqHostConfigurator.Password("guest");
        });

        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

await app.RunAsync();
