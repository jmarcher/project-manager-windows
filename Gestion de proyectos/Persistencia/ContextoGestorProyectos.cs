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
            Proyecto retorno = new Proyecto();
            var proyectos = from p in Proyectos
                            where p.ProyectoID == proyectoID
                            select p;
            foreach (Proyecto proyectoActual in proyectos)
            {
                retorno = proyectoActual;
            }
            return retorno;
        }

        public int AgregarEtapa(Etapa etapa)
        {
            Etapas.Add(etapa);
            SaveChanges();
            return etapa.EtapaID;
        }

        public Etapa ObtenerEtapa(int id)
        {
            Etapa etapaRetorno = null;
            var etapas = from etapa in Etapas
                         where etapa.EtapaID == id
                         select etapa;
            foreach (Etapa e in etapas)
            {
                etapaRetorno = e;
            }
            return etapaRetorno;
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
            Tarea tareaRetorno = null;
            var tareas = from t in Tareas
                         where t.TareaID == id
                         select t;
            foreach(Tarea tarea in tareas)
            {
                tareaRetorno = tarea;
            }
            return tareaRetorno;
        }

        public int AgregarPersona(Persona p)
        {
            Personas.Add(p);
            SaveChanges();
            return p.PersonaID;
        }

        public Persona ObtenerPersona(int id)
        {
            Persona pRetorno = null;
            var personas = from p in Personas
                           where p.PersonaID == id
                           select p;
            foreach(var per in personas)
            {
                pRetorno = per;
            }
            return pRetorno;
        }

        public void EliminarPersona(int id)
        {
            Persona p = Personas.Find(id);
            Personas.Remove(p);
            SaveChanges();
        }
    }

}
