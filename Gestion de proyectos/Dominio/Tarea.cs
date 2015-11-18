using DominioInterfaz;
using PersistenciaInterfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dominio
{
    public abstract class Tarea : IFechas, INombrable, IDuracionPendienteCalculable, IPersonificable, IDuracionEstimable, ITarea
    {
        public const int PRIORIDAD_BAJA = 0;
        public const int PRIORIDAD_MEDIA = 1;
        public const int PRIORIDAD_ALTA = 2;
        public static readonly DateTime FECHA_NULA = new DateTime(2010, 1, 1);

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TareaID { get; set; }
        public DateTime FechaModificada { get; set; }
        public int Prioridad { get; set; }

        public int DuracionEstimada { get; set; }

        public String Nombre { get; set; }
        public String Objetivo { get; set; }
        public String Descripcion { get; set; }
        public virtual List<Tarea> Antecesoras { get; set; }
        public virtual List<Persona> Personas { get; set; }
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
                _FechaInicio = value;
            }
        }
        public abstract DateTime FechaFinalizacion { get; set; }

        public IContextoGestorProyectos Contexto { get; set; }

        public String Historial { get; set; }

        public Tarea()
        {
            Antecesoras = new List<Tarea>();
            Personas = new List<Persona>();
            Prioridad = PRIORIDAD_MEDIA;
            _FechaInicio = FECHA_NULA;
            Nombre = "[Nombre por defecto]";
            Objetivo = "Objetivo";
            Descripcion = String.Empty;
            EstaFinalizada = false;
            FechaModificada = DateTime.Now.Date;
        }

        public Tarea(IContextoGestorProyectos contexto)
        {
            Antecesoras = new List<Tarea>();
            Personas = new List<Persona>();
            Prioridad = PRIORIDAD_MEDIA;
            _FechaInicio = FECHA_NULA;
            Nombre = "[Nombre por defecto]";
            Objetivo = "Objetivo";
            Descripcion = String.Empty;
            EstaFinalizada = false;
            FechaModificada = DateTime.Now.Date;
            Contexto = contexto;
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


        

        public bool FechaEsIgual(DateTime primera, DateTime segunda)
        {
            return primera.Day == segunda.Day &&
                primera.Month == segunda.Month &&
                primera.Year == segunda.Year;
        }

        public Proyecto ObtenerProyectoPadre()
        {
                foreach (Proyecto proyecto in Contexto.DevolverProyectos())
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
            valorRetorno.Append(FechaInicio.Date.ToShortDateString());
            valorRetorno.Append(", Fin: ");
            valorRetorno.Append(FechaFinalizacion.Date.ToShortDateString());
            valorRetorno.Append("]");
            return valorRetorno.ToString();
        }

        public string prioridadAString()
        {
            if (Prioridad == PRIORIDAD_ALTA)
                return "Alta";
            else if (Prioridad == PRIORIDAD_MEDIA)
                return "Media";
            else
                return "Baja";
        }


        public virtual bool estaEnSubtareas(Tarea tarea)
        {
            return false;
        }

        public void AgregarPersona(Persona persona)
        {
            Personas.Add(persona);
        }

        public void AgregarModificacion(string modificacion)
        {
            Historial += "["+DateTime.Now+"] " + modificacion+"\r\n";
        }

    }
}
