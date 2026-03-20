using CentroCapacitacionEmergencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.ViewModels
{
    public class CohorteViewModel
    {
        //lista de cohortes para mostrar en la vista 
        public List<Cohorte> Cohortes { get; set; }

        //Propiedades para mostrar en la vista de Editar de una cohorte
        public Cohorte Cohorte { get; set; }
    }
}