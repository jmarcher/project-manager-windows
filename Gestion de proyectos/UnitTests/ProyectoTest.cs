using Xunit;
using Dominio;
using System;
namespace UnitTests
{
    public class ProyectoTest
    {
        [Fact]
        public void DosProyectosSonIguales()
        {
            Proyecto proyectoUno = new Proyecto()
            {
                Id=1,
                Nombre="Proyecto"
            };

            Proyecto proyectoDos = new Proyecto()
            {
                Id = 1,
                Nombre = "Proyecto"
            };

            Assert.True(proyectoUno.Equals(proyectoDos));
        }

        [Fact]
        public void EliminarEtapa()
        {
            Proyecto proyecto = new Proyecto()
            {
                Id = 1,
                Nombre = "Proyecto"
            };
            Etapa etapa = new Etapa()
            {
                Id = 1,
                Nombre = "Etapa"
            };
            proyecto.AgregarEtapa(etapa);
            proyecto.QuitarEtapa(etapa);
            Assert.False(proyecto.PerteneceEtapa(etapa));
        }

        [Fact]
        public void EliminarEtapaQueNoPertenece()
        {
            Proyecto proyecto = new Proyecto()
            {
                Id = 1,
                Nombre = "Proyecto"
            };
            Etapa etapa = new Etapa()
            {
                Id = 1,
                Nombre = "Etapa"
            };
            proyecto.QuitarEtapa(etapa);
            Assert.False(proyecto.PerteneceEtapa(etapa));
        }

        [Fact]
        public void DosProyectosSonDistintos()
        {
            Proyecto proyectoUno = new Proyecto()
            {
                Id = 1,
                Nombre = "Proyecto"
            };

            Proyecto proyectoDos = new Proyecto()
            {
                Id = 2,
                Nombre = "Proyecto"
            };

            Assert.False(proyectoUno.Equals(proyectoDos));
        }

        [Fact]
        public void DuracionCorrecta()
        {
            Proyecto proyecto = new Proyecto();

            Tarea tarea1 = new Tarea() { Nombre = "Tarea", DuracionPendiente = 6 };
            Tarea tarea2 = new Tarea() { Nombre = "Subtarea", DuracionPendiente = 4 };

            Etapa etapa1 = new Etapa();
            Etapa etapa2 = new Etapa();


            tarea1.AgregarSubtarea(tarea2);
            etapa1.AgregarTarea(tarea1);
            

            proyecto.AgregarEtapa(etapa1);
            proyecto.AgregarEtapa(etapa2);

            Assert.Equal(10, proyecto.CalcularDuracion());
        }

        [Fact]
        public void FechaFinalizacionCorrecta()
        {
            Proyecto proyecto = new Proyecto();
            DateTime fecha = DateTime.Parse("5/1/2015 8:30:00 AM");
            Etapa etapa1 = new Etapa();
            Tarea tarea1 = new Tarea() { FechaInicio = fecha};
            etapa1.AgregarTarea(tarea1);
            proyecto.AgregarEtapa(etapa1);

            Assert.Equal(DateTime.Parse("5/1/2015 8:30:00 AM"), proyecto.ObtenerFechaFinalizacion());
        }
        [Fact]
        public void FechaFinalizacionCorrectaDosTareas()
        {
            Proyecto proyecto = new Proyecto();
            DateTime fecha = DateTime.Parse("5/1/2015 8:30:00 AM");
            DateTime fecha2 = DateTime.Parse("5/9/2015 8:30:00 AM");
            Etapa etapa1 = new Etapa();
            Tarea tarea1 = new Tarea() { FechaInicio = fecha };
            Tarea tarea2 = new Tarea() { FechaInicio = fecha2 , DuracionPendiente = 10};
            etapa1.AgregarTarea(tarea2);
            etapa1.AgregarTarea(tarea1);
            proyecto.AgregarEtapa(etapa1);

            Assert.Equal(DateTime.Parse("15/9/2015 8:30:00 AM"), proyecto.ObtenerFechaFinalizacion());
        }
        [Fact]
        public void FechaFinalizacionCorrectaDosEtapas()
        {
            Proyecto proyecto = new Proyecto();
            DateTime fecha = DateTime.Parse("5/1/2015 8:30:00 AM");
            DateTime fecha2 = DateTime.Parse("5/1/2020 8:30:00 AM");
            DateTime fecha3 = DateTime.Parse("5/1/2030 8:30:00 AM");
            Etapa etapa1 = new Etapa();
            Etapa etapa2 = new Etapa();
            Tarea tarea1 = new Tarea() { FechaInicio = fecha };
            Tarea tarea2 = new Tarea() { FechaInicio = fecha2 };
            Tarea tarea3 = new Tarea() { FechaInicio = fecha3 };
            etapa1.AgregarTarea(tarea1);
            etapa1.AgregarTarea(tarea3);
            etapa2.AgregarTarea(tarea2);

            proyecto.AgregarEtapa(etapa1);
            proyecto.AgregarEtapa(etapa2);


            Assert.Equal(DateTime.Parse("5/1/2030 8:30:00 AM"), proyecto.ObtenerFechaFinalizacion());
        }

        [Fact]
        public void MarcarProyectoComoFinalizado()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Proyecto proyecto = new Proyecto();
            proyecto.AgregarEtapa(etapa);
            proyecto.MarcarFinalizado();
            Assert.True(proyecto.Finalizado);
        }

        [Fact]
        public void MarcarProyectoComoFinalizadoConEtapaSinFinalizar()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            Proyecto proyecto = new Proyecto();
            proyecto.AgregarEtapa(etapa);
            proyecto.MarcarFinalizado();
            Assert.False(proyecto.Finalizado);
        }

    }
}
