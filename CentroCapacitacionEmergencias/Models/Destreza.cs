using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class Destreza
    {
        public int Id { get; set; }

       public string Titulo { get; set; }
  
        public string Descripcion { get; set; }

        public virtual ICollection<CursoDestrezas> CursoDestrezas { get; set; }

    }
}