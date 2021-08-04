using LCode.NETCore.Base._5._0.Configuracion;
using LCode.NETCore.Base._5._0.Logs;
using NUnit.Framework;
using System;

namespace LCode.NETCore.Base._5._0.Test
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Evento evn = new Evento();
            string llave = Seguridad.Seguridad.T3DES.EncryptKeyTripleDes(DateTime.Now.ToString());
            try
            {
                Evento.ErrorAsync(Entidades.TipoEvento.Error,"ERROR!");
                BaseConfiguracion.ObtenerValor("AAAA");
            }
            catch(Exception Ex)
            {
                Evento.ErrorAsync(Entidades.TipoEvento.Error, Ex);
            }
        }
    }
}