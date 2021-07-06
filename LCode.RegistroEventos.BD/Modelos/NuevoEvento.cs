using LCode.NETCore.Base._5._0.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LCode.RegistroEventos.BD.Modelos
{
    public class NuevoEvento: EventoEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRegistroEvento { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? IPOrigen { get; set; }
    }
}
