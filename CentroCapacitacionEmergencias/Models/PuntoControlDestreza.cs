using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class PuntoControlDestreza
    {
        public int Id { get; set; }

        public int IdPunto { get; set; }
        public int IdDestreza { get; set; }

        //public virtual PuntoControl Punto { get; set; }
        //public virtual Destreza Destreza { get; set; }


    }
}