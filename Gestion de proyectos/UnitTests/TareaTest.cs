using Xunit;

using Dominio;
using System;
using Dominio.Exceptions;

namespace UnitTests
{
    public class TareaTest
    {
        [Fact]
        public void MarcarTareaComoFinalizada()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            Assert.True(tarea.EstaFinalizada);
        }

        [Fact]
        public void MarcarTareaComoFinalizadaConSubtareaSinFinalizar()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            Tarea subtarea = new Tarea() { Nombre = "subTarea" };
            tarea.AgregarSubtarea(subtarea);
            tarea.MarcarFinalizada();
            Assert.False(tarea.EstaFinalizada);
        }

        [Fact]
        public void MarcarTareaComoFinalizadaConSubtareaFinalizada()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            Tarea subtarea = new Tarea() { Nombre = "subTarea" };
            subtarea.MarcarFinalizada();
            tarea.AgregarSubtarea(subtarea);
            tarea.MarcarFinalizada();
            Assert.True(tarea.EstaFinalizada);
        }


        [Fact]
        public void AgregarUnaSubtarea()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            Tarea subtarea = new Tarea() { Nombre = "Subtarea" };

            bool retorno = tarea.AgregarSubtarea(subtarea);
            Assert.True(retorno);
        }

        [Fact]
        public void AgregarUnaSubtareaIniciaAntes()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea", FechaInicio = new DateTime(2015, 10, 10) };
            Tarea subtarea = new Tarea() { Nombre = "Subtarea", FechaInicio = new DateTime(2015, 10, 9) };

            Assert.Throws<FechaInvalida>(() => tarea.AgregarSubtarea(subtarea));
        }

        [Fact]
        public void SeAgregaASiMismaComoSubtarea()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            bool retorno = tarea.AgregarSubtarea(tarea);
            Assert.False(retorno);
        }

        [Fact]
        public void CalcularDuracionSimple()
        {
            Tarea tarea = new Tarea() { DuracionPendiente = 10 };
            Assert.Equal(10, tarea.CalcularDuracion());
        }

        [Fact]
        public void CalcularDuracionCompuesta()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea", DuracionPendiente = 10 };
            Tarea subtarea = new Tarea() { Nombre = "Subtarea", DuracionPendiente = 10 };
            tarea.AgregarSubtarea(subtarea);

            Assert.Equal(20, tarea.CalcularDuracion());
        }

        [Fact]
        public void CalcularDuracionDosVecesCompuesta()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea", DuracionPendiente = 10 };
            Tarea subtarea = new Tarea() { Nombre = "Subtarea", DuracionPendiente = 10 };
            Tarea subsubtarea = new Tarea() { Nombre = "Subsubtarea", DuracionPendiente = 10 };
            tarea.AgregarSubtarea(subtarea);
            subtarea.AgregarSubtarea(subsubtarea);

            Assert.Equal(30, tarea.CalcularDuracion());
        }

        [Fact]
        public void ValidarFechas()
        {
            Tarea tarea = new Tarea()
            {
                FechaInicio = new System.DateTime(2015, 09, 01),
                FechaFinalizacion = new System.DateTime(2015, 09, 10)
            };
            Assert.Equal(1, tarea.FechaFinalizacion.CompareTo(tarea.FechaInicio));
        }

        [Fact]
        public void ValidarFechasInvalidas()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Tarea()
            {
                FechaInicio = new System.DateTime(2015, 09, 01),
                FechaFinalizacion = new System.DateTime(2015, 08, 10)
            });
            // Assert.Equal(1, tarea.FFinalizacion.CompareTo(tarea.FInicio));
        }


        [Fact]
        public void AgregarAntecesora()
        {
            Tarea tarea = new Tarea();
            Tarea antecesora = new Tarea();

            bool retorno = tarea.AgregarAntecesora(antecesora);

            Assert.True(retorno);

        }

        [Fact]
        public void AgregarAntecesoraASiMisma()
        {
            Tarea tarea = new Tarea();

            bool retorno = tarea.AgregarAntecesora(tarea);

            Assert.False(retorno);

        }
    }
}
