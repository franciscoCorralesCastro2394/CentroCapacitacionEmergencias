using CentroCapacitacionEmergencias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentroCapacitacionEmergencias.ViewModels
{
    public class EvaluacionViewModel
    {
        // Propiedades para mostrar en la vista de evaluación

        // ID del curso y participante para realizar la evaluación
        public int idCurso { get; set; }
        public int idParticipante { get; set; }

        // ID de la destreza que se está evaluando
        public int idDestreza { get; set; }

        // Propiedades para mostrar información relevante en la vista
        public Curso Curso { get; set; }
        public Participante Participante { get; set; }
        public Destreza Destreza { get; set; }

        public EvaluacionDestreza Evaluacion { get; set; }

        // Lista de puntos de control asociados a la destreza que se está evaluando
        public List<PuntoControl> PuntoControls { get; set; }

    }
}