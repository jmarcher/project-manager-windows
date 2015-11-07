using Dominio;
using Xunit;
using Persistencia;
using System;

namespace PruebasUnitarias
{
    [Collection("Pruebas de agregar")]
    public class AgregarPrueba
    {
        [Theory]
        [InlineData("Proyecto 1", "Objetivo")]
        [InlineData("Proyecto 2", "Objetivo 2")]
        [InlineData("Proyecto 3", "Objetivo 3")]
        [InlineData("Proyecto 4", "Objetivo 4")]
        [InlineData("Proyecto 5", "Objetivo 5")]
        public void AgregarProyecto(string nombre, string objetivo)
        {
            Proyecto p = new Proyecto()
            {
                Nombre = nombre,
                Objetivo = objetivo,
                FechaInicio = DateTime.Now
            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarProyecto(p);
                Proyecto proyectoRetorno = db.ObtenerProyecto(id);
                Assert.Equal(p, proyectoRetorno);

            }

        }

        [Theory]
        [InlineData("Proyecto con persona 1", "Objetivo", "Jorge", "Admin")]
        [InlineData("Proyecto con persona 2", "Objetivo 2", "Jorge", "Limpiador")]
        [InlineData("Proyecto con persona 3", "Objetivo 3", "Mario", "Desarrollador")]
        [InlineData("Proyecto con persona 4", "Objetivo 4", "Sandrio", "Mirador")]
        [InlineData("Proyecto con persona 5", "Objetivo 5", "Dario", "Fotografo")]
        public void AgregarProyectoConPersona(string nombreProyecto, string objetivo, string nombrePersona, string rol)
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = nombreProyecto,
                Objetivo = objetivo,
                FechaInicio = DateTime.Now
            };
            Persona persona = new Persona()
            {
                Nombre = nombrePersona,
                Rol = rol
            };
            proyecto.AgregarPersona(persona);
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarProyecto(proyecto);
                Proyecto proyectoRetorno = db.ObtenerProyecto(id);
                Assert.True(proyectoRetorno.Personas.Contains(persona));
                Assert.Equal(proyecto, proyectoRetorno);
            }

        }

        [Theory]
        [InlineData("Proyecto con persona 1", "Objetivo", "Jorge", "Admin")]
        [InlineData("Proyecto con persona 2", "Objetivo 2", "Jorge", "Limpiador")]
        [InlineData("Proyecto con persona 3", "Objetivo 3", "Mario", "Desarrollador")]
        [InlineData("Proyecto con persona 4", "Objetivo 4", "Sandrio", "Mirador")]
        [InlineData("Proyecto con persona 5", "Objetivo 5", "Dario", "Fotografo")]
        public void AgregarProyectoConPersonaYEtapa(string nombreProyecto, string objetivo, string nombrePersona, string rol)
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = nombreProyecto,
                Objetivo = objetivo,
                FechaInicio = DateTime.Now
            };
            Persona persona = new Persona()
            {
                Nombre = nombrePersona,
                Rol = rol
            };
            proyecto.AgregarPersona(persona);
            Etapa etapa = new Etapa()
            {
                Nombre = "Una etapa",
                FechaInicio = DateTime.Now
            };
            proyecto.AgregarEtapa(etapa);
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarProyecto(proyecto);
                Proyecto unProyecto = db.ObtenerProyecto(id);
                Assert.True(unProyecto.Etapas.Contains(etapa));
                Assert.True(unProyecto.Personas.Contains(persona));
                Assert.Equal(proyecto, unProyecto);
            }

        }

        [Theory]
        [InlineData("Etapa 1")]
        [InlineData("Etapa 2")]
        [InlineData("Etapa 3")]
        [InlineData("Etapa 4")]
        [InlineData("Etapa 5")]
        public void AgregarEtapa(string nombre)
        {
            Etapa etapa = new Etapa()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Now
            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarEtapa(etapa);
                Etapa unaEtapa = db.ObtenerEtapa(id);
                Assert.Equal(etapa, unaEtapa);
            }
        }

        [Theory]
        [InlineData("Etapa 1")]
        [InlineData("Etapa 2")]
        [InlineData("Etapa 3")]
        [InlineData("Etapa 4")]
        [InlineData("Etapa 5")]
        public void AgregarEtapaConPersona(string nombre)
        {
            Etapa etapa = new Etapa()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Now
            };
            Persona p = new Persona()
            {
                Nombre = "Roger",
                Rol = "Un rol"
            };
            etapa.AgregarPersona(p);
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarEtapa(etapa);
                Etapa unaEtapa = db.ObtenerEtapa(id);
                Assert.Contains(p, unaEtapa.Personas);
                Assert.Equal(etapa, unaEtapa);
            }
        }

        [Fact]
        public void AgregarTareaSimple()
        {
            TareaSimple ts = new TareaSimple()
            {
                Nombre = "Una tarea simple",
                Descripcion = "Desc",
                Objetivo = "Obj",
                FechaInicio = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_BAJA,
                DuracionPendiente = 14
            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarTarea(ts);
                TareaSimple tsRetorno = (TareaSimple)db.ObtenerTarea(id);
                Assert.Equal(ts, tsRetorno);
            }
        }

        [Fact]
        public void AgregarTareaCompuesta()
        {
            TareaSimple ts = new TareaSimple()
            {
                Nombre = "Una tarea simple",
                Descripcion = "Desc",
                Objetivo = "Obj",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(1),
                Prioridad = Tarea.PRIORIDAD_BAJA,
                DuracionPendiente = 14
            };
            TareaCompuesta tc = new TareaCompuesta()
            {
                Nombre = "Una tarea Compuesta",
                Descripcion = "Desc comp",
                Objetivo = "Obj comp",
                FechaInicio = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_BAJA,
            };
            tc.AgregarSubtarea(ts);
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarTarea(tc);
                TareaCompuesta tcRetorno = (TareaCompuesta)db.ObtenerTarea(id);
                Assert.Contains(ts, tc.Subtareas);
                Assert.Equal(tc, tcRetorno);
            }
        }

        [Theory]
        [InlineData("Raul","Arquitecto")]
        [InlineData("Sergio", "Fisurador")]
        [InlineData("Mario", "Maqueteador")]
        [InlineData("Julio", "Lopuo")]
        [InlineData("David", "Limpus")]

        public void AgregarPersona(string nombre, string rol)
        {
            Persona p = new Persona()
            {
                Nombre = nombre,
                Rol = rol
            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarPersona(p);
                Persona pRetorno = db.ObtenerPersona(id);
                Assert.Equal(p, pRetorno);
            }
        }
    }
}
