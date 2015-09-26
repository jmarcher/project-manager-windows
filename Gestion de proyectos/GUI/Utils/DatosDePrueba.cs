using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Utils
{
    public class DatosDePrueba
    {
        private Random random;
        private List<Proyecto> Proyectos { get; set; }
        public DatosDePrueba()
        {
            Proyectos = new List<Proyecto>();
            random = new Random(DateTime.Now.Millisecond);
        }

        public List<Proyecto> ObtenerUnaListaProyectos()
        {
            for (int i = 0; i < random.Next(30, 35); i++)
            {
                Proyectos.Add(CrearProyectoAleatorio());
            }
            return Proyectos;
        }

        private Tarea CrearTareaAleatoria(DateTime fecha, bool agregarSubTareas = false)
        {
            Tarea tarea = new Tarea()
            {
                Nombre = "Tarea Nro: "+random.Next(1,3000),
                FInicio =fecha.CompareTo(DateTime.Now)==0 ? fecha : fecha.AddDays(random.Next(1,30)),
                Objetivo = "Objetivo",
                Prioridad = random.Next(0,2),
                
            };
            if(agregarSubTareas){
                GenerarSubtareas(random, tarea);
            }
            return tarea;
        }

        private void GenerarSubtareas(Random random, Tarea tarea)
        {
            for (int i = 0; i < random.Next(0, 5); i++)
            {
                bool agregarMas = false;
                if (random.Next(1, 10) > 8) agregarMas = true;
                tarea.AgregarSubtarea(CrearTareaAleatoria(tarea.FInicio, agregarMas));
            }
        }

        private Etapa CrearEtapaAleatoria()
        {
            Etapa etapa = new Etapa()
            {
                Nombre = "Etapa Nro: "+random.Next(1,3000),
                Id = GenerarIdUnico(),
                
            };

            AgregarTareasAEtapa(random, etapa);
            return etapa;
        }

        private void AgregarTareasAEtapa(Random random, Etapa etapa)
        {
            for (int i = 0; i < random.Next(1, 25); i++)
            {
                etapa.AgregarTarea(CrearTareaAleatoria(DateTime.Now,true));
            }
        }

        private int GenerarIdUnico()
        {
            int Numero = (int)((random.Next(444, 0983098093) / 10) % 10000);
            return Numero;
        }

        private Proyecto CrearProyectoAleatorio()
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = "Proyecto Nro: "+random.Next(1,3000),
                Objetivo = "Objetivo Nro: "+random.Next(1,35),
                Id = GenerarIdUnico()
            };

            AgregarEtapasAProyecto(random, proyecto);
            return proyecto;
        }

        private void AgregarEtapasAProyecto(Random random, Proyecto proyecto)
        {
            for (int i = 0; i < random.Next(4, 30); i++)
            {
                proyecto.AgregarEtapa(CrearEtapaAleatoria());
            }
        }
    }
}
