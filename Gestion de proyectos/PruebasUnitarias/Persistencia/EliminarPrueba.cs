using Dominio;
using Persistencia;
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
    }
}
