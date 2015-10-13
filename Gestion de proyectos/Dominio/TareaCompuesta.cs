using Dominio.Excepciones;
using System.Collections.Generic;
using System;

namespace Dominio
{
    public class TareaCompuesta : Tarea
    {
        public List<Tarea> Subtareas { get; private set; }

        public override DateTime FechaFinalizacion
        {
            get
            {
                return FechaMayorDeSubtarea();
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        public override bool EstaAtrasada
        {
            get
            {
                foreach (Tarea tarea in Subtareas)
                {
                    if (tarea.EstaAtrasada)
                        return true;
                }
                return false;
            }
        }

        public TareaCompuesta() : base()
        {
            Subtareas = new List<Tarea>();
        }

        public TareaCompuesta(Tarea tareaSimple) : base()
        {
            Subtareas = new List<Tarea>();
            Antecesoras = tareaSimple.Antecesoras;
            Nombre = tareaSimple.Nombre;
            Prioridad = tareaSimple.Prioridad;
            Objetivo = tareaSimple.Objetivo;
            FechaInicio = tareaSimple.FechaInicio;
            this.Descripcion = tareaSimple.Descripcion;
        }

        private Tarea tareaFechaMayor()
        {
            DateTime fechaRetorno = DateTime.MinValue;
            Tarea tareaMayor = null;
            foreach (Tarea tarea in Subtareas)
            {
                if (tarea.FechaFinalizacion >= fechaRetorno)
                    tareaMayor = tarea;
            }
            return tareaMayor;
        }

        private DateTime FechaMayorDeSubtarea()
        {
            Tarea tarea = tareaFechaMayor();
            if (tarea == null)
                return Tarea.FECHA_NULA;
            return tarea.FechaFinalizacion;
        }


        private bool TareaIniciaDespues(Tarea tarea)
        {
            return FechaEsMenor(this.FechaInicio, tarea.FechaInicio)
                || FechaEsIgual(this.FechaInicio, tarea.FechaInicio);
        }

        public bool AgregarSubtarea(Tarea subtarea)
        {
            if (Equals(subtarea))
                return false;
            if (TareaIniciaDespues(subtarea))
                Subtareas.Add(subtarea);
            else
                throw new FechaInvalida();
            return true;
        }

        public override int CalcularDuracionPendiente()
        {
            Tarea tarea = tareaFechaMayor();
            if (tarea == null) return 0;
            int valorRetorno = tarea.CalcularDuracionPendiente();
            foreach (Tarea tareaAntecesora in tareaFechaMayor().Antecesoras)
            {
                valorRetorno += tareaAntecesora.CalcularDuracionPendiente();
            }
            return valorRetorno;
        }

        private bool TodasSubTareasFinalizadas()
        {
            bool valorRetorno = true;
            foreach (Tarea tarea in Subtareas)
            {
                valorRetorno = valorRetorno && tarea.EstaFinalizada;

            }
            return valorRetorno;
        }

        public override void MarcarFinalizada()
        {
            if (TodasSubTareasFinalizadas())
                EstaFinalizada = true;
        }

        public void EliminarSubtarea(Tarea tarea)
        {
            if (Subtareas.Contains(tarea))
            {
                Subtareas.Remove(tarea);
            }
        }
        public override Tarea Clonar() 
        {
            TareaCompuesta copia = new TareaCompuesta
            {
                Nombre = this.Nombre,
                Objetivo = this.Objetivo,
                Descripcion = this.Descripcion,
                FechaInicio = this.FechaInicio,
                Prioridad = this.Prioridad,
                Subtareas = this.Subtareas,
                EstaFinalizada = this.EstaFinalizada,
                Antecesoras = this.Antecesoras
            };
          return copia;
        }

        
    }
}