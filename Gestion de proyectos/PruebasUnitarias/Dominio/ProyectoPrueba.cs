using Xunit;
using Dominio;
using System;
using PersistenciaImp;

namespace PruebasUnitarias
{
    public class ProyectoPrueba
    {
        [Fact]
        public void DosProyectosSonIguales()
        {
            Proyecto proyectoUno = new Proyecto()
            {
                ProyectoID=1,
                Nombre="Proyecto",
                Objetivo = "Objetivo",
                DuracionEstimada = 10,
                FechaInicio = DateTime.Now.Date
            };

            Proyecto proyectoDos = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto",
                Objetivo = "Objetivo",
                DuracionEstimada = 10,
                FechaInicio=DateTime.Now.Date
            };
            Assert.Equal(proyectoUno.Objetivo, proyectoDos.Objetivo);
            Assert.Equal(proyectoUno.FechaInicio, proyectoDos.FechaInicio);
            Assert.True(proyectoUno.Equals(proyectoDos));
        }

        [Fact]
        public void EliminarEtapa()
        {
            Proyecto proyecto = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto"
            };
            Etapa etapa = new Etapa()
            {
                EtapaID = 1,
                Nombre = "Etapa"
            };
            proyecto.AgregarEtapa(etapa);
            proyecto.QuitarEtapa(etapa);
            Assert.False(proyecto.ContieneEtapa(etapa));
        }

        [Fact]
        public void EliminarEtapaQueNoPertenece()
        {
            Proyecto proyecto = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto"
            };
            Etapa etapa = new Etapa()
            {
                EtapaID = 1,
                Nombre = "Etapa"
            };
            proyecto.QuitarEtapa(etapa);
            Assert.False(proyecto.ContieneEtapa(etapa));
        }

        [Fact]
        public void DosProyectosSonDistintos()
        {
            Proyecto proyectoUno = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto"
            };

            Proyecto proyectoDos = new Proyecto()
            {
                ProyectoID = 2,
                Nombre = "Proyecto"
            };

            Assert.False(proyectoUno.Equals(proyectoDos));
        }

        [Fact]
        public void MarcarProyectoComoFinalizado()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos()) { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Proyecto proyecto = new Proyecto();
            proyecto.AgregarEtapa(etapa);
            proyecto.MarcarFinalizado();
            Assert.True(proyecto.EstaFinalizado);
        }

       /* [Fact]
        public void AgregarPersonaAProyecto()
        {
            Persona persona = new Persona()
            {
                Nombre = "Juan",
                Rol = "Administrador"
            };
            Proyecto proyecto = CrearUnProyectoConUnaEtapa();
            proyecto.AgregarPersona(persona);

            Assert.True(proyecto.Personas.Contains(persona));
        }*/

        [Fact]
        public void MarcarProyectoComoFinalizadoConEtapaSinFinalizar()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos()) { Nombre = "Tarea" };
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
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
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
                ProyectoID = 1,
                Nombre = "Proyecto"
            };
            proyecto.AgregarEtapa(etapa);
            Assert.True(proyecto.EstaAtrasado);
        }
        [Fact]
        public void ProyectoPadreDeTarea()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Tarea hija",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
                DuracionPendiente = 100
            };
            Etapa etapa = new Etapa()
            {
                EtapaID = 1,
                FechaInicio = DateTime.Now
            };
            etapa.AgregarTarea(tarea);
            Proyecto proyecto = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto"
            };
            proyecto.AgregarEtapa(etapa);
            using (var db = new ContextoGestorProyectos())
            {
                db.AgregarProyecto(proyecto);
            }
            Assert.Equal(proyecto.ProyectoID, tarea.ObtenerProyectoPadre().ProyectoID);
        }

        [Fact]
        public void ProyectoNoEstaAtrasado()
        {
            Tarea tarea = new TareaSimple(new ContextoGestorProyectos())
            {
                FechaInicio = DateTime.Now.AddDays(-50),
                FechaFinalizacion = DateTime.Now.AddDays(40),
                DuracionPendiente = 20
            };
            Etapa etapa = new Etapa()
            {
                FechaInicio = DateTime.Now.AddDays(-60)
            };
            etapa.AgregarTarea(tarea);
            Proyecto proyecto = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto no atrasado"
            };
            proyecto.AgregarEtapa(etapa);
            Assert.False(proyecto.EstaAtrasado);
        }

        [Fact]
        public void ProyectoContieneEtapa()
        {
            Etapa etapa = new Etapa()
            {
                EtapaID = 10,
                FechaInicio = DateTime.Now.AddDays(-60)
            };
            Proyecto proyecto = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto con etapa"
            };
            proyecto.AgregarEtapa(etapa);
            Assert.True(proyecto.ContieneEtapa(etapa));
        }

        [Fact]
        public void FechaFinalizacionProyecto()
        {
            DateTime fechaEsperada = DateTime.Now.AddDays(1501);

            Proyecto unProyecto = CrearUnProyectoConUnaEtapa();

            Assert.Equal(fechaEsperada.Date, unProyecto.FechaFinalizacion.Date);
        }

        [Fact]
        public void DuracionPendienteProyecto()
        {
            Proyecto unProyecto = CrearUnProyectoConUnaEtapa();
            Assert.Equal(100, unProyecto.CalcularDuracionPendiente());
        }

        private static Proyecto CrearUnProyectoConUnaEtapa()
        {
            Etapa imprimeCuenta = CrearEtapaConDosTareas();

            Proyecto unProyecto = new Proyecto()
            {
                ProyectoID = 1
            };
            unProyecto.AgregarEtapa(imprimeCuenta);
            return unProyecto;
        }

        private static Etapa CrearEtapaConDosTareas()
        {
            TareaCompuesta sumar = CrearTareaCompuestaConOtraTareaSimple();

            TareaCompuesta imprimir = CrearTareaCompuestaConUnaTareaSimple();

            Etapa imprimeCuenta = new Etapa()
            {
                EtapaID = 1,
                Nombre = "Imprime una cuenta",
                FechaInicio = DateTime.Now
            };
            AgregarTareasAEtapa(sumar, imprimir, imprimeCuenta);
            return imprimeCuenta;
        }

        private static void AgregarTareasAEtapa(TareaCompuesta sumar, TareaCompuesta imprimir, Etapa imprimeCuenta)
        {
            imprimeCuenta.AgregarTarea(imprimir);
            imprimeCuenta.AgregarTarea(sumar);
        }

        private static TareaCompuesta CrearTareaCompuestaConOtraTareaSimple()
        {
            Tarea contar = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Cuenta numeros",
                FechaInicio = DateTime.Now,
                FechaFinalizacion = DateTime.Now,
                DuracionPendiente = 20
            };
            TareaCompuesta sumar = new TareaCompuesta(new ContextoGestorProyectos())
            {
                Nombre = "Sumar",
                FechaInicio = DateTime.Now
            };

            sumar.AgregarSubtarea(contar);
            return sumar;
        }

        private static TareaCompuesta CrearTareaCompuestaConUnaTareaSimple()
        {
            Tarea mostrar = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = "Muestra resultado",
                FechaInicio = DateTime.Now.AddDays(400),
                FechaFinalizacion = DateTime.Now.AddDays(1501),
                DuracionPendiente = 100
            };
            TareaCompuesta imprimir = new TareaCompuesta(new ContextoGestorProyectos())
            {
                Nombre = "Imprime lo que muestra",
                FechaInicio = mostrar.FechaInicio
            };
            imprimir.AgregarSubtarea(mostrar);
            return imprimir;
        }

       
    }
}
