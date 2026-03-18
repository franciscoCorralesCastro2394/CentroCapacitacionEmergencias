using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class CursoDestrezas
    {
        public int Id { get; set; }

        public int CursoId { get; set; }
        public int DestrezaId { get; set; }

        public virtual Curso Curso { get; set; }
        public virtual Destreza Destreza { get; set; }
    }
}