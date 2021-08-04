using LCode.NETCore.Base._5._0.Configuracion;
using LCode.NETCore.Base._5._0.Entidades;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LCode.NETCore.Base._5._0.Logs
{
    public class EventosLocales
    {
        internal static async Task Registrar(object Excepcion_Mensaje, object SegundoMensaje = null)
        {
            string Mensaje = Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------" +
            Environment.NewLine + DateTime.Now.ToString() +
            Environment.NewLine + "Log-";
            if (Excepcion_Mensaje.GetType().Name.ToUpper().Contains("EXCEPTION"))
            {
                StackTrace st = new StackTrace(((Exception)Excepcion_Mensaje), true);
                StackFrame sf = st.GetFrame(0);
                Mensaje += "Excepcion::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + "Nombre Archivo: " + sf.GetFileName() +
Environment.NewLine + "Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + "Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + "Numero Linea: " + sf.GetFileLineNumber() +
Environment.NewLine + "Numero Columna: " + sf.GetFileColumnNumber() +
Environment.NewLine + "Mensaje Error: " + ((Exception)Excepcion_Mensaje).Message +
Environment.NewLine + "Mensaje Detallado: " + ((Exception)Excepcion_Mensaje).InnerException;
            }
            else
            {
                StackFrame sf = new StackFrame(1, true);
                Mensaje += @"Mensaje::: Datos - Nombre Proyecto: " + Assembly.GetEntryAssembly().GetName().Name +
Environment.NewLine + @"Nombre de Clase: " + sf.GetMethod().DeclaringType +
Environment.NewLine + @"Nombre Metodo: " + sf.GetMethod() +
Environment.NewLine + "Numero Linea: " + sf.GetFileLineNumber() +
Environment.NewLine + "Numero Columna: " + sf.GetFileColumnNumber() +
Environment.NewLine + @"Mensaje: " + Evento.InterpretaObjetos(Excepcion_Mensaje);
            }
            if (SegundoMensaje != null)
            {
                Mensaje += Environment.NewLine + "Mensaje Adicional 2: " + Evento.InterpretaObjetos(SegundoMensaje);
            }
            Mensaje += Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------";
            await GuardarLocal(Mensaje);
        }
        internal static async Task Registrar(AplicativoComponente eventoEntidad)
        {
            string Mensaje = Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------" +
            Environment.NewLine + DateTime.Now.ToString() +
            Environment.NewLine + "Log-";
            var TipoEvento = eventoEntidad.ListaOrigen.First().ListaEventos.First().TipoEvento;
            Mensaje += nameof(TipoEvento).ToString() + "::: Datos - Nombre Proyecto: " + eventoEntidad.NombreComponente +
            Environment.NewLine + "Version: " + eventoEntidad.ListaOrigen.First().Version +
            Environment.NewLine + "Id Seguimiento: " + eventoEntidad.ListaOrigen.First().IdActividad +
            Environment.NewLine + "Nombre Archivo: " + eventoEntidad.NombreComponenteCompleto +
            Environment.NewLine + "Nombre de Clase: " + eventoEntidad.ListaOrigen.First().ListaEventos.First().ListaRastros.First().NombreClase +
            Environment.NewLine + "Nombre Metodo: " + eventoEntidad.ListaOrigen.First().ListaEventos.First().ListaRastros.First().NombreMetodo +
            Environment.NewLine + "Numero Linea: " + eventoEntidad.ListaOrigen.First().ListaEventos.First().ListaRastros.First().NumeroLinea +
            Environment.NewLine + "Numero Columna: " + eventoEntidad.ListaOrigen.First().ListaEventos.First().ListaRastros.First().NumeroColumna +
            Environment.NewLine + "Mensaje: " + eventoEntidad.ListaOrigen.First().ListaEventos.First().Mensaje;
            if (!string.IsNullOrEmpty(eventoEntidad.ListaOrigen.First().ListaEventos.First().MensajeDetallado))
            {
                Mensaje += Environment.NewLine + "Mensaje Detallado: " + eventoEntidad.ListaOrigen.First().ListaEventos.First().MensajeDetallado;
            }
            if (!string.IsNullOrEmpty(eventoEntidad.ListaOrigen.First().ListaEventos.First().MensajeAdicional))
            {
                Mensaje += Environment.NewLine + "Mensaje Adicional 2: " + Evento.InterpretaObjetos(eventoEntidad.ListaOrigen.First().ListaEventos.First().MensajeAdicional);
            }
            Mensaje += Environment.NewLine + "-------------------------------------------------------------------------------------------------------------------------";
            await GuardarLocal(Mensaje);
        }
        private static async Task GuardarLocal(string Texto)
        {
            try
            {
                LogsE Lc = new LogsE();
                string RutaFinal = System.AppDomain.CurrentDomain.BaseDirectory + "LocalLogs";
                try
                { 
                    BaseConfiguracion.ObtenerSeccionBase("Logs").Bind(Lc);
                }
                catch
                {
                    RutaFinal = System.AppDomain.CurrentDomain.BaseDirectory + "LocalLogs";
                }
                if (Lc != null && Lc.GuardarEnArchivo && Lc.ConfiguracionArchivo != null && !string.IsNullOrEmpty(Lc.ConfiguracionArchivo.RutaGuardado))
                {
                    RutaFinal = Lc.ConfiguracionArchivo.RutaGuardado + "\\LogsAplicativos\\" + Assembly.GetEntryAssembly().GetName().Name;
                }
                if (!Directory.Exists(RutaFinal))
                {
                    Directory.CreateDirectory(RutaFinal);
                }
                StreamWriter ArchivoLog = new StreamWriter(RutaFinal + "\\" + DateTime.Today.ToString("dd-MM-yyy"), true);
                await ArchivoLog.WriteLineAsync(Texto);
                ArchivoLog.Close();
            }
            catch
            {
                //Si esto falla, solo queda correr!
            }
        }
    }
}
