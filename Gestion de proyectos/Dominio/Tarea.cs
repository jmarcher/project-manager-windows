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
        public String Nombre { get; set; }
        public DateTime FInicio { get; set; }
        public DateTime FFinalizacion { get; set; }
        public int Prioridad { get; set; }
        public List<Tarea> Antecesoras { get; set; }
        public String Objetivo { get; set; }
        public String Descripcion { get; set; }
        public List<Tarea> Subtareas { get; set; }

        public Tarea()
        {
            Antecesoras = new List<Tarea>();
            Subtareas = new List<Tarea>();
            Prioridad = PRIORIDAD_MEDIA;
        }

        public int CalcularDuracion()
        {
            int SumaDuracion = DuracionPendiente;
            foreach (Tarea t in Subtareas)
            {
                SumaDuracion += t.CalcularDuracion();
            }
            return SumaDuracion;
        }

        public void AgregarSubtarea(Tarea subtarea)
        {
            Subtareas.Add(subtarea);
        }
    }
}
