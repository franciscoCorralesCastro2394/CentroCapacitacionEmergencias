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

        //numero maximo de instructores por curso, se puede modificar si se desea permitir mas de 1 instructor por curso
        public int? maxInstructores { get; set; } = 1;

    }
}