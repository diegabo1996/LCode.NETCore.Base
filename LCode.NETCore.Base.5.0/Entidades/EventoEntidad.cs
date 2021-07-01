using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCode.NETCore.Base._5._0.Entidades
{
    public class EventoEntidad
    {
        [Required]
        public TipoEvento TipoEvento { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string NombreComponente { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string NombreComponenteCompleto { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Version { get; set; }
        [Required]
        public bool EsDocker { get; set; }
        [Required]
        [Column(TypeName = "varchar(150)")]
        public string IdActividad { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string NombreClase { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string NombreMetodo { get; set; }
        public int NumeroLinea { get; set; }
        public int NumeroColumna { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Mensaje { get; set; }
        [Column(TypeName = "text")]
        public string? MensajeDetallado { get; set; }
        [Column(TypeName = "text")]
        public string? MensajeAdicional { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime FechaHoraEvento { get; set; }
    }
    public enum TipoEvento
    {
        Error,
        Advertencia,
        Informativo
    }
}
