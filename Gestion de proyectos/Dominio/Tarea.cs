using Dominio.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Tarea
    {
        public const int PRIORIDAD_BAJA = 0;
        public const int PRIORIDAD_MEDIA = 1;
        public const int PRIORIDAD_ALTA = 2;

        public int DuracionPendiente { get; set; }
        public int Prioridad { get; set; }

        public String Nombre { get; set; }
        public String Objetivo { get; set; }
        public String Descripcion { get; private set; }
        
        
        public List<Tarea> Antecesoras { get; set; }
        public List<Tarea> Subtareas { get; private set; }

        private DateTime _FInicio;
        private DateTime _FFinalizacion;

        public bool Finalizada { get; private set; }

        public DateTime FInicio
        {
            get{
                return _FInicio;
            }
            set{
                if (FechaNula(_FFinalizacion)|| (!FechaNula(_FFinalizacion) && FechaEsMenor(value, _FFinalizacion))){
                    _FInicio = value;
                }else{
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public DateTime FFinalizacion
        {
            get{
                return _FFinalizacion;
            }
            set{
                if(FechaNula(_FInicio) || (!FechaNula(_FInicio) && FechaEsMenor(_FInicio,value))){
                      _FFinalizacion = value;
                }else{
                    throw new ArgumentOutOfRangeException();
                }
              
            }
        }

        public Tarea()
        {
            Antecesoras = new List<Tarea>();
            Subtareas = new List<Tarea>();
            Prioridad = PRIORIDAD_MEDIA;
            _FInicio = DateTime.MinValue;
            _FFinalizacion = DateTime.MinValue;
            Nombre = "[Nombre por defecto]";
            Finalizada = false;
        }
        public Tarea(String nombre, String objetivo, String descripcion, DateTime fechaI, DateTime fechaF, int duracion, String prioridad)
        {
            Antecesoras = new List<Tarea>();
            Subtareas = new List<Tarea>();
            DefinirPrioridad(prioridad);
            _FInicio = fechaI;
            _FFinalizacion = fechaF;
            Nombre = nombre;
            Objetivo = objetivo;
            Descripcion = descripcion;
            Finalizada = false;
            DuracionPendiente = duracion;
        }

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

        private int CalcularDuracionSubtareas()
        {
            int SumaDuracion = 0;
            foreach (Tarea t in Subtareas)
            {
                SumaDuracion += t.CalcularDuracion();
            }
            return SumaDuracion;
        }

        public int CalcularDuracion()
        {
            int SumaDuracion = DuracionPendiente;
            SumaDuracion += CalcularDuracionSubtareas();
            return SumaDuracion;
        }

        private bool TareaIniciaDespues(Tarea tarea)
        {
            return FechaEsMenor(this.FInicio, tarea.FInicio)
                || FechaEsIgual(this.FInicio, tarea.FInicio);
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
            Tarea t = (Tarea)obj;
            return t.Nombre.Equals(this.Nombre)
                && t.FInicio.Equals(this.FInicio)
                && t.Prioridad == this.Prioridad;
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
                Finalizada = true;
        }

        private bool TodasSubTareasFinalizadas()
        {
            bool retorno = true;
            foreach (Tarea t in Subtareas)
            {
                retorno = retorno && t.Finalizada;
                   
            }
            return retorno;
        }

        public override string ToString()
        {
            return Nombre + "["+Descripcion+"]";
        }
        
    }
}
