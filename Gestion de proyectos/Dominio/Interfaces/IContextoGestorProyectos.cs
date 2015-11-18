using System.Data.Entity;
using System.Linq;
using Dominio;
using DominioInterfaz;

namespace PersistenciaInterfaz
{
    public interface IContextoGestorProyectos
    {
        DbSet<Etapa> Etapas { get; set; }
        DbSet<Persona> Personas { get; set; }
        DbSet<Proyecto> Proyectos { get; set; }
        DbSet<Tarea> Tareas { get; set; }

        int AgregarEtapa(Etapa etapa);
        int AgregarPersona(Persona p);
        int AgregarProyecto(Proyecto proyecto);
        int AgregarTarea(Tarea tarea);
        IQueryable<Proyecto> DevolverProyectos();
        void EliminarEtapa(int id);
        void EliminarPersona(int id);
        void EliminarProyecto(int id);
        void EliminarTarea(int id);
        IEtapa ObtenerEtapa(int id);
        IPersona ObtenerPersona(int id);
        IProyecto ObtenerProyecto(int proyectoID);
        ITarea ObtenerTarea(int id);
        void VaciarBaseDeDatos();
        void ModificarProyecto(IProyecto proyecto);
        void ModificarEtapa(IEtapa etapa);
        void ModificarTarea(ITarea tarea);
    }
}