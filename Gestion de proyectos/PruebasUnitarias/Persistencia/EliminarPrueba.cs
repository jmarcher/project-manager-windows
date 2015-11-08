using Dominio;
using Persistencia;
using System;
using Xunit;

namespace PruebasUnitarias.Persistencia
{
    public class EliminarPrueba
    {
        [Fact]
        public void EliminarPersona()
        {
            Persona p = new Persona()
            {
                Nombre = "Julio",
                Rol = "Un rol"
            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarPersona(p);
                db.EliminarPersona(id);
                Assert.DoesNotContain(p, db.Personas);
            }
        }

        [Fact]
        public void EliminarProyecto()
        {
            Proyecto p = new Proyecto()
            {
                Nombre = "Proyecto",
                Objetivo = "Objetivo",
                FechaInicio = DateTime.Now
            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarProyecto(p);
                db.EliminarProyecto(id);
                Assert.DoesNotContain(p, db.Proyectos);
            }
        }

        [Fact]
        public void EliminarEtapa()
        {
            Etapa etapa = new Etapa()
            {
                Nombre = "Etapa",
                FechaInicio = DateTime.Now
                
            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarEtapa(etapa);
                db.EliminarEtapa(id);
                Assert.DoesNotContain(etapa, db.Etapas);
            }
        }

        [Fact]
        public void EliminarTarea()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                FechaFinalizacion = DateTime.Now.AddDays(1),
                Descripcion="Descripcion",
                Objetivo="Objetivo",
                DuracionPendiente=10



            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarTarea(tarea);
                db.EliminarTarea(id);
                Assert.DoesNotContain(tarea, db.Tareas);
            }
        }
    }
}
