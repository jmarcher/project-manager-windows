using System;
using System.Data.Entity;
using Dominio;
using System.Linq;
using System.Collections.Generic;

namespace PersistenciaImp
{
    public class ContextoGestorProyectos : DbContext, Dominio.IContextoGestorProyectos
    {
        private const string ENTIDAD_ETAPA = "Etapas";
        private const string ENTIDAD_PERSONA = "Personas";
        private const string ENTIDAD_TAREA_PROYECTOS = "Etapas.Tareas";
        private const string ENTIDAD_TAREA_ETAPAS = "Tareas";

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Etapa> Etapas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        public ContextoGestorProyectos() : base()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public void VaciarBaseDeDatos()
        {
            Personas.RemoveRange(Personas);
            Tareas.RemoveRange(Tareas);
            Etapas.RemoveRange(Etapas);
            Proyectos.RemoveRange(Proyectos);
            SaveChanges();
        }

        public int AgregarProyecto(Proyecto proyecto)
        {
            Proyectos.Add(proyecto);
            this.SaveChanges();
            return proyecto.ProyectoID;
        }

        public Proyecto ObtenerProyecto(int proyectoID)
        {
            var consulta = from p in Proyectos
                           .Include(ENTIDAD_ETAPA)
                           .Include(ENTIDAD_PERSONA)
                           .Include(ENTIDAD_TAREA_PROYECTOS)
                           where p.ProyectoID == proyectoID
                           select p;
            return consulta.FirstOrDefault<Proyecto>();
        }

        public int AgregarEtapa(Etapa etapa)
        {
            Etapas.Add(etapa);
            SaveChanges();
            return etapa.EtapaID;
        }

        public Etapa ObtenerEtapa(int id)
        {

            var consulta = from e in Etapas.Include(ENTIDAD_PERSONA).Include(ENTIDAD_TAREA_ETAPAS)
                           where e.EtapaID == id
                           select e;
            return consulta.FirstOrDefault<Etapa>();
        }

        public int AgregarTarea(Tarea tarea)
        {
            Tareas.Add(tarea);
            SaveChanges();
            return tarea.TareaID;
        }

        public Tarea ObtenerTarea(int id)
        {
            Tarea t = null;
            var tareas = from r in Tareas.Include(ENTIDAD_PERSONA)
                         where r.TareaID == id
                         select r;
            foreach (Tarea ta in tareas)
            {
                t = ta;
            }
            return t;
        }

        public int AgregarPersona(Persona p)
        {
            Personas.Add(p);
            SaveChanges();
            return p.PersonaID;
        }

        public Persona ObtenerPersona(int id)
        {
            return Personas.Find(id);
        }

        public void EliminarPersona(int id)
        {
            Persona persona = Personas.Find(id);
            Personas.Remove(persona);
            SaveChanges();
        }

        public void EliminarProyecto(int id)
        {
            Configuration.LazyLoadingEnabled = false;
            Proyecto proyecto = ObtenerProyecto(id);
            for (int i = proyecto.Personas.Count - 1; i >= 0; i--)
            {
                EliminarPersona(proyecto.Personas[i].PersonaID);
            }
            for (int i = proyecto.Etapas.Count - 1; i >= 0; i--)
            {
                EliminarEtapa(proyecto.Etapas[i].EtapaID);
            }
            Proyectos.Remove(proyecto);
            SaveChanges();
        }

        public IQueryable<Proyecto> DevolverProyectos()
        {
            return from pro in Proyectos.Include(ENTIDAD_ETAPA).Include(ENTIDAD_PERSONA).Include(ENTIDAD_TAREA_PROYECTOS)
                   select pro;
        }

        public void EliminarEtapa(int id)
        {
            Etapa etapa = ObtenerEtapa(id);
            for (int i = etapa.Personas.Count - 1; i >= 0; i--)
            {
                EliminarPersona(etapa.Personas[i].PersonaID);
            }
            for (int i = etapa.Tareas.Count - 1; i >= 0; i--)
            {
                EliminarTarea(etapa.Tareas[i].TareaID);
            }
            etapa.Personas = new List<Persona>();
            etapa.Tareas = new List<Tarea>();
            Etapas.Remove(etapa);
            SaveChanges();
        }
        public void EliminarTarea(int id)
        {
            Tarea tarea = ObtenerTarea(id);
            eliminarPersonasEnTarea(tarea);
            eliminarAntecesoras(tarea);
            eliminarSubtareasSiTiene(tarea);

            Tareas.Remove(tarea);
            SaveChanges();

        }

        private static bool esTareaCompuesta(Tarea tarea)
        {
            return tarea.GetType() == typeof(TareaCompuesta) || tarea.GetType().BaseType == typeof(TareaCompuesta);
        }

        private void eliminarPersonasEnTarea(Tarea tarea)
        {
            for (int i = tarea.Personas.Count - 1; i >= 0; i--)
            {
                EliminarPersona(tarea.Personas[i].PersonaID);
            }
        }

        private void eliminarAntecesoras(Tarea tarea)
        {
            for (int i = tarea.Antecesoras.Count - 1; i >= 0; i--)
            {
                EliminarTarea(tarea.Antecesoras[i].TareaID);
            }
        }

        private void eliminarSubtareasSiTiene(Tarea tarea)
        {
            if (esTareaCompuesta(tarea))
            {
                for (int i = (((TareaCompuesta)tarea).Subtareas).Count - 1; i >= 0; i--)
                {
                    EliminarTarea((((TareaCompuesta)tarea).Subtareas)[i].TareaID);
                }
            }

        }

        public void ModificarProyecto(Proyecto proyecto)
        {
            var p = Proyectos.Find(proyecto.ProyectoID);
            p.Nombre = proyecto.Nombre;
            p.Objetivo = proyecto.Objetivo;
            p.FechaInicio = proyecto.FechaInicio;
            p.Etapas = proyecto.Etapas;
            SaveChanges();
        }
    }

}
