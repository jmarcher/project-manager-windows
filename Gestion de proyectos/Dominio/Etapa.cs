using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Etapa
    {
        public int Id { get; set; }
        public List<Tarea> Tareas { get; set; }

        public Etapa()
        {
            Tareas = new List<Tarea>();
        }

        public override bool Equals(object obj)
        {
            Etapa e = (Etapa)obj;
            return e.Id == this.Id;
        }

        public int CalcularDuracion()
        {
            int SumaDuracion = 0;
            foreach (Tarea t in Tareas)
            {
                SumaDuracion += t.CalcularDuracion();
            }
            return SumaDuracion;
        }

        public void AgregarTarea(Tarea tarea)
        {
            Tareas.Add(tarea);
        }
    }
}