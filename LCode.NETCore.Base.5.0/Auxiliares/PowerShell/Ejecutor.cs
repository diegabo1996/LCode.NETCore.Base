using LCode.NETCore.Base._5._0.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LCode.NETCore.Base._5._0.Auxiliares.PowerShell
{
    public class Ejecutor
    {
        public string EjecucionSimple(string script)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe",
                script)
                {
                    WorkingDirectory = Environment.CurrentDirectory,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                }
            };
            process.Start();

            var reader = process.StandardOutput;
            process.Kill();
            return reader.ReadToEnd();
        }
        public PowerShellEntidad.Respuesta EjecucionCompleja(string command)
        {
            PowerShellEntidad.Respuesta Resp = new PowerShellEntidad.Respuesta();
            Resp.Respuestas = new List<string>();
            Resp.Errores = new List<string>();
            try
            {
                using (var ps = System.Management.Automation.PowerShell.Create())
                {

                    ps.AddCommand("Set-ExecutionPolicy")
                      .AddParameter("Scope", "Process")
                      .AddParameter("ExecutionPolicy", "Bypass")
                      .Invoke();
                    ps.AddScript(command, false);

                    var Rest = ps.Invoke();

                    ps.Commands.Clear();
                    if (Rest != null)
                    { 
                        foreach (var result in Rest)
                        {
                            if (result!=null && result.BaseObject!=null)
                            { 
                                Resp.Respuestas.Add(result.BaseObject.ToString());
                            }
                        }
                    }
                    Resp.Exito = true;

                    foreach (var error in ps.Streams.Error)
                    {
                        if (error!=null)
                        { 
                            Resp.Errores.Add(error.ToString());
                        }
                        Resp.Exito = false;
                    }
                }
                return Resp;
            }
            catch (Exception ex)
            {
                Resp.Errores.Add(ex.ToString());
                Resp.Exito = false;
                return Resp;
            }

        }

    }
}
