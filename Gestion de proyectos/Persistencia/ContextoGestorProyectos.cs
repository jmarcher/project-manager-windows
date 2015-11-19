using System.Data.Entity;
using Dominio;
using System.Linq;
using System.Collections.Generic;
using PersistenciaInterfaz;
using DominioInterfaz;

namespace PersistenciaImp
{
    public class ContextoGestorProyectos : DbContext, IContextoGestorProyectos
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

        public IProyecto ObtenerProyecto(int proyectoID)
        {
            var consulta = from p in Proyectos
                           .Include(ENTIDAD_ETAPA)
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

        public IEtapa ObtenerEtapa(int id)
        {

            var consulta = from e in Etapas.Include(ENTIDAD_TAREA_ETAPAS)
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

        public ITarea ObtenerTarea(int id)
        {
            ITarea t = null;
            var tareas = from r in Tareas.Include(ENTIDAD_PERSONA)//.Include("Antecesoras").Include("Subtareas")
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

        public IPersona ObtenerPersona(int id)
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
            IProyecto proyecto = ObtenerProyecto(id);
            for (int i = proyecto.Etapas.Count - 1; i >= 0; i--)
            {
                EliminarEtapa(proyecto.Etapas[i].EtapaID);
            }
            Proyectos.Remove(Proyectos.Find(id));
            SaveChanges();
        }

        public IQueryable<Proyecto> DevolverProyectos()
        {
            return from pro in Proyectos.Include(ENTIDAD_ETAPA).Include(ENTIDAD_TAREA_PROYECTOS)
                   select pro;
        }

        public void EliminarEtapa(int id)
        {
            IEtapa etapa = ObtenerEtapa(id);

            for (int i = etapa.Tareas.Count - 1; i >= 0; i--)
            {
                EliminarTarea(etapa.Tareas[i].TareaID);
            }
            etapa.Tareas = new List<Tarea>();
            Etapas.Remove(Etapas.Find(id));
            SaveChanges();
        }
        public void EliminarTarea(int id)
        {
            ITarea tarea = ObtenerTarea(id);
            eliminarPersonasEnTarea(tarea);
            eliminarAntecesoras(tarea);
            eliminarSubtareasSiTiene(tarea);

            Tareas.Remove(Tareas.Find(id));
            SaveChanges();

        }


        private static bool esTareaCompuesta(ITarea tarea)
        {
            return tarea.GetType() == typeof(TareaCompuesta) || tarea.GetType().BaseType == typeof(TareaCompuesta);
        }

        private void eliminarPersonasEnTarea(ITarea tarea)
        {
            for (int i = tarea.Personas.Count - 1; i >= 0; i--)
            {
                EliminarPersona(tarea.Personas[i].PersonaID);
            }
        }

        private void eliminarAntecesoras(ITarea tarea)
        {
            for (int i = tarea.Antecesoras.Count - 1; i >= 0; i--)
            {
                EliminarTarea(tarea.Antecesoras[i].TareaID);
            }
        }

        private void eliminarSubtareasSiTiene(ITarea tarea)
        {
            if (esTareaCompuesta(tarea))
            {
                for (int i = (((TareaCompuesta)tarea).Subtareas).Count - 1; i >= 0; i--)
                {
                    EliminarTarea((((TareaCompuesta)tarea).Subtareas)[i].TareaID);
                }
            }

        }

        public void ModificarProyecto(IProyecto proyecto)
        {
            var proyectoAEditar = Proyectos.Find(proyecto.ProyectoID);
            proyectoAEditar.Nombre = proyecto.Nombre;
            proyectoAEditar.Objetivo = proyecto.Objetivo;
            proyectoAEditar.FechaInicio = proyecto.FechaInicio;
            proyectoAEditar.Etapas = proyecto.Etapas;
            SaveChanges();
        }

        public void ModificarEtapa(IEtapa etapa)
        {
            var etapaAEditar = Etapas.Find(etapa.EtapaID);
            etapaAEditar.Nombre = etapa.Nombre;
            etapaAEditar.DuracionEstimada = etapa.DuracionEstimada;
            etapaAEditar.FechaInicio = etapa.FechaInicio;
            etapaAEditar.Tareas = etapa.Tareas;
            SaveChanges();
        }

        public void ModificarTarea(ITarea tarea)
        {
            var t = Tareas.Find(tarea.TareaID);
            t.Nombre = tarea.Nombre;
            t.Objetivo = tarea.Objetivo;
            t.Descripcion = tarea.Descripcion;
            t.DuracionEstimada = tarea.DuracionEstimada;
            t.Personas = tarea.Personas;
            t.Antecesoras = tarea.Antecesoras;
            t.FechaInicio = tarea.FechaInicio;
            t.Prioridad = tarea.Prioridad;
            if (esTareaCompuesta(tarea))
            {
                ((TareaCompuesta)t).Subtareas = ((TareaCompuesta)tarea).Subtareas;
            }
            else
            {
                ((TareaSimple)t).FechaFinalizacion = ((TareaSimple)tarea).FechaFinalizacion;
            }
            SaveChanges();
        }
    }

}
