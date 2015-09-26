using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Proyecto
    {
        public static string Espacio = " ";
        public int Id { get; set; }

        public String Nombre { get; set; }
        public String Objetivo { get; set; }
        public DateTime FechaFinalizado { get; set; }
        public bool Finalizado { get; private set; }
        public List<Etapa> Etapas{get; set;}

        public Proyecto()
        {
            Etapas = new List<Etapa>();
        }
        public Proyecto(String nombre,String objetivo)
        {
            Nombre = nombre;
            Objetivo = objetivo;
            FechaFinalizado = DateTime.MinValue;
            Etapas = new List<Etapa>();
            Finalizado = false;

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
            Proyecto p = (Proyecto)obj;
            return p.Id == this.Id;
        }

        public int CalcularDuracion()
        {
            int sumaDuracion = 0;
            foreach (Etapa e in Etapas)
            {
                sumaDuracion += e.CalcularDuracion();
            }
            return sumaDuracion;
        }

        public DateTime ObtenerFechaFinalizacion()
        {

            DateTime fecha = new DateTime();
            foreach (Etapa e in Etapas)
            {
                if (DateTime.Compare(e.ObtenerFechaFinalizacion(), fecha) > 0)
                {
                    fecha = e.ObtenerFechaFinalizacion();
                }
            }
            return fecha;
        }

        public void MarcarFinalizado()
        {
            if (TodasEtapasFinalizadas())
                Finalizado = true;
            FechaFinalizado = ObtenerFechaFinalizacion();
        }
        private bool TodasEtapasFinalizadas()
        {
            bool retorno = true;
            foreach (Etapa e in Etapas)
            {
                retorno = retorno && e.Finalizada;

            }
            return retorno;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Nombre);
            sb.Append(Espacio);
            sb.Append(this.Objetivo);
            sb.Append(Espacio);
            sb.Append(this.Finalizado);
            return sb.ToString();
        }
       
    
    }
}
 