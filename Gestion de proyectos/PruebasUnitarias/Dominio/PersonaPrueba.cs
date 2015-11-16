using Xunit;
using Dominio;

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

        [Theory]
        [InlineData("Jorge", "Administrador")]
        [InlineData("Diego", "Un rol")]
        [InlineData("Nombre", "Rol")]
        public void ToStringPrueba(string nombre, string rol)
        {
            Persona persona = new Persona()
            {
                Nombre = nombre,
                Rol = rol
            };

            Assert.Equal(nombre + " [" + rol + "]", persona.ToString());
        }

        [Theory]
        [InlineData("Jorge", "Administrador")]
        [InlineData("Diego", "Un rol")]
        [InlineData("Nombre", "Rol")]
        public void PeronasIguales(string nombre, string rol)
        {
            Persona persona = new Persona()
            {
                Nombre = nombre,
                Rol = rol
            };
            Persona igualA = new Persona()
            {
                Nombre = nombre,
                Rol = rol
            };
            Assert.True(persona.Equals(igualA));
        }

    }
}
