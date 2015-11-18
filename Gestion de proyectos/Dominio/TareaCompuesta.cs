using Dominio.Excepciones;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using PersistenciaInterfaz;

namespace Dominio
{
    public class TareaCompuesta : Tarea
    {
        public virtual List<Tarea> Subtareas { get; set; }

        public override DateTime FechaFinalizacion
        {
            get
            {
                return fechaMayorDeSubtarea();
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

        public TareaCompuesta() : base() { Subtareas = new List<Tarea>(); }

        public TareaCompuesta(IContextoGestorProyectos contexto) : base(contexto)
        {
            Subtareas = new List<Tarea>();
        }

        public TareaCompuesta(Tarea tareaSimple) : base(tareaSimple.Contexto)
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

        private DateTime fechaMayorDeSubtarea()
        {
            Tarea tarea = tareaFechaMayor();
            if (tarea == null)
                return Tarea.FECHA_NULA;
            return tarea.FechaFinalizacion;
        }

        public bool FechaEsMenor(DateTime primera, DateTime segunda)
        {
            return DateTime.Compare(primera, segunda) < 0;
        }

        private bool tareaIniciaDespues(Tarea tarea)
        {
            return FechaEsMenor(this.FechaInicio, tarea.FechaInicio)
                || FechaEsIgual(this.FechaInicio, tarea.FechaInicio);
        }

        public bool AgregarSubtarea(Tarea subtarea)
        {
            if (Equals(subtarea))
                return false;
            if (tareaIniciaDespues(subtarea))
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

        private bool todasSubTareasFinalizadas()
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
            if (todasSubTareasFinalizadas())
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
            TareaCompuesta copia = new TareaCompuesta(Contexto)
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

        public override bool estaEnSubtareas(Tarea tarea)
        {
            TareaCompuesta tareaCompuesta = ((TareaCompuesta)this);
            if (tareaCompuesta.Subtareas.Contains(tarea))
            {
                return true;
            }
            foreach (Tarea tareaActual in tareaCompuesta.Subtareas)
            {
                return tareaActual.estaEnSubtareas(tarea);
            }
            return false;
        }

    }
}