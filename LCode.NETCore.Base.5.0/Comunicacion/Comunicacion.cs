using LCode.NETCore.Base._5._0.Entidades;
using LCode.NETCore.Base._5._0.Logs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LCode.NETCore.Base._5._0.Comunicacion
{
    public class Comunicacion
    {
        public static async Task<string> PostAsync(string Url, object Data, List<CabeceraRequest> ValoresCabecera=null,bool IgnorarSSL=false)
        {
            try
            {
                if (IgnorarSSL)
                { 
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                    (se, cert, chain, sslerror) =>
                    {
                        return true;
                    };
                }
                var request = (HttpWebRequest)WebRequest.Create(Url);
                foreach (CabeceraRequest cabecera in ValoresCabecera)
                {
                    request.Headers.Add(cabecera.Titulo, cabecera.Valor);
                }
                request.ContentType = "application/json";
                request.Method = "POST";
                string dataString = JsonConvert.SerializeObject(Data, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                using (var requestWriter = new StreamWriter(request.GetRequestStream()))
                {
                    requestWriter.Write(dataString);
                }
                using (WebResponse response = await request.GetResponseAsync())
                {
                    //if (response == null)
                    //    throw new InvalidOperationException("La respuesta es Null.");
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        string Resp = sr.ReadToEnd();
                        return Resp;
                    }
                }
            }
            catch (Exception ex)
            {
                await Evento.ErrorAsync(Entidades.TipoEvento.Error,ex);
                return null;
            }
        }
        internal static async Task<string> PostLocalAsync(string Url, object Data, List<CabeceraRequest> ValoresCabecera = null, bool IgnorarSSL = false)
        {
            try
            {
                if (IgnorarSSL)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                    (se, cert, chain, sslerror) =>
                    {
                        return true;
                    };
                }
                var request = (HttpWebRequest)WebRequest.Create(Url);
                foreach (CabeceraRequest cabecera in ValoresCabecera)
                {
                    request.Headers.Add(cabecera.Titulo, cabecera.Valor);
                }
                request.ContentType = "application/json";
                request.Method = "POST";
                string dataString = JsonConvert.SerializeObject(Data, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                using (var requestWriter = new StreamWriter(request.GetRequestStream()))
                {
                    requestWriter.Write(dataString);
                }
                using (WebResponse response = await request.GetResponseAsync())
                {
                    //if (response == null)
                    //    throw new InvalidOperationException("La respuesta es Null.");
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        string Resp = sr.ReadToEnd();
                        return Resp;
                    }
                }
            }
            catch (Exception ex)
            {
                await EventosLocales.Registrar(ex,"URL:"+Url+"||DATA:"+ JsonConvert.SerializeObject(Data));
                return null;
            }
        }
        internal static string PostLocal(string Url, object Data, List<CabeceraRequest> ValoresCabecera = null, bool IgnorarSSL = false)
        {
            try
            {
                if (IgnorarSSL)
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                    (se, cert, chain, sslerror) =>
                    {
                        return true;
                    };
                }
                var request = (HttpWebRequest)WebRequest.Create(Url);
                foreach (CabeceraRequest cabecera in ValoresCabecera)
                {
                    request.Headers.Add(cabecera.Titulo, cabecera.Valor);
                }
                request.ContentType = "application/json";
                request.Method = "POST";
                string dataString = JsonConvert.SerializeObject(Data, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                using (var requestWriter = new StreamWriter(request.GetRequestStream()))
                {
                    requestWriter.Write(dataString);
                }
                using (WebResponse response = request.GetResponse())
                {
                    //if (response == null)
                    //    throw new InvalidOperationException("La respuesta es Null.");
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        string Resp = sr.ReadToEnd();
                        return Resp;
                    }
                }
            }
            catch (Exception ex)
            {
                EventosLocales.Registrar(ex, "URL:" + Url + "||DATA:" + JsonConvert.SerializeObject(Data));
                return null;
            }
        }
    }
}
