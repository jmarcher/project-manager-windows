using Dominio.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Tarea
    {
        public const int PRIORIDAD_BAJA = 0;
        public const int PRIORIDAD_MEDIA = 1;
        public const int PRIORIDAD_ALTA = 2;

        public int Prioridad { get; set; }

        public String Nombre { get; set; }
        public String Objetivo { get; set; }
        public String Descripcion { get; set; }
        
        
        public List<Tarea> Antecesoras { get; set; }
        public List<Tarea> Subtareas { get; private set; }

        private DateTime _FechaInicio;
        private DateTime _FechaFinalizacion;

        public bool EstaFinalizada { get; private set; }

        public DateTime FechaInicio
        {
            get{
                return _FechaInicio;
            }
            set{
                if (FechaNula(_FechaFinalizacion)|| (!FechaNula(_FechaFinalizacion) && FechaEsMenor(value, _FechaFinalizacion))){
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
            Subtareas = new List<Tarea>();
            Prioridad = PRIORIDAD_MEDIA;
            _FechaInicio = DateTime.MinValue;
            _FechaFinalizacion = DateTime.MinValue;
            Nombre = "[Nombre por defecto]";
            EstaFinalizada = false;
        }

        public abstract int CalcularDuracionPendiente();

        private void DefinirPrioridad(String prioridad)
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

        private bool TareaIniciaDespues(Tarea tarea)
        {
            return FechaEsMenor(this.FechaInicio, tarea.FechaInicio)
                || FechaEsIgual(this.FechaInicio, tarea.FechaInicio);
        }

        public bool AgregarSubtarea(Tarea subtarea)
        {
            if (this.Equals(subtarea))
                return false;
            if (TareaIniciaDespues(subtarea))
                Subtareas.Add(subtarea);
            else
                throw new FechaInvalida();
            return true;
        }

        public bool AgregarAntecesora(Tarea antecesora)
        {
            if (antecesora == this)
                return false;
            Antecesoras.Add(antecesora);
            return true;
        }



        public override bool Equals(object obj)
        {
            if (Convert.IsDBNull(obj))
                return false;
            Tarea tarea = (Tarea)obj;
            return tarea.Nombre.Equals(this.Nombre)
                && tarea.FechaInicio.Equals(this.FechaInicio)
                && tarea.Prioridad == this.Prioridad;
        }

        public bool FechaNula(DateTime fecha)
        {
            return FechaEsIgual(DateTime.MinValue, fecha);
        }

        public bool FechaEsMenor(DateTime a, DateTime b)
        {
            return DateTime.Compare(a, b) < 0;
        }

        public bool FechaEsIgual(DateTime a, DateTime b)
        {
            return DateTime.Compare(a, b) == 0;
        }

        public void MarcarFinalizada()
        {
            if (TodasSubTareasFinalizadas())
                EstaFinalizada = true;
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

        public override string ToString()
        {
            return Nombre + "["+Descripcion+"]";
        }
        
    }
}
