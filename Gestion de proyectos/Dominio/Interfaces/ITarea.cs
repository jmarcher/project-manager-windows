using System;
using System.Collections.Generic;
using Dominio;
using PersistenciaInterfaz;

namespace DominioInterfaz
{
    public interface ITarea
    {
        List<Tarea> Antecesoras { get; set; }
        IContextoGestorProyectos Contexto { get; set; }
        string Descripcion { get; set; }
        int DuracionEstimada { get; set; }
        bool EstaAtrasada { get; }
        bool EstaFinalizada { get; }
        DateTime FechaFinalizacion { get; set; }
        DateTime FechaInicio { get; set; }
        DateTime FechaModificada { get; set; }
        string Historial { get; set; }
        string Nombre { get; set; }
        string Objetivo { get; set; }
        List<Persona> Personas { get; set; }
        int Prioridad { get; set; }
        int TareaID { get; set; }

        bool AgregarAntecesora(Tarea antecesora);
        void AgregarModificacion(string modificacion);
        void AgregarPersona(Persona persona);
        int CalcularDuracionPendiente();
        Tarea Clonar();
        void DefinirPrioridad(string prioridad);
        bool Equals(object obj);
        bool EstaEnSubtareas(Tarea tarea);
        bool FechaEsIgual(DateTime primera, DateTime segunda);
        void MarcarFinalizada();
        Proyecto ObtenerProyectoPadre();
        string prioridadAString();
        string ToString();
        Tarea UltimaAntecesora();
    }
}