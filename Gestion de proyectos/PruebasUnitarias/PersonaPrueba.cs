using Xunit;

namespace PruebasUnitarias
{
    public class PersonaPrueba
    {
        [Theory]
        [InlineData("Jorge", "Administrador")]
        [InlineData("Diego", "Un rol")]
        [InlineData("Nombre","Rol")]
        public void CrearPersona(string nombre, string rol)
        {
            Persona persona = new Persona()
            {
                Nombre = nombre,
                Rol = rol
            };

            Assert.Equal(nombre, persona.Nombre);
            Assert.Equal(rol, persona.Rol);
        }
    }
}
