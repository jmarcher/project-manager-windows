using Xunit;
using Dominio;
using System;
namespace PruebasUnitarias
{
    public class ProyectoPrueba
    {
        [Fact]
        public void DosProyectosSonIguales()
        {
            Proyecto proyectoUno = new Proyecto()
            {
                Identificador=1,
                Nombre="Proyecto"
            };

            Proyecto proyectoDos = new Proyecto()
            {
                Identificador = 1,
                Nombre = "Proyecto"
            };

            Assert.True(proyectoUno.Equals(proyectoDos));
        }

        [Fact]
        public void EliminarEtapa()
        {
            Proyecto proyecto = new Proyecto()
            {
                Identificador = 1,
                Nombre = "Proyecto"
            };
            Etapa etapa = new Etapa()
            {
                Identificacion = 1,
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
                Identificador = 1,
                Nombre = "Proyecto"
            };
            Etapa etapa = new Etapa()
            {
                Identificacion = 1,
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
                Identificador = 1,
                Nombre = "Proyecto"
            };

            Proyecto proyectoDos = new Proyecto()
            {
                Identificador = 2,
                Nombre = "Proyecto"
            };

            Assert.False(proyectoUno.Equals(proyectoDos));
        }

        [Fact]
        public void DuracionCorrecta()
        {
            Proyecto proyecto = new Proyecto();

            TareaCompuesta tarea1 = new TareaCompuesta() { Nombre = "Tarea" };
            Tarea tarea2 = new TareaSimple() { Nombre = "Subtarea", DuracionPendiente = 4 };

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
            Tarea tarea1 = new TareaSimple() { FechaInicio = fecha};
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
            Tarea tarea1 = new TareaSimple() { FechaInicio = fecha };
            Tarea tarea2 = new TareaSimple() { FechaInicio = fecha2 , DuracionPendiente = 10};
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
            Tarea tarea1 = new TareaSimple() { FechaInicio = fecha };
            Tarea tarea2 = new TareaSimple() { FechaInicio = fecha2 };
            Tarea tarea3 = new TareaSimple() { FechaInicio = fecha3 };
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
            Tarea tarea = new TareaSimple() { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Proyecto proyecto = new Proyecto();
            proyecto.AgregarEtapa(etapa);
            proyecto.MarcarFinalizado();
            Assert.True(proyecto.EstaFinalizado);
        }

        [Fact]
        public void MarcarProyectoComoFinalizadoConEtapaSinFinalizar()
        {
            Tarea tarea = new TareaSimple() { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            Proyecto proyecto = new Proyecto();
            proyecto.AgregarEtapa(etapa);
            proyecto.MarcarFinalizado();
            Assert.False(proyecto.EstaFinalizado);
        }

        [Fact]
        public void ProyectoAtrasado()
        {
            Tarea tarea = new TareaSimple()
            {
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
                DuracionPendiente = 1000
            };
            Etapa etapa = new Etapa()
            {
                FechaInicio = DateTime.Now
            };
            etapa.AgregarTarea(tarea);
            Proyecto proyecto = new Proyecto()
            {
                Identificador = 1,
                Nombre = "Proyecto"
            };
            proyecto.AgregarEtapa(etapa);
            Assert.True(proyecto.EstaAtrasado);
        }

        [Fact]
        public void FechaFinalizacionProyecto()
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
                FechaInicio = DateTime.Now
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

            Proyecto unProyecto = new Proyecto()
            {
                Identificador = 1
            };
            unProyecto.AgregarEtapa(imprimeCuenta);

            Assert.Equal(mostrar.FechaFinalizacion, unProyecto.FechaFinalizacion);
        }

        [Fact]
        public void DuracionPendienteProyecto()
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
                FechaInicio = DateTime.Now
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

            Proyecto unProyecto = new Proyecto()
            {
                Identificador = 1
            };
            unProyecto.AgregarEtapa(imprimeCuenta);
            Assert.Equal(100, unProyecto.CalcularDuracionPendiente());
        }
    }
}
