using LCode.NETCore.Base._5._0.Configuracion;
using LCode.NETCore.Base._5._0.Logs;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace LCode.NETCore.Base._5._0.BaseDatos
{
    public interface IConexionBDs
    {
        void Iniciar(string Contexto);
    }

    public class ConexionContextoBD : DbContext, IConexionBDs
    {
        public string NombreConexion;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            RegistroLogs Logs = new RegistroLogs();
            BaseConfiguracion Config = new BaseConfiguracion();
            LCode.NETCore.Base._5._0.Entidades.ConexionBD ConexionFinal = Config.ObtenerBD(NombreConexion);
            var Conexion = new SqlConnectionStringBuilder
            {
                DataSource = ConexionFinal.Servidor,
                InitialCatalog = ConexionFinal.BaseDatos,
                UserID = ConexionFinal.Usuario,
                Password = ConexionFinal.Contrasenia
            };
            optionsBuilder.UseSqlServer(Conexion.ToString());
        }

        public void Iniciar(string Contexto)
        {
            NombreConexion = Contexto;
        }
    }
}
