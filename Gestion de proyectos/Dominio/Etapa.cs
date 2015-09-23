using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Etapa
    {
        public String Nombre { get; set; }
        public int Id { get; set; }
        public List<Tarea> Tareas { get; set; }
        public bool Finalizada { get; private set; }

        public Etapa()
        {
            Nombre = "[Nombre por defecto]";
            Finalizada = false;
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

        public DateTime ObtenerFechaFinalizacion()
        {
            DateTime fecha = Tareas.First().FInicio;
            fecha = fecha.AddDays(Tareas.First().DuracionPendiente);
            foreach(Tarea t in Tareas)
            {
                DateTime fechaActual = t.FInicio;
                fechaActual = fechaActual.AddDays(t.DuracionPendiente);

                if (DateTime.Compare(fechaActual, fecha) > 0)
                    fecha = fechaActual;
            }
           
            return fecha;
        }
        public void MarcarFinalizada() {
            if (TodasTareasFinalizadas())
                Finalizada = true;
        }
        private bool TodasTareasFinalizadas()
        {
            bool retorno = true;
            foreach (Tarea t in Tareas)
            {
                retorno = retorno && t.Finalizada;

            }
            return retorno;
        }    
    
    }
}
