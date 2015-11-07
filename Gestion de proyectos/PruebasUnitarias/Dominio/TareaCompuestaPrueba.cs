using System;
using Xunit;
using Dominio;
using Dominio.Excepciones;

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
            Assert.True(tareaCompuesta.FechaEsIgual(tareaCompuesta.FechaFinalizacion, tarea.FechaFinalizacion));
        }

        [Fact]
        public void AgregarseComoSubtarea()
        {
            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now
            };
            Assert.False(tareaCompuesta.AgregarSubtarea(tareaCompuesta));
            Assert.False(tareaCompuesta.Subtareas.Contains(tareaCompuesta));
        }

        [Fact]
        public void FechaFinalizacionTareaCompuesta()
        {
            Tarea tareaPrimera = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaFinalizacion = DateTime.Now.AddDays(1),
                FechaInicio = DateTime.Now
            };
            Tarea tareaSegunda = new TareaSimple()
            {
                Nombre = "Tarea2",
                FechaFinalizacion = DateTime.Now.AddDays(2),
                FechaInicio = DateTime.Now
            };
            Tarea tareaTercera = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaFinalizacion = DateTime.Now.AddDays(10),
                FechaInicio = DateTime.Now.AddDays(2)
            };

            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta",
                FechaInicio = tareaPrimera.FechaInicio
            };
            tareaCompuesta.AgregarSubtarea(tareaPrimera);
            tareaCompuesta.AgregarSubtarea(tareaSegunda);
            tareaCompuesta.AgregarSubtarea(tareaTercera);

            Assert.True(tareaPrimera.FechaEsIgual(tareaTercera.FechaFinalizacion, tareaCompuesta.FechaFinalizacion));
        }

        [Fact]
        public void IngresarFechaFinalizacion()
        {
            Assert.Throws<NotSupportedException>(() => new TareaCompuesta()
            {
                Nombre="Tarea",
                FechaFinalizacion=DateTime.Now.Date
            });
        }

        [Theory]
        [InlineData("Tarea 1", "1990-10-12 00:00", "1990-10-13 00:00")]
        [InlineData("Tarea 2", "1990-10-12 00:00", "1990-10-14 00:00")]
        [InlineData("Tarea 3", "1990-10-12 00:00", "2015-10-12 00:00")]
        [InlineData("Tarea 4", "1990-10-12 00:00", "1991-10-12 00:00")]
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
            Assert.Throws<FechaInvalida>(()=>tareaCompuesta.AgregarSubtarea(tarea));
            Assert.False(tareaCompuesta.Subtareas.Contains(tarea));
        }

        [Fact]
        public void MarcarTareaFinalizadaConSubtareasFinalizadas()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                Objetivo = "Objetivo"
            };
            tarea.MarcarFinalizada();
            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta"
            };
            tareaCompuesta.AgregarSubtarea(tarea);
            tareaCompuesta.MarcarFinalizada();
            Assert.True(tareaCompuesta.EstaFinalizada);
        }

        [Fact]
        public void MarcarTareaFinalizadaSinTodasSubtareasFinalizadas()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                Objetivo = "Objetivo"
            };
            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta"
            };
            tareaCompuesta.AgregarSubtarea(tarea);
            tareaCompuesta.MarcarFinalizada();
            Assert.False(tareaCompuesta.EstaFinalizada);
        }

        [Fact]
        public void EstaEnSubestapas()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                Objetivo = "Objetivo"
            };
            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta"
            };
            tareaCompuesta.AgregarSubtarea(tarea);
            tareaCompuesta.MarcarFinalizada();
            Assert.True(tareaCompuesta.estaEnSubtareas(tarea));
        }

        [Fact]
        public void EstaEnSubestapasDeSubetapas()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                Objetivo = "Objetivo"
            };
            TareaCompuesta tareaCompuestaOtra = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta"
            };
            tareaCompuestaOtra.AgregarSubtarea(tarea);
            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta"
            };
            tareaCompuesta.AgregarSubtarea(tareaCompuestaOtra);
            Assert.False(tareaCompuesta.estaEnSubtareas(tarea));
        }

        [Fact]
        public void DuracionPendienteTareaCompuesta()
        {
            Tarea tareaPrimera = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaFinalizacion = DateTime.Now.AddDays(1),
                FechaInicio = DateTime.Now,
                DuracionPendiente = 3
            };
            Tarea tareaSegunda = new TareaSimple()
            {
                Nombre = "Tarea2",
                FechaFinalizacion = DateTime.Now.AddDays(2),
                FechaInicio = DateTime.Now,
                DuracionPendiente = 5
            };
            Tarea tareaTercera = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaFinalizacion = DateTime.Now.AddDays(10),
                FechaInicio = DateTime.Now.AddDays(2),
                DuracionPendiente=2
            };

            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta",
                FechaInicio = tareaPrimera.FechaInicio
            };
            tareaCompuesta.AgregarSubtarea(tareaPrimera);
            tareaCompuesta.AgregarSubtarea(tareaSegunda);
            tareaCompuesta.AgregarSubtarea(tareaTercera);

            Assert.Equal(2, tareaCompuesta.CalcularDuracionPendiente());
        }
        [Fact]
        public void DuracionPendienteTareaCompuestaPorOtraTareaCompuesta()
        {
            Tarea tareaPrimera = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaFinalizacion = DateTime.Now.AddDays(1),
                FechaInicio = DateTime.Now,
                DuracionPendiente = 3
            };
            Tarea tareaSegunda = new TareaSimple()
            {
                Nombre = "Tarea2",
                FechaFinalizacion = DateTime.Now.AddDays(2),
                FechaInicio = DateTime.Now,
                DuracionPendiente = 5
            };
            Tarea tareaTercera = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaFinalizacion = DateTime.Now.AddDays(10),
                FechaInicio = DateTime.Now.AddDays(2),
                DuracionPendiente = 2
            };

            TareaCompuesta tareaCompuestaHija = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta hija",
                FechaInicio = tareaSegunda.FechaInicio
            };

            tareaCompuestaHija.AgregarSubtarea(tareaSegunda);

            TareaCompuesta tareaCompuesta = new TareaCompuesta()
            {
                Nombre = "Tarea Compuesta",
                FechaInicio = tareaPrimera.FechaInicio
            };

            tareaCompuesta.AgregarSubtarea(tareaCompuestaHija);
            tareaCompuesta.AgregarSubtarea(tareaPrimera);
            tareaCompuesta.AgregarSubtarea(tareaTercera);

            Assert.Equal(2, tareaCompuesta.CalcularDuracionPendiente());
        }

        [Theory]
        [InlineData("Tarea 1", 10, "1980-10-12 00:00", "1990-10-12 00:00")]
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
            TareaCompuesta compuesta = new TareaCompuesta()
            {
                Nombre = "Compuesta",
                FechaInicio = DateTime.Parse(fechaInicio)
            };
            compuesta.AgregarSubtarea(tarea);
            Assert.True(compuesta.EstaAtrasada);
        }

        [Theory]
        [InlineData("Tarea 1", 10, "1980-10-12 00:00", "2016-10-13 00:00")]
        [InlineData("Tarea 2", 10, "1990-10-12 00:00", "2016-10-12 00:00")]
        [InlineData("Tarea 3", 10, "1990-10-12 00:00", "2016-10-22 00:00")]
        [InlineData("Tarea 4", 10, "2015-9-12 00:00", "2016-10-20 00:00")]
        public void TareaNoEstaAtrasada(string nombre, int duracionPendiente, string fechaInicio, string fechaFin)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Parse(fechaInicio),
                FechaFinalizacion = DateTime.Parse(fechaFin),
                DuracionPendiente = duracionPendiente
            };
            TareaCompuesta compuesta = new TareaCompuesta()
            {
                Nombre = "Compuesta",
                FechaInicio = DateTime.Parse(fechaInicio)
            };
            compuesta.AgregarSubtarea(tarea);
            Assert.False(compuesta.EstaAtrasada);
        }

        [Fact]
        public void EliminarSubtarea()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea simple",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(30),
                DuracionPendiente = 20
            };
            TareaCompuesta compuesta = new TareaCompuesta()
            {
                Nombre = "Compuesta",
                FechaInicio = DateTime.Now
            };
            compuesta.EliminarSubtarea(tarea);
            Assert.False(compuesta.Subtareas.Contains(tarea));
            compuesta.AgregarSubtarea(tarea);
            compuesta.EliminarSubtarea(tarea);
            Assert.False(compuesta.Subtareas.Contains(tarea));

        }

        [Theory]
        [InlineData("Tarea 1", 10, "1980-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 2", 10, "1990-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 3", 10, "1990-10-12 00:00", "1990-10-22 00:00")]
        [InlineData("Tarea 4", 10, "2015-9-12 00:00", "2015-10-1 00:00")]
        public void ConvertirSimpleACompuesta(string nombre, int duracionPendiente, string fechaInicio, string fechaFin)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Parse(fechaInicio),
                FechaFinalizacion = DateTime.Parse(fechaFin),
                DuracionPendiente = duracionPendiente
            };
            TareaCompuesta tareaCompuesta = new TareaCompuesta(tarea);
            Assert.Equal(nombre, tareaCompuesta.Nombre);
            Assert.Equal(tarea.FechaInicio.Date, tareaCompuesta.FechaInicio.Date);
            Assert.Equal(0, tareaCompuesta.Subtareas.Count);
        }


        [Theory]
        [InlineData("Tarea 1", 10, "1980-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 2", 10, "1990-10-12 00:00", "1990-10-12 00:00")]
        [InlineData("Tarea 3", 10, "1990-10-12 00:00", "1990-10-22 00:00")]
        [InlineData("Tarea 4", 10, "2015-9-12 00:00", "2015-10-1 00:00")]
        public void ClonarPrueba(string nombre, int duracionPendiente, string fechaInicio, string fechaFin)
        {
            TareaCompuesta tarea = new TareaCompuesta()
            {
                Nombre = nombre,
                FechaInicio = DateTime.Parse(fechaInicio)
            };
            Tarea tareaClonada = tarea.Clonar();
            Assert.Equal(tarea,tareaClonada);
        }

    }
}
