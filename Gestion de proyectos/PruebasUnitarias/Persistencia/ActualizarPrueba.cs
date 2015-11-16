using Xunit;
using Dominio;
using PersistenciaImp;
using System;

namespace PruebasUnitarias.Persistencia
{
    [Collection("Pruebas de persistencia")]
    public class ActualizarPrueba
    {
        [Theory]
        [InlineData("Proyecto 1", "Objetivo")]
        [InlineData("Proyecto 2", "Objetivo 2")]
        [InlineData("Proyecto 3", "Objetivo 3")]
        [InlineData("Proyecto 4", "Objetivo 4")]
        [InlineData("Proyecto 5", "Objetivo 5")]
        public void ActualizarProyecto(string nombre, string objetivo)
        {
            
            using (var db = new ContextoGestorProyectos())
            {
                Proyecto p = new Proyecto(db)
                {
                    Nombre = nombre,
                    Objetivo = objetivo,
                    FechaInicio = DateTime.Now
                };
                db.AgregarProyecto(p);
                p.Nombre = "Uno nuevo";
                p.Objetivo = "Objetivooooo";
                p.FechaInicio = DateTime.Now.AddDays(-10);
                p.AgregarEtapa(new Etapa());
                db.ModificarProyecto(p);
                Assert.Equal(p.Nombre, db.ObtenerProyecto(p.ProyectoID).Nombre);
                Assert.Equal(p.Objetivo, db.ObtenerProyecto(p.ProyectoID).Objetivo);
                Assert.Equal(p.FechaInicio, db.ObtenerProyecto(p.ProyectoID).FechaInicio);
            }
        }

        [Theory]
        [InlineData("Etapa 1")]
        [InlineData("Etapa 2")]
        [InlineData("Etapa 3")]
        [InlineData("Etapa 4")]
        [InlineData("Etapa 5")]
        public void ActualizarEtapa(string nombre)
        {
            Etapa etapa = new Etapa()
            {
                Nombre = nombre,
                DuracionEstimada = 10,
                FechaInicio = DateTime.Now
            };
            using (var db = new ContextoGestorProyectos())
            {
                db.AgregarEtapa(etapa);
                etapa.DuracionEstimada = 15;
                etapa.Nombre = "Raul";
                etapa.AgregarPersona(new Persona());
                db.ModificarEtapa(etapa);
                Assert.Equal(etapa.Nombre, db.ObtenerEtapa(etapa.EtapaID).Nombre);
            }
        }

        [Fact]
        public void ActualizarTareaSimple()
        {
            TareaSimple tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Una tarea simple",
                Descripcion = "Desc",
                Objetivo = "Obj",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(1),
                Prioridad = Tarea.PRIORIDAD_BAJA,
                DuracionPendiente = 14
            };
            using(var db = new ContextoGestorProyectos())
            {
                db.AgregarTarea(tarea);
                tarea.Antecesoras.Add(new TareaSimple());
                tarea.Nombre = "Saladaaaaa";
                tarea.FechaFinalizacion = DateTime.Now.AddDays(19);
                db.ModificarTarea(tarea);
                Assert.Equal(tarea.Nombre, db.ObtenerTarea(tarea.TareaID).Nombre);
            }
        }

        [Fact]
        public void ActualizarTareaCompuesta()
        {
            TareaCompuesta tarea = new TareaCompuesta(new ContextoGestorProyectos())
            {
                Nombre = "Una tarea Compuesta",
                Descripcion = "Desc comp",
                Objetivo = "Obj comp",
                FechaInicio = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_BAJA,
            };
            using (var db = new ContextoGestorProyectos())
            {
                db.AgregarTarea(tarea);
                tarea.Subtareas.Add(new TareaSimple());
                tarea.Nombre = "Saladaaaaa";
                db.ModificarTarea(tarea);
                Assert.Equal(tarea.Nombre, db.ObtenerTarea(tarea.TareaID).Nombre);
            }
        }
    }
}
