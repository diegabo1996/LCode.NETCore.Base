using LCode.NETCore.Base._5._0.Configuracion;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LCode.NETCore.Base._5._0.Logs
{
    public class RegistroLogs
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public void RegistrarEvento(string tipoEvento, object Excepcion_Mensaje = null, Exception Excepcion = null, object SegundoMensaje = null)
        {
            try
            {
                if (Excepcion_Mensaje.GetType().Name.ToUpper().Contains("EXCEPTION"))
                {
                    Excepcion = ((Exception)Excepcion_Mensaje);
                    Excepcion_Mensaje = null;
                }
                string UnidadLogs = BaseConfiguracion.ObtenerValorBase("UnidadLogs");
                string RutaFinal = UnidadLogs + ":\\LogsAplicativos\\" + Assembly.GetEntryAssembly().GetName().Name;
                if (!Directory.Exists(RutaFinal))
                {
                    Directory.CreateDirectory(RutaFinal);
                }
                StreamWriter ArchivoLog = new StreamWriter(RutaFinal + "\\" + DateTime.Today.ToString("dd-MM-yyy") + ".txt", true);
                string Mensaje = Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------" +
                    Environment.NewLine + DateTime.Now.ToString() +
                    Environment.NewLine + "Log-";
                if (tipoEvento == TipoEvento.Error)
                {
                    StackTrace st = new StackTrace(1, true);
                    StackFrame sf = st.GetFrame(0);
                    Mensaje += tipoEvento + "::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + "Nombre Archivo: " + sf.GetFileName() +
Environment.NewLine + "Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + "Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + "Numero Linea: " + sf.GetFileLineNumber() +
Environment.NewLine + "Numero Columna: " + sf.GetFileColumnNumber() +
Environment.NewLine + "Mensaje Error: " + ((Exception)Excepcion).Message +
Environment.NewLine + "Mensaje Detallado: " + ((Exception)Excepcion).InnerException +
Environment.NewLine + "Mensaje Adicional: " + InterpretaObjetos(Excepcion_Mensaje);
                    if (SegundoMensaje != null)
                    {
                        Mensaje += Environment.NewLine + "Mensaje Adicional 2: " + InterpretaObjetos(SegundoMensaje);
                    }
                }
                else if (tipoEvento == TipoEvento.Advertencia)
                {
                    StackFrame sf = new StackFrame(1);
                    Mensaje += @"" + tipoEvento + @"::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + @"Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + @"Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + @"Mensaje: " + InterpretaObjetos(Excepcion_Mensaje);
                    if (SegundoMensaje != null)
                    {
                        Mensaje += Environment.NewLine + "Mensaje Adicional 2: " + InterpretaObjetos(SegundoMensaje);
                    }
                }
                else if (tipoEvento == TipoEvento.Informativo)
                {
                    StackFrame sf = new StackFrame(1);
                    Mensaje += tipoEvento + "::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + "Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + "Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + "Mensaje: " + InterpretaObjetos(Excepcion_Mensaje);
                    if (SegundoMensaje != null)
                    {
                        Mensaje += Environment.NewLine + "Mensaje Adicional 2: " + InterpretaObjetos(SegundoMensaje);
                    }
                }
                Mensaje += Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------";
                ArchivoLog.WriteLine(Mensaje);
                ArchivoLog.Close();
            }
            catch
            {

            }
        }
        public void RegistrarEvento(string tipoEvento, object Excepcion_Mensaje = null)
        {
            try
            {
                Exception Excepcion = null;
                if (Excepcion_Mensaje.GetType().Name.ToUpper().Contains("EXCEPTION"))
                {
                    Excepcion = ((Exception)Excepcion_Mensaje);
                    Excepcion_Mensaje = null;
                }
                string UnidadLogs = BaseConfiguracion.ObtenerValorBase("UnidadLogs");
                string RutaFinal = UnidadLogs + ":\\LogsAplicativos\\" + Assembly.GetEntryAssembly().GetName().Name;
                if (!Directory.Exists(RutaFinal))
                {
                    Directory.CreateDirectory(RutaFinal);
                }
                StreamWriter ArchivoLog = new StreamWriter(RutaFinal + "\\" + DateTime.Today.ToString("dd-MM-yyy") + ".txt", true);
                string Mensaje = Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------" +
                    Environment.NewLine + DateTime.Now.ToString() +
                    Environment.NewLine + "Log-";
                if (tipoEvento == TipoEvento.Error)
                {
                    StackTrace st = new StackTrace(((Exception)Excepcion), true);
                    StackFrame sf = st.GetFrame(0);
                    Mensaje += tipoEvento + "::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + "Nombre Archivo: " + sf.GetFileName() +
Environment.NewLine + "Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + "Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + "Numero Linea: " + sf.GetFileLineNumber() +
Environment.NewLine + "Numero Columna: " + sf.GetFileColumnNumber() +
Environment.NewLine + "Mensaje Error: " + ((Exception)Excepcion).Message +
Environment.NewLine + "Mensaje Detallado: " + ((Exception)Excepcion).InnerException +
Environment.NewLine + "Mensaje Adicional: " + InterpretaObjetos(Excepcion_Mensaje);
                }
                else if (tipoEvento == TipoEvento.Advertencia)
                {
                    StackFrame sf = new StackFrame(1);
                    Mensaje += @"" + tipoEvento + @"::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + @"Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + @"Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + @"Mensaje: " + InterpretaObjetos(Excepcion_Mensaje);
                }
                else if (tipoEvento == TipoEvento.Informativo)
                {
                    StackFrame sf = new StackFrame(1);
                    Mensaje += tipoEvento + "::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + "Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + "Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + "Mensaje: " + InterpretaObjetos(Excepcion_Mensaje);
                }
                Mensaje += Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------";
                ArchivoLog.WriteLine(Mensaje);
                ArchivoLog.Close();
            }
            catch
            {

            }
        }

        public void RegistrarEvento(string tipoEvento, object Excepcion_Mensaje = null, object SegundoMensaje = null)
        {
            try
            {
                string UnidadLogs = BaseConfiguracion.ObtenerValorBase("UnidadLogs");
                string RutaFinal = UnidadLogs + ":\\LogsAplicativos\\" + Assembly.GetEntryAssembly().GetName().Name;
                if (!Directory.Exists(RutaFinal))
                {
                    Directory.CreateDirectory(RutaFinal);
                }
                StreamWriter ArchivoLog = new StreamWriter(RutaFinal + "\\" + DateTime.Today.ToString("dd-MM-yyy") + ".txt", true);
                string Mensaje = Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------" +
                    Environment.NewLine + DateTime.Now.ToString() +
                    Environment.NewLine + "Log-";
                if (tipoEvento == TipoEvento.Error)
                {
                    Exception Excepcion;
                    if (Excepcion_Mensaje.GetType().Name.ToUpper().Contains("EXCEPTION"))
                    {
                        Excepcion = ((Exception)Excepcion_Mensaje);
                        Excepcion_Mensaje = null;
                        StackTrace st = new StackTrace(((Exception)Excepcion), true);
                        StackFrame sf = st.GetFrame(0);
                        Mensaje += tipoEvento + "::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
    Environment.NewLine + "Nombre Archivo: " + sf.GetFileName() +
    Environment.NewLine + "Nombre de Clase: " + sf.GetMethod().DeclaringType +
    Environment.NewLine + "Nombre Metodo: " + sf.GetMethod() +
    Environment.NewLine + "Numero Linea: " + sf.GetFileLineNumber() +
    Environment.NewLine + "Numero Columna: " + sf.GetFileColumnNumber() +
    Environment.NewLine + "Mensaje Error: " + ((Exception)Excepcion).Message +
    Environment.NewLine + "Mensaje Detallado: " + ((Exception)Excepcion).InnerException +
    Environment.NewLine + "Mensaje Adicional: " + InterpretaObjetos(Excepcion_Mensaje);
                        if (SegundoMensaje != null)
                        {
                            Mensaje += Environment.NewLine + "Mensaje Adicional 2: " + InterpretaObjetos(SegundoMensaje);
                        }
                    }
                }
                else if (tipoEvento == TipoEvento.Advertencia)
                {
                    StackFrame sf = new StackFrame(1);
                    Mensaje += @"" + tipoEvento + @"::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + @"Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + @"Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + @"Mensaje: " + InterpretaObjetos(Excepcion_Mensaje);
                    if (SegundoMensaje != null)
                    {
                        Mensaje += Environment.NewLine + "Mensaje Adicional 2: " + InterpretaObjetos(SegundoMensaje);
                    }
                }
                else if (tipoEvento == TipoEvento.Informativo)
                {
                    StackFrame sf = new StackFrame(1);
                    Mensaje += tipoEvento + "::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + "Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + "Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + "Mensaje: " + InterpretaObjetos(Excepcion_Mensaje);
                    if (SegundoMensaje != null)
                    {
                        Mensaje += Environment.NewLine + "Mensaje Adicional 2: " + InterpretaObjetos(SegundoMensaje);
                    }
                }
                Mensaje += Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------";
                ArchivoLog.WriteLine(Mensaje);
                ArchivoLog.Close();
            }
            catch
            {

            }
        }
        public void RegistrarEventoSimple(string NombreEvento, DateTime FechaHora, string Evento)
        {
            try
            {
                string UnidadLogs = BaseConfiguracion.ObtenerValorBase("UnidadLogs");
                string RutaFinal = UnidadLogs + ":\\LogsAplicativos\\" + Assembly.GetEntryAssembly().GetName().Name + "\\EventosSimples";
                if (!Directory.Exists(RutaFinal))
                {
                    Directory.CreateDirectory(RutaFinal);
                }
                StreamWriter ArchivoLog = new StreamWriter(RutaFinal + "\\" + NombreEvento + ".txt", true);
                string Mensaje = FechaHora.ToString("dd-MM-yyyy HH:mm:ss") + "||" + Evento;
                ArchivoLog.WriteLine(Mensaje);
                ArchivoLog.Close();
            }
            catch
            {

            }
        }
        private string InterpretaObjetos(object Objeto)
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
    public static class TipoEvento
    {
        public static string Informativo = "Informativo";
        public static string Error = "Error";
        public static string Advertencia = "Advertencia";
    }
}
