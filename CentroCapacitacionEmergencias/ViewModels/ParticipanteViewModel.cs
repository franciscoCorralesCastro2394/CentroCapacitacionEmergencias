using CentroCapacitacionEmergencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CentroCapacitacionEmergencias.ViewModels
{
    public class ParticipanteViewModel
    {
        public int Id { get; set; } = 0;
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

        //Filtros  

        public string filtroNombre { get; set; }
        public string filtroCedula { get; set; }
        public int? filtroCohorteId { get; set; }
        public int? filtroCursoId { get; set; }

        //Listas paar busqueda
        public List<SelectListItem> Cohortes { get; set; }
        public List<SelectListItem> Cursos { get; set; }

        //Lista de participantes
        public List<Participante> Participantes { get; set; }

        //detalle ID para busqueda Detalles de un partisipante 

        public int? detalleId { get; set; }

        //ID para Evaluar un participante
        public int? evaluacionId { get; set; }

        //Nombre del participante evaluado 
        public string ParticipanteEvaluado { get; set; }

        //Listas para mostrar en detalles de un participante
        public List<Destreza> destrezasParticipante { get; set; }

        //Lista de cursos para evaluar a un participante
        public List<SelectListItem> CursosPorEvaluar { get; set; }

        //ID del curso para evaluar a un participante
        public int CursoIdEvaluar { get; set; }

        //titulo del curso a evaluar
        public string CursoTituloEvaluar { get; set; }
    }
}