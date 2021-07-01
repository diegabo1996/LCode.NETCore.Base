using LCode.NETCore.Base._5._0.BaseDatos;
using LCode.RegistroEventos.BD.Modelos;
using Microsoft.EntityFrameworkCore;

namespace LCode.RegistroEventos.BD
{
    public class Contexto : ConexionContextoBD
    {
        string NombreConexionContexto = "LCode.Logs";
        public Contexto()
        {
            Iniciar(NombreConexionContexto);
        }

        public DbSet<NuevoEvento> Eventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("lcode");
        }
    }
}
