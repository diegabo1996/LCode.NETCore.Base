using LCode.NETCore.Base._5._0.Configuracion;
using LCode.NETCore.Base._5._0.Entidades;
using LCode.NETCore.Base._5._0.Comunicacion;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LCode.NETCore.Base._5._0.Logs
{
    public class Evento
    {
        public static async Task Registrar(Entidades.TipoEvento TipoEvento, object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            Exception Excepcion = null;
            EventoEntidad eventoEntidad = new EventoEntidad();
            StackTrace st;
            #region Cabecera
            eventoEntidad.EsDocker = DatosServicio.EsDocker;
            eventoEntidad.NombreComponente = DatosServicio.NombreServicio;
            eventoEntidad.NombreComponenteCompleto = DatosServicio.NombreServicioCompleto;
            eventoEntidad.Version = DatosServicio.Version;
            eventoEntidad.IdActividad = Activity.Current.RootId+"||"+ Activity.Current.Id;
            eventoEntidad.FechaHoraEvento = DateTime.Now;
            #endregion
            if (Excepcion_Mensaje.GetType().Name.ToUpper().Contains("EXCEPTION"))
            {
                Excepcion = ((Exception)Excepcion_Mensaje);
                st = new StackTrace(((Exception)Excepcion), true);
                Excepcion_Mensaje = null;
                eventoEntidad.Mensaje = Excepcion.Message;
                eventoEntidad.MensajeDetallado = Excepcion.ToString();
            }
            else
            {
                st = new StackTrace(1, true);
                eventoEntidad.Mensaje = InterpretaObjetos(Excepcion_Mensaje);
            }
            eventoEntidad.MensajeAdicional = InterpretaObjetos(NotaMensajeExtra);
            eventoEntidad.TipoEvento = TipoEvento;
            foreach (StackFrame sf in st.GetFrames())
            {
                var ttt = sf.GetFileLineNumber();
                if (ttt != 0)
                {
                    eventoEntidad.NombreClase = sf.GetMethod().DeclaringType.Name;
                    eventoEntidad.NombreMetodo = sf.GetMethod().Name;
                    eventoEntidad.NumeroLinea = ttt;
                    eventoEntidad.NumeroColumna = sf.GetFileColumnNumber();
                }
            }
            await GuardarRegistro(eventoEntidad);
        }
        private static async Task GuardarRegistro(EventoEntidad eventoEntidad)
        {
            try
            {
                LogsE Lc = new LogsE();
                BaseConfiguracion.ObtenerSeccionBase("Logs").Bind(Lc);
                if (Lc.UtilizarMicroServicio)
                {
                    if (!string.IsNullOrEmpty(Lc.ConfiguracionMicroservicio.URL))
                        await Comunicacion.Comunicacion.PostLocalAsync(Lc.ConfiguracionMicroservicio.URL, eventoEntidad, new List<CabeceraRequest>() { new CabeceraRequest() { Titulo = "LlaveServicio", Valor = Seguridad.Seguridad.T3DES.EncryptKeyTripleDes(DateTime.Now.ToString()) } }, true);
                    else
                    {
                        throw new Exception("La seccion de ConfiguracionMicroservicio no contiene la URL requerida.");
                    }
                }
                if (Lc.GuardarEnArchivo)
                {
                    await EventosLocales.Registrar(eventoEntidad);
                }
            }
            catch(Exception Ex)
            {
                await EventosLocales.Registrar(Ex);
            }
        }
        internal static string InterpretaObjetos(object Objeto)
        {
            if (Objeto != null)
            {
                Type tp = Objeto.GetType();
                if (tp.Equals(typeof(string)))
                {
                    return ((string)Objeto);
                }
                else
                {
                    return JsonConvert.SerializeObject(Objeto);
                }
            }
            else
            {
                return "";
            }
        }
    }
}




