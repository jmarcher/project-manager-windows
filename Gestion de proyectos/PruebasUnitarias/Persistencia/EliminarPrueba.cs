using Dominio;
using Persistencia;
using System;
using Xunit;

namespace PruebasUnitarias.Persistencia
{
    [Collection("Pruebas de persistencia")]
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
            Etapa etapa = new Etapa()
            {
                Nombre = "Etapa",
                FechaInicio = DateTime.Now

            };
            Persona persona = new Persona()
            {
                Nombre = "Nombre",
                Rol = "Roger"
            };
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                FechaFinalizacion = DateTime.Now.AddDays(1),
                Descripcion = "Descripcion",
                Objetivo = "Objetivo",
                DuracionPendiente = 10
            };
            etapa.AgregarTarea(tarea);
            p.AgregarPersona(persona);
            p.AgregarEtapa(etapa);
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
            Persona p = new Persona()
            {
                Nombre = "Nombre",
                Rol = "Roger"
            };
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                FechaFinalizacion = DateTime.Now.AddDays(1),
                Descripcion = "Descripcion",
                Objetivo = "Objetivo",
                DuracionPendiente = 10
            };
            etapa.AgregarPersona(p);
            etapa.AgregarTarea(tarea);
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
                Nombre = "TareaSimpleSubtarea",
                FechaInicio = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                FechaFinalizacion = DateTime.Now.AddDays(1),
                Descripcion="Descripcion",
                Objetivo="Objetivo",
                DuracionPendiente=10
            };
            Tarea tareaAntecesora = new TareaSimple()
            {
                Nombre = "TareaAntecesora",
                FechaInicio = DateTime.Now.AddDays(-10),
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                FechaFinalizacion = DateTime.Now.AddDays(-9),
                Descripcion = "Descripcion",
                Objetivo = "Objetivo",
                DuracionPendiente = 10
            };
            TareaCompuesta tc = new TareaCompuesta()
            {
                Nombre = "TareaCompuesta",
                FechaInicio = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                Descripcion = "Descripcion",
                Objetivo = "Objetivo",
            };
            tc.AgregarAntecesora(tareaAntecesora);
            tc.AgregarSubtarea(tarea);
            Persona p = new Persona()
            {
                Nombre = "Nombre",
                Rol = "Roger"
            };
            tc.AgregarPersona(p);
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarTarea(tc);
                db.EliminarTarea(id);
                Assert.DoesNotContain(tc, db.Tareas);
            }
        }
    }
}
