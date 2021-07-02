using LCode.NETCore.Base._5._0.Configuracion;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCode.RegistroEventos.ServicioProcesador.ConsumidoresMQ;

namespace LCode.RegistroEventos.ServicioProcesador
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    string ServidorMQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Servidor");
                    string MQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Cola");
                    string UsuarioMQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Usuario");
                    string ContraseniaMQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Contrasenia");
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<RegistroEventosConsumidor>();
                        x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                        {
                            cfg.Host(new Uri("rabbitmq://" + ServidorMQ), h =>
                            {
                                h.Username(UsuarioMQ);
                                h.Password(ContraseniaMQ);
                            });
                            cfg.ReceiveEndpoint(MQ, ep =>
                            {
                                ep.PrefetchCount = 16;
                                ep.UseMessageRetry(r => r.Interval(2, 100));
                                ep.ConfigureConsumer<RegistroEventosConsumidor>(provider);
                            });
                        }));
                    });
                    services.AddMassTransitHostedService();
                });
    }
}
