using Dominio;
using DominioInterfaz;
using PersistenciaImp;
using System;
using Xunit;

namespace PruebasUnitarias
{
    public class EtapaPrueba
    {

        [Fact]
        public void FechaFinalizacionCorrecta()
        {
            DateTime fechaEsperada = new DateTime(2015, 12, 20);
            IEtapa imprimeCuenta = CrearEtapaConSubTarea();
            Assert.Equal(fechaEsperada.Date, imprimeCuenta.FechaFinalizacion.Date);
        }

        [Fact]
        public void MarcarEtapaComoFinalizada()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos()) { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            IEtapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Assert.True(etapa.EstaFinalizada);
        }
		
        [Fact]
        public void MarcarEtapaComoFinalizadaConTareaSinFinalizar()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos()) { Nombre = "Tarea" };
            IEtapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Assert.False(etapa.EstaFinalizada);
        }
		
        [Fact]
        public void MarcarEtapaComoFinalizadaConTareaConYSinFinalizar()
        {
            Tarea tareaNoFinalizada = new TareaSimple(new ContextoGestorProyectos()) { Nombre = "Tarea sin terminar" };
            Tarea tareaFinalizada = new TareaSimple(new ContextoGestorProyectos()) { Nombre = "Tarea terminada" };
            tareaFinalizada.MarcarFinalizada();
            IEtapa etapa = new Etapa();
            etapa.AgregarTarea(tareaNoFinalizada);
            etapa.AgregarTarea(tareaFinalizada);
            etapa.MarcarFinalizada();
            Assert.False(etapa.EstaFinalizada);
        }

        [Fact]
        public void CalcularDuracionPendiente()
        {
            IEtapa imprimeCuenta = CrearEtapaConSubTarea();

            Assert.Equal(111, imprimeCuenta.CalcularDuracionPendiente());
        }

        private static IEtapa CrearEtapaConSubTarea()
        {
            TareaSimple T1 = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "T1",
                FechaInicio = new DateTime(2015, 1, 1),
                FechaFinalizacion = new DateTime(2015, 1, 10),
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                DuracionPendiente = 8
            };

            TareaSimple T2 = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "T2",
                FechaInicio = new DateTime(2015, 1, 11),
                FechaFinalizacion = new DateTime(2015, 12, 15),
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                DuracionPendiente = 100
            };
            TareaSimple T3 = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "T3",
                FechaInicio = new DateTime(2015, 12, 16),
                FechaFinalizacion = new DateTime(2015, 12, 20),
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                DuracionPendiente = 3
            };
            TareaSimple T4 = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "T1",
                FechaInicio = new DateTime(2015, 1, 11),
                FechaFinalizacion = new DateTime(2015, 1, 13),
                Prioridad = Tarea.PRIORIDAD_MEDIA,
                DuracionPendiente = 10
            };

            T2.AgregarAntecesora(T1);
            T3.AgregarAntecesora(T2);
            T4.AgregarAntecesora(T1);

            IEtapa etapaPrueba = new Etapa()
            {
                EtapaID = 1,
                Nombre = "Imprime una cuenta",
                FechaInicio = DateTime.Now
            };

            etapaPrueba.AgregarTarea(T1);
            etapaPrueba.AgregarTarea(T2);
            etapaPrueba.AgregarTarea(T3);
            etapaPrueba.AgregarTarea(T4);

            return etapaPrueba;
        }


        [Fact]
        public void EtapaNoEstaAtrasada()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now.AddDays(2000),
                DuracionPendiente = 1000
            };
            IEtapa etapa = new Etapa()
            {
                FechaInicio = DateTime.Now,
                DuracionEstimada = 10,
            };
            etapa.AgregarTarea(tarea);
            Assert.False(etapa.EstaAtrasada);
        }

        [Fact]
        public void EtapaAtrasada()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
                DuracionPendiente = 1000
            };
            IEtapa etapa = new Etapa()
            {
                FechaInicio = DateTime.Now
            };
            etapa.AgregarTarea(tarea);
            Assert.True(etapa.EstaAtrasada);
        }

        [Fact]
        public void EliminarUnaTarea()
        {
            IEtapa etapa = CrearEtapaConSubTarea();
            Tarea aEliminar = etapa.Tareas[0];
            etapa.EliminarTarea(aEliminar);
            Assert.False(etapa.Tareas.Contains(aEliminar));
        }

        [Theory]
        [InlineData(1,"Etapa", "1990-10-13 00:00")]
        [InlineData(2, "Etapa", "1990-10-13 00:00")]
        [InlineData(3, "Etapa", "1990-10-13 00:00")]
        [InlineData(4, "Etapa", "1990-10-13 00:00")]
        [InlineData(5, "Etapa", "1990-10-13 00:00")]
        public void CreacionDeEtapa(int id, string nombre, string fechaInicio)
        {
            Etapa etapa = new Etapa()
            {
                EtapaID = id,
                Nombre = nombre,
                FechaInicio = DateTime.Parse(fechaInicio).Date
            };
            Assert.Equal(DateTime.Parse(fechaInicio).Date, etapa.FechaInicio.Date);
            Assert.Equal(id, etapa.EtapaID);
            Assert.Equal(nombre, etapa.Nombre);
        }


    }
}
