using DominioInterfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Etapa : IFechas, INombrable, IDuracionPendienteCalculable, IDuracionEstimable, IEtapa
    {
        public String Nombre { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EtapaID { get; set; }
        public int DuracionEstimada { get; set; }
        public virtual List<Tarea> Tareas { get; set; }
        public bool EstaFinalizada { get; private set; }
        public bool EstaAtrasada
        {
            get
            {
                foreach(Tarea tarea in Tareas)
                {
                    if (tarea.EstaAtrasada)
                        return true;
                }
                return false;
            }
        }
        public DateTime FechaInicio { get;set; }
        public DateTime FechaFinalizacion {
            get
            {
                return ultimaFechaDeTareas();
            }
        }
        

        public Etapa()
        {
            Nombre = "[Nombre por defecto]";
            EstaFinalizada = false;
            Tareas = new List<Tarea>();
            FechaInicio = Tarea.FECHA_NULA;
            DuracionEstimada = 0;
        }
        

        public override bool Equals(object obj)
        {
            IEtapa etapa = (IEtapa)obj;
            return etapa.EtapaID == this.EtapaID;
        }

        public List<Tarea> ObtenerCaminoCritico()
        {
            List<Tarea> retorno = new List<Tarea>();
            Tarea tareaMasGrande = tareaQueFinalizaUltima();
            if (tareaMasGrande != null)
                retorno.Add(tareaMasGrande);
            while (esTareaAntecesora(tareaMasGrande))
            {
                tareaMasGrande = tareaMasGrande.UltimaAntecesora();
                if(tareaMasGrande != null)
                    retorno.Add(tareaMasGrande);
            }
            return retorno;
        }

        public int CalcularDuracionPendiente()
        {
            int mayorDuracionPendiente = 0;
            foreach (Tarea t in ObtenerCaminoCritico())
            {
                if(esTareaAntecesora(t))
                    mayorDuracionPendiente += t.CalcularDuracionPendiente();
            }
            return mayorDuracionPendiente;
        }

        private bool esTareaAntecesora(Tarea tareaMasGrande)
        {
            return tareaMasGrande!=null;
        }

        public void AgregarTarea(Tarea tarea)
        {
            Tareas.Add(tarea);
        }
        public void MarcarFinalizada() {
            if (todasTareasFinalizadas())
            {
                EstaFinalizada = true;
            }
        }
        private bool todasTareasFinalizadas()
        {
            bool valorRetorno = true;
            foreach (Tarea tarea in Tareas)
            {
                valorRetorno = valorRetorno && tarea.EstaFinalizada;

            }
            return valorRetorno;
        }

        private Tarea tareaQueFinalizaUltima()
        {
            Tarea tareaRetorno = new TareaSimple();
            DateTime mayorFecha = DateTime.MinValue;
            foreach (Tarea tarea in Tareas)
            {
                if (tarea.FechaFinalizacion > mayorFecha)
                {
                    mayorFecha = tarea.FechaFinalizacion;
                    tareaRetorno = tarea;
                }
            }
            return tareaRetorno;
        }

        private DateTime ultimaFechaDeTareas()
        {
            DateTime mayorFecha = tareaQueFinalizaUltima().FechaFinalizacion;
            return mayorFecha;
        }

        public void EliminarTarea(Tarea tarea)
        {
            if (Tareas.Contains(tarea))
            {
                Tareas.Remove(tarea);
            }
        }

    }
}
