using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UdemyNewMicroService.Bus
{
    public static class MasstransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services, IConfiguration configuration)
        {

            var busOption = (configuration.GetSection(nameof(BusOption)).Get<BusOption>())!;

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOption.Address}"), host =>
                    {
                        host.Username(busOption.UserName);
                        host.Password(busOption.Password);
                    });
                });
            });


            return services;
        }
    }
}
