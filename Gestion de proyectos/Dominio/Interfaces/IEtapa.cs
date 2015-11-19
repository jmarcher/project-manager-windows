using System;
using System.Collections.Generic;
using Dominio;

namespace DominioInterfaz
{
    public interface IEtapa
    {
        int DuracionEstimada { get; set; }
        bool EstaAtrasada { get; }
        bool EstaFinalizada { get; }
        int EtapaID { get; set; }
        DateTime FechaFinalizacion { get; }
        DateTime FechaInicio { get; set; }
        string Nombre { get; set; }
        List<Tarea> Tareas { get; set; }

        void AgregarTarea(Tarea tarea);
        int CalcularDuracionPendiente();
        void EliminarTarea(Tarea tarea);
        bool Equals(object obj);
        void MarcarFinalizada();
        List<Tarea> ObtenerCaminoCritico();
    }
}