using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LCode.NETCore.Base._5._0.Entidades
{
    public class DatosServicio
    {
        public string NombreServicio { get { return Assembly.GetEntryAssembly().GetName().Name; } }
        public string NombreServicioCompleto { get { return Assembly.GetEntryAssembly().GetName().FullName; } }
        public string CodigoBase { get { return Assembly.GetEntryAssembly().GetName().CodeBase; } }
        public string Version { get { return Assembly.GetEntryAssembly().GetName().Version.ToString(); } }
        public bool EsDocker { get { return Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true"; } }
    }
}
