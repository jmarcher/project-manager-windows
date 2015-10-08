using Dominio;
using System;
using System.Collections.Generic;

namespace InterfazGrafica.Utiles
{
    public class DatosDePrueba2
    {
        private Random aleatorio;
        private List<Proyecto> Proyectos { get; set; }
        public DatosDePrueba2()
        {
            Proyectos = new List<Proyecto>();
            aleatorio = new Random(DateTime.Now.Millisecond);
        }

        public List<Proyecto> ObtenerUnaListaProyectos()
        {
            for (int i = 0; i < 5; i++)
            {
                Proyectos.Add(CrearProyectoAleatorio(i));
            }
            return Proyectos;
        }

        private Tarea CrearTareaAleatoria(DateTime fecha, bool agregarSubTareas = false)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea Nro: "+aleatorio.Next(1,3000),
                FechaInicio =fecha.CompareTo(DateTime.Now)==0 ? fecha : fecha.AddDays(aleatorio.Next(1,30)),
                
                Objetivo = "Objetivo",
                Prioridad = aleatorio.Next(0,2),
                
            };
            tarea.FechaFinalizacion = tarea.FechaInicio.CompareTo(DateTime.Now) == 0 ?
                tarea.FechaInicio : tarea.FechaInicio.AddDays(aleatorio.Next(1, 30));
            if (agregarSubTareas){
                tarea = new TareaCompuesta(tarea);
                GenerarSubtareas(aleatorio,(TareaCompuesta) tarea);
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

        private Etapa CrearEtapaAleatoria(int i)
        {
            Etapa etapa = new Etapa()
            {
                Nombre = "Etapa Nro: "+i,
                Identificacion = GenerarIdUnico(),
                
            };

            AgregarTareasAEtapa(aleatorio, etapa);
            return etapa;
        }

        private void AgregarTareasAEtapa(Random aleatorio, Etapa etapa)
        {
            Tarea tarea1 = new TareaSimple()
            {
                Nombre = "Tarea: " + aleatorio.Next(1, 5),
                Objetivo = "Objetivo: " + aleatorio.Next(1, 5),
                Descripcion = "Descripcion: " + aleatorio.Next(1, 5),
                DuracionPendiente = aleatorio.Next(5, 10),
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(aleatorio.Next(10, 15))
            };

            Tarea tarea2 = new TareaSimple()
            {
                Nombre = "Tarea: " + aleatorio.Next(1, 5),
                Objetivo = "Objetivo: " + aleatorio.Next(1, 5),
                Descripcion = "Descripcion: " + aleatorio.Next(1, 5),
                DuracionPendiente = aleatorio.Next(5, 10),
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(aleatorio.Next(10, 15))
            };

            Tarea tarea3 = new TareaSimple()
            {
                Nombre = "Tarea: " + aleatorio.Next(1, 5),
                Objetivo = "Objetivo: " + aleatorio.Next(1, 5),
                Descripcion = "Descripcion: " + aleatorio.Next(1, 5),
                DuracionPendiente = aleatorio.Next(5, 10),
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(aleatorio.Next(10, 15))
            };

            Tarea tareaAntecesora = new TareaCompuesta()
            {
                Nombre = "Tarea antecesora: " + aleatorio.Next(1, 5),
                Objetivo = "Objetivo: " + aleatorio.Next(1, 5),
                Descripcion = "Descripcion: " + aleatorio.Next(1, 5),
                FechaInicio = DateTime.Now.AddDays(-200)
            };

            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea compuesta " + aleatorio.Next(1, 5),
                FechaInicio = DateTime.Now.AddDays(-1),
                Descripcion= "Algo " + aleatorio.Next(1, 5),
                Objetivo = "Objetivo " + aleatorio.Next(1, 5)
            };
            tareaCompuesta.AgregarAntecesora(tareaAntecesora);
			TareaCompuesta otraTareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea compuesta de compuesta " + aleatorio.Next(1, 5),
                FechaInicio = DateTime.Now,
                Descripcion= "Algo " + aleatorio.Next(1, 5),
                Objetivo = "Objetivo " + aleatorio.Next(1, 5)
            };
			otraTareaCompuesta.AgregarSubtarea(tarea1);
            otraTareaCompuesta.AgregarSubtarea(tarea2);
            tareaCompuesta.AgregarSubtarea(tarea1);
            tareaCompuesta.AgregarSubtarea(tarea2);
            tareaCompuesta.AgregarSubtarea(tarea3);
			tareaCompuesta.AgregarSubtarea(otraTareaCompuesta);

            Tarea tarea4 = new TareaSimple()
            {
                Nombre = "Tarea: " + aleatorio.Next(1, 5),
                Objetivo = "Objetivo: " + aleatorio.Next(1, 5),
                Descripcion = "Descripcion: " + aleatorio.Next(1, 5),
                DuracionPendiente = aleatorio.Next(5, 10),
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(aleatorio.Next(10, 15))
            };
            etapa.AgregarTarea(tareaCompuesta);
            etapa.AgregarTarea(tarea4);
            etapa.AgregarTarea(tarea2);

        }

        private int GenerarIdUnico()
        {
            int Numero = (int)((aleatorio.Next(444, 0983098093) / 10) % 10000);
            return Numero;
        }

        private Proyecto CrearProyectoAleatorio(int i)
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = "Proyecto Nro: "+i,
                Objetivo = "Objetivo Nro: "+i,
                Identificador = GenerarIdUnico()
            };

            AgregarEtapasAProyecto(aleatorio, proyecto);
            return proyecto;
        }

        private void AgregarEtapasAProyecto(Random aleatorio, Proyecto proyecto)
        {
            for (int i = 0; i < 3; i++)
            {
                proyecto.AgregarEtapa(CrearEtapaAleatoria(i));
            }
        }
    }
}
