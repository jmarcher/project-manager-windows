using Dominio;
using Xunit;
using PersistenciaImp;
using System;
using DominioInterfaz;

namespace PruebasUnitarias
{
    [Collection("Pruebas de persistencia")]
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
            
            using (var db = new ContextoGestorProyectos())
            {
                Proyecto p = new Proyecto(db)
                {
                    Nombre = nombre,
                    Objetivo = objetivo,
                    FechaInicio = DateTime.Now
                };
                int id = db.AgregarProyecto(p);
                IProyecto proyectoRetorno = db.ObtenerProyecto(id);
                Assert.Equal(p, proyectoRetorno);

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
            
            using (var db = new ContextoGestorProyectos())
            {
                Proyecto proyecto = new Proyecto(db)
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
                Etapa etapa = new Etapa()
                {
                    Nombre = "Una etapa",
                    FechaInicio = DateTime.Now
                };
                proyecto.AgregarEtapa(etapa);
                int id = db.AgregarProyecto(proyecto);
                IProyecto unProyecto = db.ObtenerProyecto(id);
                Assert.True(unProyecto.Etapas.Contains(etapa));
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
                IEtapa unaEtapa = db.ObtenerEtapa(id);
                Assert.Equal(etapa, unaEtapa);
            }
        }

        [Theory]
        [InlineData("Etapa 1")]
        [InlineData("Etapa 2")]
        [InlineData("Etapa 3")]
        [InlineData("Etapa 4")]
        [InlineData("Etapa 5")]
        public void AgregarEtapaFecha(string nombre)
        {
            Etapa etapa = new Etapa()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Now
            };
            using (var db = new ContextoGestorProyectos())
            {
                int id = db.AgregarEtapa(etapa);
                IEtapa unaEtapa = db.ObtenerEtapa(id);
                Assert.Equal(etapa, unaEtapa);
            }
        }

        [Fact]
        public void AgregarTareaSimple()
        {
            TareaSimple ts = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Una tarea simple",
                Descripcion = "Desc",
                Objetivo = "Obj",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
                Prioridad = Tarea.PRIORIDAD_BAJA,
                DuracionPendiente = 14,
                Contexto = new ContextoGestorProyectos()
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
            TareaSimple ts = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Una tarea simple",
                Descripcion = "Desc",
                Objetivo = "Obj",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(1),
                Prioridad = Tarea.PRIORIDAD_BAJA,
                DuracionPendiente = 14
            };
            TareaCompuesta tc = new TareaCompuesta(new ContextoGestorProyectos())
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
                IPersona pRetorno = db.ObtenerPersona(id);
                Assert.Equal(p, pRetorno);
            }
        }
    }
}
