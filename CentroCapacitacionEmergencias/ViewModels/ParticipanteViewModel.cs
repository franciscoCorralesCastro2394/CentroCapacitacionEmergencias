using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.ViewModels
{
    public class ParticipanteViewModel
    {
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }

        public string NombreCompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Provincia { get; set; }

        public string Canton { get; set; }

        public string Distrito { get; set; }

        public string DetalleDireccion { get; set; }

        public string EstadoCivil { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string DireccionResidencia { get; set; }

        public string ContactoEmergencia { get; set; }
    }
}