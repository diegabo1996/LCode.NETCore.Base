using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCode.NETCore.Base._5._0.Entidades
{
    public class AplicativosComponentes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAplicativoComponente { get; set; }
        [Required]
        [Column(TypeName = "varchar(200)")]
        [DisplayName("Nombre Aplicativo y/o Componente")]
        public string NombreComponente { get; set; }
        [Required]
        [Column(TypeName = "varchar(500)")]
        [DisplayName("Nombre Completo Componente")]
        public string NombreComponenteCompleto { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [DisplayName("Fecha y Hora Registro Componente")]
        public DateTime FechaHoraCreacion { get; set; }
    }
    public class EventoOrigen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEventoOrigen { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? IPOrigen { get; set; }
        [Column(TypeName = "varchar(25)")]
        [DisplayName("Nombre Host")]
        public string? NombreHost { get; set; }
        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Version { get; set; }
        [Required]
        [DisplayName("Docker")]
        public bool EsDocker { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [DisplayName("Fecha y Hora Registro Origen")]
        public DateTime FechaHoraCreacion { get; set; }
    }
    public class EventoEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEvento { get; set; }
        [Required]
        [DisplayName("Tipo de Evento")]
        public TipoEvento TipoEvento { get; set; }
        [Required]
        [Column(TypeName = "varchar(150)")]
        [DisplayName("Identificador Actividad")]
        public string IdActividad { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Nombre de Clase")]
        public string NombreClase { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Nombre de Método")]
        public string NombreMetodo { get; set; }
        public int NumeroLinea { get; set; }
        public int NumeroColumna { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string Mensaje { get; set; }
        [Column(TypeName = "text")]
        [DisplayName("Mensaje Completo")]
        public string? MensajeDetallado { get; set; }
        [Column(TypeName = "text")]
        [DisplayName("Mensaje Adicional")]
        public string? MensajeAdicional { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        [DisplayName("Fecha Hora Registro Evento")]
        public DateTime FechaHoraEvento { get; set; }
    }
    public enum TipoEvento
    {
        Error,
        Error_No_Controlado,
        Advertencia,
        Informativo
    }
}
