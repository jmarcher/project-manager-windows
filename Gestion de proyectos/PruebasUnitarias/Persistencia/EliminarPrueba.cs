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
    }
}
