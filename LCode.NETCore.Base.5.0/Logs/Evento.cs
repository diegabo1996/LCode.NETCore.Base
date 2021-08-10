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
using System.ComponentModel;

namespace LCode.NETCore.Base._5._0.Logs
{
    public class Evento
    {
        #region MetodosRegistroEventos
        public static async Task ErrorAsync(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            await RegistrarAsync(Entidades.TipoEvento.Error, Excepcion_Mensaje, NotaMensajeExtra);
        }
        public static void Error(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            Task.Run(async () => await RegistrarAsync(Entidades.TipoEvento.Error, Excepcion_Mensaje, NotaMensajeExtra));
        }
        public static async Task InformativoAsync(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            if (NivelLogs() == 1 || NivelLogs() == 2 || NivelLogs() == 3)
                await RegistrarAsync(Entidades.TipoEvento.Informativo, Excepcion_Mensaje, NotaMensajeExtra);
        }
        public static void Informativo(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            if (NivelLogs() == 1 || NivelLogs() == 2 || NivelLogs() == 3)
                Task.Run(async () => await RegistrarAsync(Entidades.TipoEvento.Informativo, Excepcion_Mensaje, NotaMensajeExtra));
        }
        public static async Task AdvertenciaAsync(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            if (NivelLogs() == 1 || NivelLogs() == 2)
                await RegistrarAsync(Entidades.TipoEvento.Advertencia, Excepcion_Mensaje, NotaMensajeExtra);
        }
        public static void Advertencia(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            if (NivelLogs() == 1 || NivelLogs() == 2)
                Task.Run(async () => await RegistrarAsync(Entidades.TipoEvento.Advertencia, Excepcion_Mensaje, NotaMensajeExtra));
        }
        public static void Depuracion(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            if (NivelLogs() == 1)
                Task.Run(async () => await RegistrarAsync(Entidades.TipoEvento.Depuracion, Excepcion_Mensaje, NotaMensajeExtra));
        }
        public static async Task DepuracionAsync(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            if (NivelLogs() == 1)
                await RegistrarAsync(Entidades.TipoEvento.Depuracion, Excepcion_Mensaje, NotaMensajeExtra);
        }
        public static async Task ErrorNoControladoAsync(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            await RegistrarAsync(Entidades.TipoEvento.Error_No_Controlado, Excepcion_Mensaje, NotaMensajeExtra);
        }
        public static void ErrorNoControlado(object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            Registrar(Entidades.TipoEvento.Error_No_Controlado, Excepcion_Mensaje, NotaMensajeExtra);
        }
        #endregion MetodosRegistroEventos

