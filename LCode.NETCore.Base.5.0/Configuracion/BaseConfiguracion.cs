using Microsoft.Extensions.Configuration;
using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using LCode.NETCore.Base._5._0.Entidades;

namespace LCode.NETCore.Base._5._0.Configuracion
{
    public static class BaseConfiguracion
    {
        public static string ObtenerValor(string ValorObtn)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            //var configurationBuilder = new ConfigurationBuilder();
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            //configurationBuilder.AddJsonFile(path, false);
            //var root = configurationBuilder.Build();
            //var appSetting = root.GetValue("ConexionBDBASE");
            string Valor = configuration[ValorObtn];
            return Valor;
        }
        public static string ObtenerMQ(string ValorObtn)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.mq.json")
            .Build();
            //var configurationBuilder = new ConfigurationBuilder();
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            //configurationBuilder.AddJsonFile(path, false);
            //var root = configurationBuilder.Build();
            //var appSetting = root.GetValue("ConexionBDBASE");
            string Valor = configuration[ValorObtn];
            return Valor;
        }
        public static string ObtenerValorDB(string ValorObtn)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.db.json")
            .Build();
            //var configurationBuilder = new ConfigurationBuilder();
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            //configurationBuilder.AddJsonFile(path, false);
            //var root = configurationBuilder.Build();
            //var appSetting = root.GetValue("ConexionBDBASE");
            string Valor = configuration[ValorObtn];
            return Valor;
        }
        public static string ObtenerValorBase(string ValorObtn)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.base.json")
            .Build();
            //var configurationBuilder = new ConfigurationBuilder();
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            //configurationBuilder.AddJsonFile(path, false);
            //var root = configurationBuilder.Build();
            //var appSetting = root.GetValue("ConexionBDBASE");
            string Valor = configuration[ValorObtn];
            return Valor;
        }
        public static IConfigurationSection ObtenerSeccionBase(string ValorObtn)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.base.json")
            .Build();
            //var configurationBuilder = new ConfigurationBuilder();
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            //configurationBuilder.AddJsonFile(path, false);
            //var root = configurationBuilder.Build();
            //var appSetting = root.GetValue("ConexionBDBASE");
            return configuration.GetSection(ValorObtn);
        }
        public static string ObtenerSeccion(string Seccion)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            //var configurationBuilder = new ConfigurationBuilder();
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            //configurationBuilder.AddJsonFile(path, false);
            //var root = configurationBuilder.Build();
            //var appSetting = root.GetValue("ConexionBDBASE");
            string Valor = JsonConvert.SerializeObject(configuration.GetSection(Seccion));
            return Valor;
        }
        public static ConexionBD ObtenerBD(string NombreConexion)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.db.json")
            .Build();
            //var configurationBuilder = new ConfigurationBuilder();
            //var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            //configurationBuilder.AddJsonFile(path, false);
            //var root = configurationBuilder.Build();
            //var appSetting = root.GetValue("ConexionBDBASE");
            var list = new List<ConexionBD>();
            configuration.GetSection("Conexiones:BasesDatos").Bind(list);
            ConexionBD Conexion = list.FirstOrDefault(x => x.NombreConexion == NombreConexion);
            return Conexion;
        }
    }
}
