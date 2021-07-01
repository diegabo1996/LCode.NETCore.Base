using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCode.NETCore.Base._5._0.Entidades
{
    public class ConfiguracionArchivo
    {
        [JsonProperty("UnidadLogs")]
        public string RutaGuardado { get; set; }
    }

    public class ConfiguracionMicroservicio
    {
        [JsonProperty("URL")]
        public string URL { get; set; }
    }

    public class LogsE
    {
        [JsonProperty("GuardarEnArchivo")]
        public bool GuardarEnArchivo { get; set; }

        [JsonProperty("ConfiguracionArchivo")]
        public ConfiguracionArchivo ConfiguracionArchivo { get; set; }

        [JsonProperty("UtilizarMicroServicio")]
        public bool UtilizarMicroServicio { get; set; }

        [JsonProperty("ConfiguracionMicroservicio")]
        public ConfiguracionMicroservicio ConfiguracionMicroservicio { get; set; }
    }

    public class LogsConfiguracion
    {
        [JsonProperty("Logs")]
        public LogsE Logs { get; set; }
    }

}
