using LCode.NETCore.Base._5._0.Configuracion;
using LCode.NETCore.Base._5._0.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using static LCode.NETCore.Base._5._0.Seguridad.Seguridad;

namespace LCode.NETCore.Base._5._0.BaseDatos
{

    public class ConexionBD : DbContext, IConexionBDs
    {
        private IConexionBDs conexionBDs;
        public string NombreConexion;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            BaseConfiguracion Config = new BaseConfiguracion();
            string Servidor = Config.ObtenerValorDB("Conexion"+ NombreConexion + ":BDServidor");
            string BaseDatos = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDNombre");
            string Usuario = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDUsuario");
            string Contrasena = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDContrasena");
            var Conexion = new SqlConnectionStringBuilder
            {
                DataSource = Servidor,
                InitialCatalog = BaseDatos,
                UserID = Usuario,
                Password = Contrasena
            };
            optionsBuilder.UseSqlServer(Conexion.ToString());
        }

        public void Iniciar(string Contexto)
        {
            NombreConexion = Contexto;
        }
    }
    public class ConexionBDN : DbContext
    {
        public string NombreConexion="CSH";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            BaseConfiguracion Config = new BaseConfiguracion();
            string Servidor = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDServidor");
            string BaseDatos = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDNombre");
            string Usuario = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDUsuario");
            string Contrasena = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDContrasena");
            var Conexion = new SqlConnectionStringBuilder
            {
                DataSource = Servidor,
                InitialCatalog = BaseDatos,
                UserID = Usuario,
                Password = Contrasena
            };
            optionsBuilder.UseSqlServer(Conexion.ToString());
        }
    }
    public class ConexionBOL : DbContext
    {
        public string NombreConexion = "BOL";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            BaseConfiguracion Config = new BaseConfiguracion();
            string Servidor = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDServidor");
            string BaseDatos = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDNombre");
            string Usuario = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDUsuario");
            string Contrasena = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDContrasena");
            var Conexion = new SqlConnectionStringBuilder
            {
                DataSource = Servidor,
                InitialCatalog = BaseDatos,
                UserID = Usuario,
                Password = Contrasena
            };
            optionsBuilder.UseSqlServer(Conexion.ToString());
        }
    }
    public class ConexionOTP : DbContext
    {
        public string NombreConexion = "OTP";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            BaseConfiguracion Config = new BaseConfiguracion();
            string Servidor = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDServidor");
            string BaseDatos = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDNombre");
            string Usuario = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDUsuario");
            string Contrasena = Config.ObtenerValorDB("Conexion" + NombreConexion + ":BDContrasena");
            var Conexion = new SqlConnectionStringBuilder
            {
                DataSource = Servidor,
                InitialCatalog = BaseDatos,
                UserID = Usuario,
                Password = Contrasena
            };
            optionsBuilder.UseSqlServer(Conexion.ToString());
        }
    }

    public class ConexionBDParametrico : DbContext
    {
        public ParametrosConexion ConexionParametrica;
        public ConexionBDParametrico(ParametrosConexion _ConexionParametrica)
        {
            ConexionParametrica = _ConexionParametrica;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            BaseConfiguracion Config = new BaseConfiguracion();
            string Servidor = ConexionParametrica.Servidor;
            string BaseDatos = ConexionParametrica.BaseDatos;
            string Usuario = ConexionParametrica.Usuario;
            string Contrasena = ConexionParametrica.Contrasena;
            var Conexion = new SqlConnectionStringBuilder
            {
                DataSource = Servidor,
                InitialCatalog = BaseDatos,
                UserID = Usuario,
                Password = RSA.Desencriptar(Contrasena)
            };
            optionsBuilder.UseSqlServer(Conexion.ToString());
        }
    }
}