        #region MetodosAuxiliares
        internal static async Task RegistrarAsync(Entidades.TipoEvento TipoEvento, object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            AplicativoComponente Aplicativo = new AplicativoComponente();
            Aplicativo.NombreComponente = DatosServicio.NombreServicio;
            Aplicativo.NombreComponenteCompleto = DatosServicio.NombreServicioCompleto;
            Aplicativo.ListaOrigen = new List<EventoOrigen>();
            #region EventoOrigen
            EventoOrigen eventoOrigen = new EventoOrigen();
            eventoOrigen.EsDocker = DatosServicio.EsDocker;
            eventoOrigen.Version = DatosServicio.Version;
            eventoOrigen.NombreHost = Environment.MachineName;
            eventoOrigen.ListaEventos = new List<EventoEntidad>();
            #region Evento
            Exception Excepcion = null;
            EventoEntidad eventoEntidad = new EventoEntidad();
            StackTrace st;
            if (Excepcion_Mensaje.GetType().Name.ToUpper().Contains("EXCEPTION"))
            {
                Excepcion = ((Exception)Excepcion_Mensaje);

                var w32ex = Excepcion as Win32Exception;
                if (w32ex == null)
                {
                    w32ex = Excepcion.InnerException as Win32Exception;
                }
                int code = 0;
                if (w32ex != null)
                {
                    code = w32ex.ErrorCode;
                }
                string ExcepcionKnd = Excepcion.GetType().ToString();
                st = new StackTrace(((Exception)Excepcion), true);
                Excepcion_Mensaje = null;
                eventoEntidad.Mensaje = Excepcion.Message;
                eventoEntidad.MensajeDetallado = Excepcion.ToString();
                eventoEntidad.CodigoExcepcion = code;
                eventoEntidad.TipoExcepcion = ExcepcionKnd;
            }
            else
            {
                st = new StackTrace(1, true);
                eventoEntidad.Mensaje = InterpretaObjetos(Excepcion_Mensaje);
            }
            if (Activity.Current != null)
            {
                eventoEntidad.IdActividad = Activity.Current?.RootId;
            }
            eventoEntidad.MensajeAdicional = InterpretaObjetos(NotaMensajeExtra);
            eventoEntidad.TipoEvento = TipoEvento;
            if (st != null)
            {
                eventoEntidad.ListaRastros = new List<RastroEntidad>();
                #region Rastros
                foreach (StackFrame sf in st.GetFrames())
                {
                    var ttt = sf.GetFileLineNumber();
                    if (ttt != 0)
                    {
                        RastroEntidad rastroEntidad = new RastroEntidad();
                        rastroEntidad.NombreDll = sf.GetMethod().DeclaringType.Assembly.ManifestModule.Name;
                        rastroEntidad.NombreArchivo = sf.GetFileName();
                        rastroEntidad.NombreClase = sf.GetMethod().DeclaringType.FullName;
                        rastroEntidad.NombreMetodo = sf.GetMethod().DeclaringType.Name;
                        rastroEntidad.NumeroLinea = ttt;
                        rastroEntidad.NumeroColumna = sf.GetFileColumnNumber();
                        eventoEntidad.ListaRastros.Add(rastroEntidad);
                    }
                }
            }
            #endregion Rastros
            eventoOrigen.ListaEventos.Add(eventoEntidad);
            #endregion Evento
            Aplicativo.ListaOrigen.Add(eventoOrigen);
            #endregion EventoOrigen
            await GuardarRegistroAsync(Aplicativo);
        }
        internal static void Registrar(Entidades.TipoEvento TipoEvento, object Excepcion_Mensaje = null, object NotaMensajeExtra = null)
        {
            AplicativoComponente Aplicativo = new AplicativoComponente();
            Aplicativo.NombreComponente = DatosServicio.NombreServicio;
            Aplicativo.NombreComponenteCompleto = DatosServicio.NombreServicioCompleto;
            Aplicativo.ListaOrigen = new List<EventoOrigen>();
            #region EventoOrigen
            EventoOrigen eventoOrigen = new EventoOrigen();
            eventoOrigen.EsDocker = DatosServicio.EsDocker;
            eventoOrigen.Version = DatosServicio.Version;
            eventoOrigen.NombreHost = Environment.MachineName;
            eventoOrigen.ListaEventos = new List<EventoEntidad>();
            #region Evento
            Exception Excepcion = null;
            EventoEntidad eventoEntidad = new EventoEntidad();
            StackTrace st;
            if (Excepcion_Mensaje.GetType().Name.ToUpper().Contains("EXCEPTION"))
            {
                Excepcion = ((Exception)Excepcion_Mensaje);

                var w32ex = Excepcion as Win32Exception;
                if (w32ex == null)
                {
                    w32ex = Excepcion.InnerException as Win32Exception;
                }
                int code = 0;
                if (w32ex != null)
                {
                    code = w32ex.ErrorCode;
                }
                string ExcepcionKnd = Excepcion.GetType().ToString();
                st = new StackTrace(((Exception)Excepcion), true);
                Excepcion_Mensaje = null;
                eventoEntidad.Mensaje = Excepcion.Message;
                eventoEntidad.MensajeDetallado = Excepcion.ToString();
                eventoEntidad.CodigoExcepcion = code;
                eventoEntidad.TipoExcepcion = ExcepcionKnd;
            }
            else
            {
                st = new StackTrace(1, true);
                eventoEntidad.Mensaje = InterpretaObjetos(Excepcion_Mensaje);
            }
            if (Activity.Current != null)
            {
                eventoEntidad.IdActividad = Activity.Current?.RootId;
            }
            eventoEntidad.MensajeAdicional = InterpretaObjetos(NotaMensajeExtra);
            eventoEntidad.TipoEvento = TipoEvento;
            if (st != null)
            {
                eventoEntidad.ListaRastros = new List<RastroEntidad>();
                #region Rastros
                foreach (StackFrame sf in st.GetFrames())
                {
                    var ttt = sf.GetFileLineNumber();
                    if (ttt != 0)
                    {
                        RastroEntidad rastroEntidad = new RastroEntidad();
                        rastroEntidad.NombreDll = sf.GetMethod().DeclaringType.Assembly.ManifestModule.Name;
                        rastroEntidad.NombreArchivo = sf.GetFileName();
                        rastroEntidad.NombreClase = sf.GetMethod().DeclaringType.FullName;
                        rastroEntidad.NombreMetodo = sf.GetMethod().DeclaringType.Name;
                        rastroEntidad.NumeroLinea = ttt;
                        rastroEntidad.NumeroColumna = sf.GetFileColumnNumber();
                        eventoEntidad.ListaRastros.Add(rastroEntidad);
                    }
                }
            }
            #endregion Rastros
            eventoOrigen.ListaEventos.Add(eventoEntidad);
            #endregion Evento
            Aplicativo.ListaOrigen.Add(eventoOrigen);
            #endregion EventoOrigen
            GuardarRegistro(Aplicativo);
        }

        private static async Task GuardarRegistroAsync(AplicativoComponente eventoEntidad)
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
            catch (Exception Ex)
            {
                await EventosLocales.Registrar(Ex);
            }
        }
        private static void GuardarRegistro(AplicativoComponente eventoEntidad)
        {
            try
            {
                LogsE Lc = new LogsE();
                BaseConfiguracion.ObtenerSeccionBase("Logs").Bind(Lc);
                if (Lc.UtilizarMicroServicio)
                {
                    if (!string.IsNullOrEmpty(Lc.ConfiguracionMicroservicio.URL))
                        Comunicacion.Comunicacion.PostLocal(Lc.ConfiguracionMicroservicio.URL, eventoEntidad, new List<CabeceraRequest>() { new CabeceraRequest() { Titulo = "LlaveServicio", Valor = Seguridad.Seguridad.T3DES.EncryptKeyTripleDes(DateTime.Now.ToString()) } }, true);
                    else
                    {
                        throw new Exception("La seccion de ConfiguracionMicroservicio no contiene la URL requerida.");
                    }
                }
                if (Lc.GuardarEnArchivo)
                {
                    EventosLocales.Registrar(eventoEntidad);
                }
            }
            catch (Exception Ex)
            {
                EventosLocales.Registrar(Ex);
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
        internal static short NivelLogs()
        {
            string NivelStr = BaseConfiguracion.ObtenerValorBase("Logs:Nivel").ToUpper();
            switch (NivelStr)
            {
                case "DEPURACION":
                    return 1;
                case "ADVERTENCIA":
                    return 2;
                case "INFORMATIVO":
                    return 3;
                case "ERROR":
                    return 4;
                default:
                    return 1;
            }
        }
        #endregion MetodosAuxiliares

    }
}




