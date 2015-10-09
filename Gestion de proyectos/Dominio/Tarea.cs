﻿using Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Tarea : IFechas, INombrable, IDuracionPendienteCalculable
    {
        public const int PRIORIDAD_BAJA = 0;
        public const int PRIORIDAD_MEDIA = 1;
        public const int PRIORIDAD_ALTA = 2;
        public static readonly DateTime FECHA_NULA = new DateTime(2001, 1, 1);

        public int Prioridad { get; set; }

        public String Nombre { get; set; }
        public String Objetivo { get; set; }
        public String Descripcion { get; set; }
        
        
        public List<Tarea> Antecesoras { get; set; }

        private DateTime _FechaInicio;

        public bool EstaFinalizada { get; protected set; }

        public abstract bool EstaAtrasada { get; }

        public DateTime FechaInicio
        {
            get{
                return _FechaInicio;
            }
            set{
                if (FechaNula(FechaFinalizacion)|| (!FechaNula(FechaFinalizacion) && 
                    (FechaEsMenor(value, FechaFinalizacion)  || FechaEsIgual(value, FechaFinalizacion)))){
                    _FechaInicio = value;
                }else{
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public abstract DateTime FechaFinalizacion{get;set;}

        public Tarea()
        {
            Antecesoras = new List<Tarea>();
            Prioridad = PRIORIDAD_MEDIA;
            _FechaInicio = FECHA_NULA;
            Nombre = "[Nombre por defecto]";
            Objetivo = "Objetivo";
            EstaFinalizada = false;
        }

        public abstract int CalcularDuracionPendiente();

        public abstract void MarcarFinalizada();

        public void DefinirPrioridad(String prioridad)
        {
            if (prioridad.Equals("Alta"))
            {
                Prioridad = PRIORIDAD_ALTA;
            }
            else if (prioridad.Equals("Media"))
            {
                Prioridad = PRIORIDAD_MEDIA;
            }
            else if (prioridad.Equals("Baja"))
            {
                Prioridad = PRIORIDAD_BAJA;
            }
        }


        public bool AgregarAntecesora(Tarea antecesora)
        {
            if (antecesora.Equals(this))
                return false;
            if (antecesora.FechaFinalizacion > FechaInicio)
                return false;
            Antecesoras.Add(antecesora);
            return true;
        }

        public override bool Equals(object obj)
        {
            Tarea tarea = (Tarea)obj;
            return tarea.Nombre.Equals(this.Nombre)
                && tarea.Objetivo.Equals(Objetivo)
                && tarea.FechaEsIgual(tarea.FechaInicio,this.FechaInicio)
                && tarea.Prioridad == this.Prioridad;
        }

        public Tarea UltimaAntecesora()
        {
            Tarea antecesoraMasGrande = null;
            DateTime mayorFecha = DateTime.MinValue;
            foreach (Tarea tarea in Antecesoras)
            {
                if (tarea.FechaFinalizacion > mayorFecha)
                {
                    mayorFecha = tarea.FechaFinalizacion;
                    antecesoraMasGrande = tarea;
                }
            }
            return antecesoraMasGrande;
        }

        public bool FechaNula(DateTime fecha)
        {
            return FechaEsIgual(FECHA_NULA, fecha);
        }

        public bool FechaEsMenor(DateTime a, DateTime b)
        {
            return DateTime.Compare(a, b) < 0;
        }

        public bool FechaEsIgual(DateTime a, DateTime b)
        {
            return a.Day == b.Day &&
                a.Month == b.Month &&
                a.Year == b.Year;
        }

        public override string ToString()
        {
            StringBuilder valorRetorno = new StringBuilder();
            valorRetorno.Append(Nombre);
            valorRetorno.Append(" [Prioridad: ");
            valorRetorno.Append(Prioridad);
            valorRetorno.Append(", Inicio: ");
            valorRetorno.Append(FechaInicio.Date.ToString());
            valorRetorno.Append(", Fin: ");
            valorRetorno.Append(FechaFinalizacion.Date.ToString());
            valorRetorno.Append("]");
            return valorRetorno.ToString();
        }

        
    }
}
