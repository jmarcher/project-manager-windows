using System;
using Xunit;
using Dominio;

namespace PruebasUnitarias
{
    public class TareaCompuestaPrueba
    {
        [Theory]
        [InlineData("Tarea 1", "10/12/1990", "10/15/1990")]
        [InlineData("Tarea 2", "10/12/1990", "10/11/1990")]
        [InlineData("Tarea 4", "10/12/1990", "10/12/1990")]
        [InlineData("Tarea 5", "10/12/1990", "10/15/1990")]
        public void AgregarTareaSimple(string nombre,string fechaInicio,
            string fechaFinalizacion)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                FechaFinalizacion = DateTime.Parse(fechaFinalizacion),
                FechaInicio = DateTime.Parse(fechaInicio)
            };
            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta",
                FechaInicio= DateTime.Parse(fechaInicio)
            };
            tareaCompuesta.AgregarSubtarea(tarea);
            Assert.NotNull(tareaCompuesta);
        }

        [Theory]
        [InlineData("Tarea 1", "10/12/1990", "10/15/1990")]
        [InlineData("Tarea 2", "10/12/1990", "10/11/1990")]
        [InlineData("Tarea 4", "10/12/1990", "10/12/1990")]
        [InlineData("Tarea 5", "10/12/1990", "10/15/1990")]
        public void AgregarTareaSimpleIniciaDespues(string nombre, string fechaInicio,
           string fechaFinalizacion)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                FechaFinalizacion = DateTime.Parse(fechaFinalizacion),
                FechaInicio = DateTime.Parse(fechaInicio),
                Prioridad=Tarea.PRIORIDAD_MEDIA
            };
            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta",
                FechaInicio = DateTime.Parse(fechaFinalizacion)
            };
            tareaCompuesta.AgregarSubtarea(tarea);
            Assert.False(tareaCompuesta.Subtareas.Contains(tarea));
        }
    }
}
