using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Etapa
    {
        public String Nombre { get; set; }
        public int Identificacion { get; set; }
        public int DuracionPendiente { get; set; }
        public List<Tarea> Tareas { get; set; }
        public bool EstaFinalizada { get; private set; }
        public DateTime FechaFinalizacion { get; private set; }
        public DateTime FechaInicio { get;set; }
        public Etapa()
        {
            Nombre = "[Nombre por defecto]";
            EstaFinalizada = false;
            Tareas = new List<Tarea>();
        }
        

        public override bool Equals(object obj)
        {
            Etapa etapa = (Etapa)obj;
            return etapa.Identificacion == this.Identificacion;
        }

        public int CalcularDuracion()
        {
            int SumaDuracion = 0;
            foreach (Tarea tarea in Tareas)
            {
                SumaDuracion += tarea.CalcularDuracion();
            }
            return SumaDuracion;
        }

        public void AgregarTarea(Tarea tarea)
        {
            Tareas.Add(tarea);
        }

        public DateTime ObtenerFechaFinalizacion()
        {
            DateTime fechaRetorno = new DateTime();
            foreach(Tarea tarea in Tareas)
            {
                DateTime fechaActual = tarea.FechaInicio;
                fechaActual = fechaActual.AddDays(tarea.DuracionPendiente);

                if (DateTime.Compare(fechaActual, fechaRetorno) > 0)
                    fechaRetorno = fechaActual;
            }
           
            return fechaRetorno;
        }
        public void MarcarFinalizada() {
            if (TodasTareasFinalizadas())
            {
                EstaFinalizada = true;
                FechaFinalizacion = DateTime.Now;
            }
        }
        private bool TodasTareasFinalizadas()
        {
            bool valorRetorno = true;
            foreach (Tarea tarea in Tareas)
            {
                valorRetorno = valorRetorno && tarea.EstaFinalizada;

            }
            return valorRetorno;
        }
        public void InsertarFechaFinalizacion()
        {
            FechaFinalizacion = ObtenerFechaFinalizacion();
        }
        public void InsertarDuracion()
        {
            DuracionPendiente = CalcularDuracion();
        }
    }
}
