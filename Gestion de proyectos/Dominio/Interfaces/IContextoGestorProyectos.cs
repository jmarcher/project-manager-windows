using System.Data.Entity;
using System.Linq;

namespace Dominio
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
        Etapa ObtenerEtapa(int id);
        Persona ObtenerPersona(int id);
        Proyecto ObtenerProyecto(int proyectoID);
        Tarea ObtenerTarea(int id);
        void VaciarBaseDeDatos();
    }
}