using Dominio;
using PersistenciaImp;
using System;
using Xunit;

namespace PruebasUnitarias
{
    public class TareaPrueba
    {
        [Theory]
        [InlineData("Alta", Tarea.PRIORIDAD_ALTA)]
        [InlineData("Media", Tarea.PRIORIDAD_MEDIA)]
        [InlineData("Baja", Tarea.PRIORIDAD_BAJA)]
        public void DefinirPrioridad(string prioridad, int esperado)
        {
            Tarea tarea = new TareaSimple( new ContextoGestorProyectos())
            {
                Nombre = "Tarea",
                Descripcion = "Hace algo",
                DuracionEstimada = 10
            };
            tarea.DefinirPrioridad(prioridad);
            Assert.Equal(esperado, tarea.Prioridad);
        }



        [Fact]
        public void AgregarAntecesora()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };
            Tarea tareaConAntecesora = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Con antecesora",
                FechaInicio = tarea.FechaFinalizacion,
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };

            Assert.True(tareaConAntecesora.AgregarAntecesora(tarea));
            Assert.True(tareaConAntecesora.Antecesoras.Contains(tarea));
        }
        [Fact]
        public void RetornoUltimaAntecesora()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Tarea antecesora ultima",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };
            Tarea tareaAntecesoraAnterior = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Tarea antecesora ultima",
                FechaInicio = DateTime.Now.AddDays(-500),
                FechaFinalizacion = DateTime.Now.AddHours(-100)
            };
            Tarea tareaConAntecesora = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Con antecesora",
                FechaInicio = tarea.FechaFinalizacion,
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };

            tareaConAntecesora.AgregarAntecesora(tarea);
            tareaConAntecesora.AgregarAntecesora(tareaAntecesoraAnterior);

            Assert.Equal(tarea, tareaConAntecesora.UltimaAntecesora());
        }

        [Fact]
        public void TareaSinAntecesora()
        {
            Tarea tareaConAntecesora = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Con antecesora",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };

            Assert.Null(tareaConAntecesora.UltimaAntecesora());
        }

        [Fact]
        public void AgregarPersonaATarea()
        {
            Persona persona = new Persona()
            {
                Nombre = "Jorge",
                Rol = "Rol"
            };
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Con antecesora",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };
            tarea.AgregarPersona(persona);


            Assert.True(tarea.Personas.Contains(persona));
        }
        [Fact]
        public void AgregarAntecesoraPeroIniciaDespues()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };
            Tarea tareaConAntecesora = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Con antecesora",
                FechaInicio = DateTime.Now.AddDays(-5),
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };

            Assert.False(tareaConAntecesora.AgregarAntecesora(tarea));
            Assert.False(tareaConAntecesora.Antecesoras.Contains(tarea));
        }

        [Fact]
        public void AgregarAntecesoraSimisma()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };

            Assert.False(tarea.AgregarAntecesora(tarea));
            Assert.False(tarea.Antecesoras.Contains(tarea));
        }

        [Fact]
        public void AgregarModificacion()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };
            tarea.AgregarModificacion("Historial de prueba.");
            Assert.Equal("["+DateTime.Now+"] Historial de prueba.\r\n",tarea.Historial);
        }

        [Fact]
        public void EqualsConDBNull()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };
            Assert.False(tarea.Equals(System.DBNull.Value));
        }

        [Fact]
        public void ObtenerPadreDeTarea()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos());
            Etapa etapa = new Etapa();
            Proyecto proyecto = new Proyecto( new ContextoGestorProyectos()) { Nombre = "proyecto de prueba" };
            etapa.AgregarTarea(tarea);
            proyecto.AgregarEtapa(etapa);

            using (var db = new ContextoGestorProyectos())
            {
                db.AgregarProyecto(proyecto);
            }

            Assert.Equal(tarea.ObtenerProyectoPadre().ProyectoID, proyecto.ProyectoID);
        }

        [Fact]
        public void ObtenerAbuelo()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                TareaID = 99,
                Nombre = "Tarea",
                Objetivo = "Objetivo",

                FechaFinalizacion = DateTime.Now.AddDays(1),
                FechaInicio = DateTime.Now
            };
            TareaCompuesta tareaCompuestaOtra = new TareaCompuesta(new ContextoGestorProyectos())
            {
                TareaID = 199,
                Nombre = "Tarea Compuesta",
                FechaInicio = DateTime.Now
            };
            tareaCompuestaOtra.AgregarSubtarea(tarea);
            TareaCompuesta tareaCompuesta = new TareaCompuesta(new ContextoGestorProyectos())
            {
                TareaID = 192,
                Nombre = "Tarea Compuesta",
                FechaInicio = DateTime.Now
            };

            tareaCompuesta.AgregarSubtarea(tareaCompuestaOtra);
            Etapa etapa = new Etapa();
            Proyecto proyecto = new Proyecto( new ContextoGestorProyectos()) { Nombre = "proyecto de prueba" };
            etapa.AgregarTarea(tareaCompuesta);
            proyecto.AgregarEtapa(etapa);
            using(var db = new ContextoGestorProyectos())
            {
                db.AgregarProyecto(proyecto);
            }
            Assert.Equal(proyecto.ProyectoID, tareaCompuesta.ObtenerProyectoPadre().ProyectoID);
        }
        [Fact]
        public void ObtenerPadreDeTareaHuerfana()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos());

            Assert.Null(tarea.ObtenerProyectoPadre());

        }

        [Fact]
        public void FechaModificada()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos());
            DateTime fechaActual = DateTime.Now.Date;
            Assert.Equal(fechaActual,tarea.FechaModificada);

        }
    }
}
