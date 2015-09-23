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

       
    
    }
}
