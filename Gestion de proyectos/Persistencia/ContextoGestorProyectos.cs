using System;
using System.Data.Entity;
using Dominio;
using System.Linq;

namespace Persistencia
{
    public class ContextoGestorProyectos : DbContext
    {
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Etapa> Etapas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }


        public int AgregarProyecto(Proyecto proyecto)
        {
            Proyectos.Add(proyecto);
            this.SaveChanges();
            return proyecto.ProyectoID;
        }

        public Proyecto ObtenerProyecto(int proyectoID)
        {
            return Proyectos.Find(proyectoID);
        }

        public int AgregarEtapa(Etapa etapa)
        {
            Etapas.Add(etapa);
            SaveChanges();
            return etapa.EtapaID;
        }

        public Etapa ObtenerEtapa(int id)
        {
            return Etapas.Find(id);
        }

        public Tuple<int,DateTime> AgregarTarea(Tarea tarea)
        {
            Tareas.Add(tarea);
            SaveChanges();
            return Tuple.Create(tarea.TareaID,tarea.FechaModificada);
        }

        public Tarea ObtenerTarea(int id, DateTime fechaModificada)
        {
            Tarea t = null;
            var tareas = from r in Tareas
                         where r.TareaID == id && r.FechaModificada == fechaModificada.Date
                         select r;
            foreach(Tarea ta in tareas)
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
            Proyecto proyecto = Proyectos.Find(id);
            Proyectos.Remove(proyecto);
            SaveChanges();
        }

        public void EliminarEtapa(int id)
        {
            Etapa etapa = Etapas.Find(id);
            Etapas.Remove(etapa);
            SaveChanges();
        }
        public void EliminarTarea(int id, DateTime fechaModificada)
        {
            var tarea = Tareas.SingleOrDefault(x => x.TareaID == id && x.FechaModificada== fechaModificada);
            if (tarea != null)
            {
                Tareas.Remove(tarea);
                SaveChanges();
            }
        }
    }

}
