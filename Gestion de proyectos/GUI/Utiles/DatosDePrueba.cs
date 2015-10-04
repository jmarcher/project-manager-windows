using Dominio;
using System;
using System.Collections.Generic;

namespace InterfazGrafica.Utiles
{
    public class DatosDePrueba
    {
        private Random aleatorio;
        private List<Proyecto> Proyectos { get; set; }
        public DatosDePrueba()
        {
            Proyectos = new List<Proyecto>();
            aleatorio = new Random(DateTime.Now.Millisecond);
        }

        public List<Proyecto> ObtenerUnaListaProyectos()
        {
            for (int i = 0; i < aleatorio.Next(30, 35); i++)
            {
                Proyectos.Add(CrearProyectoAleatorio());
            }
            return Proyectos;
        }

        private Tarea CrearTareaAleatoria(DateTime fecha, bool agregarSubTareas = false)
        {
            TareaCompuesta tarea = new TareaCompuesta()
            {
                Nombre = "Tarea Nro: "+aleatorio.Next(1,3000),
                FechaInicio =fecha.CompareTo(DateTime.Now)==0 ? fecha : fecha.AddDays(aleatorio.Next(1,30)),
                Objetivo = "Objetivo",
                Prioridad = aleatorio.Next(0,2),
                
            };
            if(agregarSubTareas){
                GenerarSubtareas(aleatorio, tarea);
            }
            return tarea;
        }

        private void GenerarSubtareas(Random aleatorio, TareaCompuesta tarea)
        {
            for (int i = 0; i < aleatorio.Next(0, 5); i++)
            {
                bool agregarMas = false;
                if (aleatorio.Next(1, 10) > 8) agregarMas = true;
                tarea.AgregarSubtarea(CrearTareaAleatoria(tarea.FechaInicio, agregarMas));
            }
        }

        private Etapa CrearEtapaAleatoria()
        {
            Etapa etapa = new Etapa()
            {
                Nombre = "Etapa Nro: "+aleatorio.Next(1,3000),
                Identificacion = GenerarIdUnico(),
                
            };

            AgregarTareasAEtapa(aleatorio, etapa);
            return etapa;
        }

        private void AgregarTareasAEtapa(Random aleatorio, Etapa etapa)
        {
            for (int i = 0; i < aleatorio.Next(1, 25); i++)
            {
                etapa.AgregarTarea(CrearTareaAleatoria(DateTime.Now,true));
            }
        }

        private int GenerarIdUnico()
        {
            int Numero = (int)((aleatorio.Next(444, 0983098093) / 10) % 10000);
            return Numero;
        }

        private Proyecto CrearProyectoAleatorio()
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = "Proyecto Nro: "+aleatorio.Next(1,3000),
                Objetivo = "Objetivo Nro: "+aleatorio.Next(1,35),
                Identificador = GenerarIdUnico()
            };

            AgregarEtapasAProyecto(aleatorio, proyecto);
            return proyecto;
        }

        private void AgregarEtapasAProyecto(Random aleatorio, Proyecto proyecto)
        {
            for (int i = 0; i < aleatorio.Next(4, 30); i++)
            {
                proyecto.AgregarEtapa(CrearEtapaAleatoria());
            }
        }
    }
}
