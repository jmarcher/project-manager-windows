using System;
using Xunit;
using Dominio;

namespace PruebasUnitarias
{
    public class TareaCompuestaPrueba
    {
        [Theory]
        [InlineData("Tarea 1", "1990-10-12 00:00", "1990-10-13 00:00")]
        [InlineData("Tarea 2", "1990-10-12 00:00", "1990-10-14 00:00")]
        [InlineData("Tarea 4", "1990-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 5", "1990-10-12 00:00", "1991-10-12 00:00")]
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
        [InlineData("Tarea 1", "1990-10-12 00:00", "1990-10-13 00:00")]
        [InlineData("Tarea 2", "1990-10-12 00:00", "1990-10-14 00:00")]
        [InlineData("Tarea 4", "1990-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 5", "1990-10-12 00:00", "1991-10-12 00:00")]
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
