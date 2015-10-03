using Xunit;
using Dominio;
using System;

namespace PruebasUnitarias
{
    public class TareaSimplePrueba
    {
        [Theory]
        [InlineData("Tarea 1","10/12/1990","10/15/1990")]
        [InlineData("Tarea 2", "10/12/1990", "10/11/1990")]
        [InlineData("Tarea 4", "10/12/1990", "10/12/1990")]
        [InlineData("Tarea 5", "10/12/1990", "10/15/1990")]
        public void CrearTareaSimpleFechasValidas(string nombre,
            string fechaInicio,string fechaFinalizacion)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Parse(fechaInicio),
                FechaFinalizacion = DateTime.Parse(fechaFinalizacion)
            };
            Assert.NotNull(tarea);
            Assert.True(tarea.FechaFinalizacion.Equals(DateTime.Parse(fechaFinalizacion)));
            Assert.True(tarea.FechaInicio.Equals(DateTime.Parse(fechaInicio)));
        }

        [Theory]
        [InlineData("Tarea 1", "10/12/1980", "10/12/1990")]
        [InlineData("Tarea 2", "10/12/1990", "10/15/1990")]
        [InlineData("Tarea 3", "10/12/1990", "10/15/1990")]
        [InlineData("Tarea 4", "10/12/1990", "10/11/1990")]        
        public void CrearTareaSimpleFechasInvalidas(string nombre,
            string fechaFinalizacion, string fechaInicio)
        {
            Assert.Throws<ArgumentOutOfRangeException>(()=>new TareaSimple()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Parse(fechaInicio),
                FechaFinalizacion = DateTime.Parse(fechaFinalizacion)
            });
        }

        [Theory]
        [InlineData("Tarea 1",10)]
        [InlineData("Tarea 2", 15)]
        [InlineData("Tarea 3", 0)]
        [InlineData("Tarea 4", 1)]
        public void CalcularDuracionPendiente(string nombre,int duracionPendiente)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                DuracionPendiente = duracionPendiente
            };
            Assert.Equal(duracionPendiente, tarea.CalcularDuracionPendiente());
        }
    }
}
