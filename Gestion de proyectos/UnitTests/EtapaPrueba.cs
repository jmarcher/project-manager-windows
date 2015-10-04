using Xunit;
using Dominio;
using System;

namespace PruebasUnitarias
{
    public class EtapaPrueba
    {
        [Fact]
        public void MarcarEtapaComoFinalizada()
        {
            Tarea tarea = new TareaSimple() { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Assert.True(etapa.EstaFinalizada);
        }
		
        [Fact]
        public void MarcarEtapaComoFinalizadaConTareaSinFinalizar()
        {
            Tarea tarea = new TareaSimple() { Nombre = "Tarea" };
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Assert.False(etapa.EstaFinalizada);
        }
		
        [Fact]
        public void MarcarEtapaComoFinalizadaConTareaConYSinFinalizar()
        {
            Tarea tareaNoFinalizada = new TareaSimple() { Nombre = "Tarea sin terminar" };
            Tarea tareaFinalizada = new TareaSimple() { Nombre = "Tarea terminada" };
            tareaFinalizada.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tareaNoFinalizada);
            etapa.AgregarTarea(tareaFinalizada);
            etapa.MarcarFinalizada();
            Assert.False(etapa.EstaFinalizada);
        }

        [Fact]
        public void CaclularDuracion()
        {
            Tarea contar = new TareaSimple()
            {
                Nombre = "Cuenta numeros",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
                DuracionPendiente = 20
            };
            TareaCompuesta sumar = new TareaCompuesta()
            {
                Nombre = "Sumar",
                FechaInicio=DateTime.Now
            };

            sumar.AgregarSubtarea(contar);

            Tarea mostrar = new TareaSimple()
            {
                Nombre = "Muestra resultado",
                FechaInicio = DateTime.Now.AddDays(400),
                FechaFinalizacion = DateTime.Now.AddDays(1501),
                DuracionPendiente = 100
            };
            TareaCompuesta imprimir = new TareaCompuesta()
            {
                Nombre = "Imprime lo que muestra",
                FechaInicio = mostrar.FechaInicio
            };
            imprimir.AgregarSubtarea(mostrar);

            Etapa imprimeCuenta = new Etapa()
            {
                Identificacion = 1,
                Nombre = "Imprime una cuenta",
                FechaInicio = DateTime.Now
            };
            imprimeCuenta.AgregarTarea(imprimir);
            imprimeCuenta.AgregarTarea(sumar);

            Assert.Equal(100, imprimeCuenta.CalcularDuracionPendiente());
            Assert.Equal(mostrar.FechaFinalizacion, imprimeCuenta.FechaFinalizacion);
        }
    }
}
