using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Etapa : IFechas, INombrable, IDuracionPendienteCalculable, IPersonificable, IDuracionEstimable
    {
        public String Nombre { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EtapaID { get; set; }
        public int DuracionEstimada { get; set; }
        public virtual List<Tarea> Tareas { get; set; }
        public virtual List<Persona> Personas { get; set; }
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
                return UltimaFechaDeTareas();
            }
        }
        

        public Etapa()
        {
            Nombre = "[Nombre por defecto]";
            EstaFinalizada = false;
            Tareas = new List<Tarea>();
            Personas = new List<Persona>();
            FechaInicio = Tarea.FECHA_NULA;
        }
        

        public override bool Equals(object obj)
        {
            Etapa etapa = (Etapa)obj;
            return etapa.EtapaID == this.EtapaID;
        }

        public int CalcularDuracionPendiente()
        {
            int mayorDuracionPendiente = 0;
            Tarea tareaMasGrande = TareaQueFinalizaUltima();
            while (EsTareaAntecesora(tareaMasGrande))
            {
                mayorDuracionPendiente += tareaMasGrande.CalcularDuracionPendiente();
                tareaMasGrande = tareaMasGrande.UltimaAntecesora();
            }
            return mayorDuracionPendiente;
        }

        private bool EsTareaAntecesora(Tarea tareaMasGrande)
        {
            return tareaMasGrande!=null;
        }

        public void AgregarTarea(Tarea tarea)
        {
            Tareas.Add(tarea);
        }
        public void MarcarFinalizada() {
            if (TodasTareasFinalizadas())
            {
                EstaFinalizada = true;
            }
        }
        private bool TodasTareasFinalizadas()
        {
            bool valorRetorno = true;
            foreach (Tarea tarea in Tareas)
            {
                valorRetorno = valorRetorno && tarea.EstaFinalizada;

            }
            return valorRetorno;
        }

        private Tarea TareaQueFinalizaUltima()
        {
            Tarea tareaRetorno = null;
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

        private DateTime UltimaFechaDeTareas()
        {
            DateTime mayorFecha = TareaQueFinalizaUltima().FechaFinalizacion;
            return mayorFecha;
        }

        public void EliminarTarea(Tarea tarea)
        {
            if (Tareas.Contains(tarea))
            {
                Tareas.Remove(tarea);
            }
        }

        public void AgregarPersona(Persona persona)
        {
            Personas.Add(persona);
        }

    }
}
