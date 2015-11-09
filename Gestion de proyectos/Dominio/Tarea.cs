using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio
{
    public abstract class Tarea : IFechas, INombrable, IDuracionPendienteCalculable,IPersonificable
    {
        public const int PRIORIDAD_BAJA = 0;
        public const int PRIORIDAD_MEDIA = 1;
        public const int PRIORIDAD_ALTA = 2;
        public static readonly DateTime FECHA_NULA = new DateTime(2001, 1, 1);

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TareaID { get; set; }
        
        [Index]
        public DateTime FechaModificada { get; set; }
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
            get
            {
                return _FechaInicio;
            }
            set
            {
                if (FechaNula(FechaFinalizacion) || (!FechaNula(FechaFinalizacion) &&
                    (FechaEsMenor(value, FechaFinalizacion) || FechaEsIgual(value, FechaFinalizacion))))
                {
                    _FechaInicio = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public abstract DateTime FechaFinalizacion { get; set; }

        public List<Persona> Personas
        {            get;
            set;
        }

        public Tarea()
        {
            Antecesoras = new List<Tarea>();
            Personas = new List<Persona>();
            Prioridad = PRIORIDAD_MEDIA;
            _FechaInicio = FECHA_NULA;
            Nombre = "[Nombre por defecto]";
            Objetivo = "Objetivo";
            EstaFinalizada = false;
            FechaModificada = DateTime.Now.Date;
        }

        public abstract int CalcularDuracionPendiente();
        public abstract Tarea Clonar();
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
            if (Convert.IsDBNull(obj))
                return false;
            Tarea tarea = (Tarea)obj;
            if(TareaID == 0)
                return tarea.Nombre.Equals(this.Nombre)
                    && tarea.Objetivo.Equals(Objetivo)
                    && tarea.FechaEsIgual(tarea.FechaInicio, this.FechaInicio)
                    && tarea.Prioridad == this.Prioridad;
            else
                return tarea.TareaID == TareaID;
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

        public bool FechaEsMenor(DateTime primera, DateTime segunda)
        {
            return DateTime.Compare(primera, segunda) < 0;
        }

        public bool FechaEsIgual(DateTime primera, DateTime segunda)
        {
            return primera.Day == segunda.Day &&
                primera.Month == segunda.Month &&
                primera.Year == segunda.Year;
        }

        public Proyecto ObtenerProyectoPadre()
        {
            foreach (Proyecto proyecto in InstanciaUnica.Instancia.DevolverProyectos())
            {
                foreach (Etapa etapa in proyecto.Etapas)
                {
                    foreach (Tarea tarea in etapa.Tareas)
                    {

                        if (tarea.Equals(this) || estaEnSubtareas(tarea))
                        {
                            return proyecto;
                        }
                    }
                }
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder valorRetorno = new StringBuilder();
            valorRetorno.Append(Nombre);
            valorRetorno.Append(" [Prioridad: ");
            valorRetorno.Append(prioridadAString());
            valorRetorno.Append(", Duración pendiente: ");
            valorRetorno.Append(CalcularDuracionPendiente().ToString());
            valorRetorno.Append(", Inicio: ");
            valorRetorno.Append(FechaInicio.Date.ToString());
            valorRetorno.Append(", Fin: ");
            valorRetorno.Append(FechaFinalizacion.Date.ToString());
            valorRetorno.Append("]");
            return valorRetorno.ToString();
        }

        private string prioridadAString()
        {
            if (Prioridad == PRIORIDAD_ALTA)
                return "Alta";
            else if (Prioridad == PRIORIDAD_MEDIA)
                return "Media";
            else
                return "Baja";
        }


        public bool estaEnSubtareas(Tarea tarea)
        {
            if (this.GetType() == typeof(TareaCompuesta))
            {
                TareaCompuesta tareaCompuesta = ((TareaCompuesta)this);
                if (tareaCompuesta.Subtareas.Contains(tarea))
                {
                    return true;
                }
                else
                {
                    foreach (Tarea tareaActual in tareaCompuesta.Subtareas)
                    {
                        return tareaActual.estaEnSubtareas(tarea);
                    }
                }
            }
            return false;
        }

        public void AgregarPersona(Persona persona)
        {
            Personas.Add(persona);
        }
    }
}
