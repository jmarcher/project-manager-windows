using Xunit;
using Dominio;
using System;

namespace PruebasUnitarias
{
    public class TareaSimplePrueba
    {
        [Theory]
        [InlineData("Tarea","10/12/1990","10/15/1990")]
        [InlineData("Tarea", "10/12/1990", "10/11/1990")]
        [InlineData("Tarea", "10/12/1990", "10/12/1990")]
        [InlineData("Tarea", "10/12/1990", "10/15/1990")]
        public void CrearTareaSimpleFechasValidas(string nombre,
            string fechaInicio,string fechaFin)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Parse(fechaInicio),
                FechaFin = DateTime.Parse(fechaFin)
            };
            Assert.NotNull(tarea);
        }
    }
}
