using LCode.NETCore.Base._5._0.Configuracion;
using LCode.NETCore.Base._5._0.Excepciones;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace LCode.RegistroEventos.WebHook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string ServidorMQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Servidor");
            string UsuarioMQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Usuario");
            string ContraseniaMQ = BaseConfiguracion.ObtenerValor("ConfigMQ:RabbitMQ:Contrasenia");
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri("rabbitmq://" + ServidorMQ), h =>
                    {
                        h.Username(UsuarioMQ);
                        h.Password(ContraseniaMQ);
                    });
                }));
            });
            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LCode.RegistroEventos.WebHook", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LCode.RegistroEventos.WebHook v1"));
            app.UsarApiCapturadorErrores();
            app.Use(async (context, next) =>
            {
                //string LlaveServicio = context.Request.Headers.FirstOrDefault(x => x.Key == "LlaveServicio").Value.FirstOrDefault();
                //if (string.IsNullOrEmpty(Seguridad.T3DES.DecryptKeyTripleDes(LlaveServicio)))
                //{
                //    context.Response.StatusCode = 401;
                //    return;
                //}
                await next();
            });
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
