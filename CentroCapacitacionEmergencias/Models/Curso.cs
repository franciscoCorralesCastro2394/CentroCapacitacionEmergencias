using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string CodigoCurso { get; set; }

        [Required]
        public int HorasPracticas { get; set; }

        public bool? Archivado  { get; set; }

        // Relación con instructores
        public virtual ICollection<Usuario> Instructores { get; set; }

        // Relación con cohortes
        public virtual ICollection<Cohorte> Cohortes { get; set; }
    }
}