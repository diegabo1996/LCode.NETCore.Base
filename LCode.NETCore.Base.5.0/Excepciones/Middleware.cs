using LCode.NETCore.Base._5._0.Logs;
using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using static LCode.NETCore.Base._5._0.Excepciones.MiddlewareExcepciones;
using System.ComponentModel;

namespace LCode.NETCore.Base._5._0.Excepciones
{
    public static class MiddlewareExcepciones
    {
        public class ExceptionHandlingMiddleware
        {
            private readonly RequestDelegate next;

            public ExceptionHandlingMiddleware(RequestDelegate next)
            {
                this.next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    await handleExceptionAsync(context, ex);
                }
            }

            private static async Task handleExceptionAsync(HttpContext context, Exception exception)
            {
                var w32ex = exception as Win32Exception;
                if (w32ex == null)
                {
                    w32ex = exception.InnerException as Win32Exception;
                }
                int code = 0;
                if (w32ex != null)
                {
                    code = w32ex.ErrorCode;
                    // do stuff
                }
                //string Excepcion = exception.GetType().ToString();
                string errorCode = calculateErrorCode(context.TraceIdentifier);
                string message = string.Format("Ha ocurrido un error no controlado: '{0}'  [{1}]  "+ code + "", errorCode, context.TraceIdentifier);

                await Evento.ErrorNoControladoAsync(exception, message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            }

            private static string calculateErrorCode(string traceIdentifier)
            {
                const int ErrorCodeLength = 6;
                const string CodeValues = "BCDFGHJKLMNPQRSTVWXYZ";

                MD5 hasher = MD5.Create();

                StringBuilder sb = new StringBuilder(10);

                byte[] traceBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(traceIdentifier));

                int codeValuesLength = CodeValues.Length;

                for (int i = 0; i < ErrorCodeLength; i++)
                {
                    sb.Append(CodeValues[traceBytes[i] % codeValuesLength]);
                }

                return sb.ToString();
            }
        }
    }
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UsarApiCapturadorErrores(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
