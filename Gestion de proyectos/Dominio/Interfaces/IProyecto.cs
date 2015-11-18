using System;
using System.Collections.Generic;
using Dominio;
using PersistenciaInterfaz;

namespace DominioInterfaz
{
    public interface IProyecto
    {
        IContextoGestorProyectos Contexto { get; set; }
        int DuracionEstimada { get; set; }
        bool EstaAtrasado { get; }
        bool EstaFinalizado { get; }
        List<Etapa> Etapas { get; set; }
        DateTime FechaFinalizacion { get; }
        DateTime FechaInicio { get; set; }
        string Historial { get; set; }
        string Nombre { get; set; }
        string Objetivo { get; set; }
        int ProyectoID { get; set; }
        void AgregarEtapa(Etapa etapa);
        void AgregarModificacion(string cambio);
        int CalcularDuracionPendiente();
        bool ContieneEtapa(Etapa etapa);
        bool Equals(object obj);
        void MarcarFinalizado();
        void QuitarEtapa(Etapa etapa);
    }
}