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
        public DateTime fechaFinalizado { get; set; }
        public bool Finalizado { get; private set; }
        public List<Etapa> Etapas{get; set;}

        public Proyecto()
        {
            Etapas = new List<Etapa>();
        }
        public void AgregarEtapa(Etapa etapa)
        {
            Etapas.Add(etapa);
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

       
       
    
    }
}
