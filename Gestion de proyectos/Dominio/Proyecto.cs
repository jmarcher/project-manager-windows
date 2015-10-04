using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proyecto
    {
        public const string ESPACIO = " ";
        public int Identificador { get; set; }
        public int Duracion { get; set; }

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

        public Proyecto()
        {
            Etapas = new List<Etapa>();
        }

        public void AgregarEtapa(Etapa etapa)
        {
            Etapas.Add(etapa);
        }

        public void QuitarEtapa(Etapa etapa)
        {
            Etapas.Remove(etapa);
        }

        public bool PerteneceEtapa(Etapa etapa)
        {
            return Etapas.Contains(etapa);
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

        public DateTime ObtenerFechaFinalizacion()
        {

            DateTime fechaRetorno = new DateTime();
            foreach (Etapa etapa in Etapas)
            {
                if (DateTime.Compare(etapa.FechaFinalizacion, fechaRetorno) > 0)
                {
                    fechaRetorno = etapa.FechaFinalizacion;
                }
            }
            return fechaRetorno;
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Nombre);
            sb.Append(ESPACIO);
            sb.Append(this.Objetivo);
            sb.Append(ESPACIO);
            sb.Append(this.EstaFinalizado);
            return sb.ToString();
        }


    }
}
