using Dominio;
using System;
using Xunit;

namespace PruebasUnitarias
{
    public class TareaPrueba
    {
        [Theory]
        [InlineData("Alta",Tarea.PRIORIDAD_ALTA)]
        [InlineData("Media", Tarea.PRIORIDAD_MEDIA)]
        [InlineData("Baja", Tarea.PRIORIDAD_BAJA)]
        public void DefinirPrioridad(string prioridad, int esperado)
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                Descripcion = "Hace algo"
            };
            tarea.DefinirPrioridad(prioridad);
            Assert.Equal(esperado, tarea.Prioridad);
        }

        [Fact]
        public void FechaInicioDespuesFechaFinalizacion()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaFinalizacion = DateTime.Now
            };
            Assert.Throws<ArgumentOutOfRangeException>(() => tarea.FechaInicio = DateTime.Now.AddDays(100));
        }

        [Fact]
        public void AgregarAntecesora()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };
            Tarea tareaConAntecesora = new TareaSimple()
            {
                Nombre = "Con antecesora",
                FechaInicio = tarea.FechaFinalizacion,
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };

            Assert.True(tareaConAntecesora.AgregarAntecesora(tarea));
            Assert.True(tareaConAntecesora.Antecesoras.Contains(tarea));
        }
        [Fact]
        public void RetornoUltimaAntecesora()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea antecesora ultima",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };
            Tarea tareaAntecesoraAnterior = new TareaSimple()
            {
                Nombre = "Tarea antecesora ultima",
                FechaInicio = DateTime.Now.AddDays(-500),
                FechaFinalizacion = DateTime.Now.AddHours(-100)
            };
            Tarea tareaConAntecesora = new TareaSimple()
            {
                Nombre = "Con antecesora",
                FechaInicio = tarea.FechaFinalizacion,
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };

            tareaConAntecesora.AgregarAntecesora(tarea);
            tareaConAntecesora.AgregarAntecesora(tareaAntecesoraAnterior);

            Assert.Equal(tarea, tareaConAntecesora.UltimaAntecesora());
        }

        [Fact]
        public void TareaSinAntecesora()
        {
            Tarea tareaConAntecesora = new TareaSimple()
            {
                Nombre = "Con antecesora",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };

            Assert.Null(tareaConAntecesora.UltimaAntecesora());
        }
        [Fact]
        public void AgregarAntecesoraPeroIniciaDespues()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };
            Tarea tareaConAntecesora = new TareaSimple()
            {
                Nombre = "Con antecesora",
                FechaInicio = DateTime.Now.AddDays(-5),
                FechaFinalizacion = DateTime.Now.AddDays(100)
            };

            Assert.False(tareaConAntecesora.AgregarAntecesora(tarea));
            Assert.False(tareaConAntecesora.Antecesoras.Contains(tarea));
        }

        [Fact]
        public void AgregarAntecesoraSimisma()
        {
            Tarea tarea = new TareaSimple()
            {
                Nombre = "Tarea",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now
            };

            Assert.False(tarea.AgregarAntecesora(tarea));
            Assert.False(tarea.Antecesoras.Contains(tarea));
        }
          [Fact]
        public void ObtenerPadreDeTarea()
        {
            Tarea tarea = new TareaSimple();
            Etapa etapa = new Etapa();
            Proyecto proyecto = new Proyecto() { Nombre = "proyecto de prueba"};
            etapa.AgregarTarea(tarea);
            proyecto.AgregarEtapa(etapa);
            InstanciaUnica.Instancia.AgregarProyecto(proyecto);

            Assert.Equal(tarea.ObtenerProyectoPadre(),proyecto);
        }
    }
}
