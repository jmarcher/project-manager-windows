﻿using Xunit;
using Dominio;
using System;
using System.Text;

namespace PruebasUnitarias
{
    public class TareaSimplePrueba
    {
        [Theory]
        [InlineData("Tarea 1","1990-10-12 00:00", "1990-10-13 00:00")]
        [InlineData("Tarea 2", "1990-10-12 00:00", "1990-10-14 00:00")]
        [InlineData("Tarea 4", "1990-10-12 00:00", "1990-10-13 00:00")]
        [InlineData("Tarea 5", "1990-10-12 00:00", "1991-10-12 00:00")]
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
        [InlineData("Tarea 1", "1980-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 2", "1910-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 3", "1970-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 4", "1990-10-12 00:00", "1990-10-13 00:00")]        
        public void CrearTareaSimpleFechasInvalidas(string nombre,
            string fechaFinalizacion, string fechaInicio)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TareaSimple()
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

        [Theory]
        [InlineData("Tarea 1", 10,"1980-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 2", 10, "1990-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 3", 10, "1990-10-12 00:00", "1990-10-22 00:00")]
        [InlineData("Tarea 4", 10, "2015-9-12 00:00", "2015-10-1 00:00")]
        public void TareaAtrasada(string nombre, int duracionPendiente, string fechaInicio, string fechaFin)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Parse(fechaInicio),
                FechaFinalizacion = DateTime.Parse(fechaFin),
                DuracionPendiente = duracionPendiente
            };
            Assert.True(tarea.EstaAtrasada);
        }

        [Theory]
        [InlineData("Tarea 1", Tarea.PRIORIDAD_ALTA, "1980-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 2", Tarea.PRIORIDAD_ALTA, "1990-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 3", Tarea.PRIORIDAD_ALTA, "1990-10-12 00:00", "1990-10-22 00:00")]
        [InlineData("Tarea 4", Tarea.PRIORIDAD_ALTA, "2015-9-12 00:00", "2015-10-1 00:00")]
        public void PruebaToString(string nombre, int prioridad, string fechaInicio, string fechaFin)
        {
            StringBuilder textoEsperado = new StringBuilder();
            textoEsperado.Append(nombre);
            textoEsperado.Append(" [Prioridad: ");
            textoEsperado.Append("Alta");
            textoEsperado.Append(", Duración pendiente: ");
            textoEsperado.Append(10);
            textoEsperado.Append(", Inicio: ");
            textoEsperado.Append(DateTime.Parse(fechaInicio).Date.ToString());
            textoEsperado.Append(", Fin: ");
            textoEsperado.Append(DateTime.Parse(fechaFin).Date.ToString());
            textoEsperado.Append("]");

            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                Prioridad = prioridad,
                FechaInicio = DateTime.Parse(fechaInicio).Date,
                FechaFinalizacion = DateTime.Parse(fechaFin).Date,
                DuracionPendiente = 10
                
            };

            Assert.Equal(textoEsperado.ToString(), tarea.ToString());

        }
    }
}
