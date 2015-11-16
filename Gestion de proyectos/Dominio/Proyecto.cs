using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    [Table("Proyectos")]
    public class Proyecto : IFechas, INombrable, IDuracionPendienteCalculable, IPersonificable, IDuracionEstimable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProyectoID { get; set; }
        public String Nombre { get; set; }
        public String Objetivo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion
        {
            get
            {
                return ultimaFechaDeEtapa();
            }
        }



        public bool EstaFinalizado { get; private set; }
        public bool EstaAtrasado
        {
            get
            {
                foreach (Etapa etapa in Etapas)
                {
                    if (etapa.EstaAtrasada)
                        return true;
                }
                return false;
            }
        }
        public virtual List<Etapa> Etapas { get; set; }
        public virtual List<Persona> Personas { get; set; }

        public int DuracionEstimada {get;set;}

        public IContextoGestorProyectos Contexto { get; set; }

        public String Historial { get; set; }


        public void AgregarModificacion(String cambio)
        {
            Historial += "[" + DateTime.Now + "] "+ cambio+" \r\n";
        }

        public Proyecto()
        {
            Etapas = new List<Etapa>();
            Personas = new List<Persona>();
            FechaInicio = Tarea.FECHA_NULA;
        }

        public Proyecto(IContextoGestorProyectos contexto)
        {
            Etapas = new List<Etapa>();
            Personas = new List<Persona>();
            FechaInicio = Tarea.FECHA_NULA;
            this.Contexto = contexto;
        }

        public void AgregarEtapa(Etapa etapa)
        {
            Etapas.Add(etapa);
        }

        public void QuitarEtapa(Etapa etapa)
        {
            Etapas.Remove(etapa);
        }

        public Boolean ContieneEtapa(Etapa etapa)
        {
            return Etapas.Contains(etapa);
        }

        public override bool Equals(object obj)
        {
            Proyecto proyecto = (Proyecto)obj;
            return proyecto.ProyectoID == this.ProyectoID;
        }

        public int CalcularDuracionPendiente()
        {
            int mayorDuracionPendiente = 0;
            DateTime mayorFecha = DateTime.MinValue;
            foreach (Etapa etapa in Etapas)
            {
                if (etapa.FechaFinalizacion > mayorFecha)
                {
                    mayorFecha = etapa.FechaFinalizacion;
                    mayorDuracionPendiente = etapa.CalcularDuracionPendiente();
                }
            }
            return mayorDuracionPendiente;
        }

        public void MarcarFinalizado()
        {
            if (todasEtapasFinalizadas())
                EstaFinalizado = true;
        }
        private bool todasEtapasFinalizadas()
        {
            bool valorRetorno = true;
            foreach (Etapa etapa in Etapas)
            {
                valorRetorno = valorRetorno && etapa.EstaFinalizada;

            }
            return valorRetorno;
        }

        private DateTime ultimaFechaDeEtapa()
        {
            DateTime mayorFecha = DateTime.MinValue;
            foreach (Etapa etapa in Etapas)
            {
                if (etapa.FechaFinalizacion > mayorFecha)
                    mayorFecha = etapa.FechaFinalizacion;
            }
            return mayorFecha;
        }

        public void AgregarPersona(Persona persona)
        {
            Personas.Add(persona);
        }

    }
}
