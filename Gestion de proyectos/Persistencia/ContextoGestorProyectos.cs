using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dominio;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int AgregarTarea(Tarea tarea)
        {
            tarea.FechaModificada = DateTime.Now.Date;
            Tareas.Add(tarea);
            SaveChanges();
            return tarea.TareaID;
        }

        public Tarea ObtenerTarea(int id)
        {
            return Tareas.Find(id);
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
            Persona p = Personas.Find(id);
            Personas.Remove(p);
            SaveChanges();
        }

        public void EliminarProyecto(int id)
        {
            Proyecto p = Proyectos.Find(id);
            Proyectos.Remove(p);
            SaveChanges();
        }
    }

}
