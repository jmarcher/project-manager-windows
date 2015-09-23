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
        private List<Proyecto> Proyectos { get; set; }
        public DatosDePrueba()
        {
            Proyectos = new List<Proyecto>();
        }

        public List<Proyecto> ObtenerUnaListaProyectos()
        {
            Random r = new Random();
            for (int i = 0; i < r.Next(30, 35); i++)
            {
                Proyectos.Add(CrearProyectoAleatorio());
            }
            return Proyectos;
        }

        private Tarea CrearTareaAleatoria(DateTime fecha, bool agregarSubTareas = false)
        {
            Random r = new Random();
            Tarea t = new Tarea()
            {
                Nombre = "Tarea Nro: "+r.Next(1,3000),
                FInicio =fecha.CompareTo(DateTime.Now)==0 ? fecha : fecha.AddDays(r.Next(1,30)),
                Objetivo = "Objetivo",
                Prioridad = r.Next(0,2),
                
            };
            if(agregarSubTareas){
                GenerarSubtareas(r, t);
            }
            return t;
        }

        private void GenerarSubtareas(Random r, Tarea t)
        {
            for (int i = 0; i < r.Next(0, 5); i++)
            {
                bool agregarMas = false;
                if (r.Next(1, 10) > 8) agregarMas = true;
                t.AgregarSubtarea(CrearTareaAleatoria(t.FInicio, agregarMas));
            }
        }

        private Etapa CrearEtapaAleatoria()
        {
            Random r = new Random();
            Etapa e = new Etapa()
            {
                Nombre = "Etapa Nro: "+r.Next(1,3000),
                Id = GenerarIdUnico(),
                
            };

            AgregarTareasAEtapa(r, e);
            return e;
        }

        private void AgregarTareasAEtapa(Random r, Etapa e)
        {
            for (int i = 0; i < r.Next(1, 25); i++)
            {
                e.AgregarTarea(CrearTareaAleatoria(DateTime.Now,true));
            }
        }

        private int GenerarIdUnico()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            int Numero = (int)((DateTime.Now.Ticks / 10) % 1000000000);

            return Numero;
        }

        private Proyecto CrearProyectoAleatorio()
        {
            Random r = new Random();
            Proyecto p = new Proyecto()
            {
                Nombre = "Proyecto Nro: "+r.Next(1,3000),
                Objetivo = "Objetivo Nro: "+r.Next(1,35),
                Id = GenerarIdUnico()
            };

            AgregarEtapasAProyecto(r, p);
            return p;
        }

        private void AgregarEtapasAProyecto(Random r, Proyecto p)
        {
            for (int i = 0; i < r.Next(4, 30); i++)
            {
                p.AgregarEtapa(CrearEtapaAleatoria());
            }
        }
    }
}
