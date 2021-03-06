﻿using PersistenciaInterfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class TareaSimple : Tarea
    {
        [Required]
        private DateTime _FechaFinalizacion;
        public int DuracionPendiente { get; set; }
        

        public override DateTime FechaFinalizacion
        {
            get
            {
                return _FechaFinalizacion;
            }

            set
            {
                    _FechaFinalizacion = value;
            }

        }

        public override bool EstaAtrasada
        {
            get
            {
                return DateTime.Now.AddDays(DuracionPendiente) > FechaFinalizacion;
            }
        }

        public TareaSimple() : base() {
            _FechaFinalizacion = FECHA_NULA;
        }

        public TareaSimple(IContextoGestorProyectos contexto) : base(contexto)
        {
            _FechaFinalizacion = FECHA_NULA;
        }

        public override int CalcularDuracionPendiente()
        {
            return DuracionPendiente;
        }

        public override void MarcarFinalizada()
        {
            EstaFinalizada = true;
        }

        public override bool EstaEnSubtareas(Tarea tarea)
        {
            return false;
        }

        public override Tarea Clonar() 
        {
            TareaSimple copia = new TareaSimple(Contexto) { 
            Nombre = this.Nombre,
            Descripcion = this.Descripcion,
            Objetivo = this.Objetivo,
            Prioridad = this.Prioridad,
            EstaFinalizada = this.EstaFinalizada,
            FechaInicio = this.FechaInicio,
            FechaFinalizacion = this.FechaFinalizacion,
            DuracionPendiente = this.DuracionPendiente
            };
            return copia;
        }
    }
}
