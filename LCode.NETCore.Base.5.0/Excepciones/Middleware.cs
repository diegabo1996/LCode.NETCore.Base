using LCode.NETCore.Base._5._0.Logs;
using System;

namespace LCode.NETCore.Base._5._0.Excepciones
{
    public static class MiddlewareExcepciones
    {
        public static void IniciarCapturaExcepciones()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(CapturaExcepciones);
        }

        static async void CapturaExcepciones(object sender, UnhandledExceptionEventArgs args)
        {
            await Evento.ErrorNoControladoAsync((Exception)args.ExceptionObject);
        }
    }
}
