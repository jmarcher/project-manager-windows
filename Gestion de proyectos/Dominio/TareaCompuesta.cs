﻿using Dominio.Excepciones;
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
            Nombre = tareaSimple.Nombre;
            Prioridad = tareaSimple.Prioridad;
            Objetivo = tareaSimple.Objetivo;
            FechaInicio = tareaSimple.FechaInicio;
            this.Descripcion = tareaSimple.Descripcion;
        }

        private DateTime FechaMayorDeSubtarea()
        {
            DateTime fechaRetorno = DateTime.MinValue;
            if (Subtareas.Count == 0)
                return FECHA_NULA;
            foreach (Tarea tarea in Subtareas)
            {
                if (tarea.FechaFinalizacion > fechaRetorno)
                    fechaRetorno = tarea.FechaFinalizacion;
            }
            return fechaRetorno;
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
            int valorRetorno = 0;
            foreach (Tarea tarea in Subtareas)
            {
                valorRetorno += tarea.CalcularDuracionPendiente();
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
    }
}