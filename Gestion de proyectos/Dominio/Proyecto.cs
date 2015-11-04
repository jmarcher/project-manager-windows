using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proyecto : IFechas, INombrable, IDuracionPendienteCalculable, IPersonificable
    {
        public int Identificador { get; set; }
        public String Nombre { get; set; }
        public String Objetivo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion
        {
            get
            {
                return UltimaFechaDeEtapa();
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
        public List<Etapa> Etapas { get; set; }
        public List<Persona> Personas { get; set; }

        public Proyecto()
        {
            Etapas = new List<Etapa>();
            Personas = new List<Persona>();
            FechaInicio = Tarea.FECHA_NULA;
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
            return proyecto.Identificador == this.Identificador;
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
            if (TodasEtapasFinalizadas())
                EstaFinalizado = true;
        }
        private bool TodasEtapasFinalizadas()
        {
            bool valorRetorno = true;
            foreach (Etapa etapa in Etapas)
            {
                valorRetorno = valorRetorno && etapa.EstaFinalizada;

            }
            return valorRetorno;
        }

        private DateTime UltimaFechaDeEtapa()
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
