using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LCode.NETCore.Base._5._0.Entidades
{
    public static class DatosServicio
    {
        public static string NombreServicio { get { return Assembly.GetEntryAssembly().GetName().Name; } }
        public static string NombreServicioCompleto { get { return Assembly.GetEntryAssembly().GetName().FullName; } }
        public static string CodigoBase { get { return Assembly.GetEntryAssembly().GetName().CodeBase; } }
        public static string Version { get { return Assembly.GetEntryAssembly().GetName().Version.ToString(); } }
        public static bool EsDocker { get { return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"; } }
    }
}
