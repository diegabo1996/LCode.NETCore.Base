using LCode.NETCore.Base._5._0.BaseDatos;
using LCode.NETCore.Base._5._0.Entidades;
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
        public DbSet<AplicativoComponente> AplicacionesComponentes { get; set; }
        public DbSet<EventoOrigen> EventosOrigenes { get; set; }
        public DbSet<EventoEntidad> Eventos { get; set; }
        public DbSet<RastroEntidad> RastrosEventos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("lcode");
        }
    }
}
