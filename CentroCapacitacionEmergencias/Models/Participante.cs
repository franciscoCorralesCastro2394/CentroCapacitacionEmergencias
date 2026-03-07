using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.Models
{
    public class Participante
    {
        public int Id { get; set; }

        // Identificación
        [Required]
        public string TipoIdentificacion { get; set; }

        [Required]
        public string Identificacion { get; set; }

        // Datos personales
        [Required]
        public string NombreCompleto { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        // Residencia
        [Required]
        public string Provincia { get; set; }

        [Required]
        public string Canton { get; set; }

        [Required]
        public string Distrito { get; set; }

        [Required]
        public string DetalleDireccion { get; set; }

        // Estado civil
        [Required]
        public string EstadoCivil { get; set; }

        // Email único
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        // OPCIONALES
        public string Telefono { get; set; }

        public string DireccionResidencia { get; set; }

        public string ContactoEmergencia { get; set; }
    }
}