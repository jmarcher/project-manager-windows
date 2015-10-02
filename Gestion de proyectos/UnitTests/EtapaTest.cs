using Xunit;
using Dominio;

namespace UnitTests
{
    public class EtapaTest
    {
        [Fact]
        public void MarcarEtapaComoFinalizada()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            tarea.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Assert.True(etapa.EstaFinalizada);
        }
		
        [Fact]
        public void MarcarEtapaComoFinalizadaConTareaSinFinalizar()
        {
            Tarea tarea = new Tarea() { Nombre = "Tarea" };
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tarea);
            etapa.MarcarFinalizada();
            Assert.False(etapa.EstaFinalizada);
        }
		
        [Fact]
        public void MarcarEtapaComoFinalizadaConTareaConYSinFinalizar()
        {
            Tarea tareaNoFinalizada = new Tarea() { Nombre = "Tarea sin terminar" };
            Tarea tareaFinalizada = new Tarea() { Nombre = "Tarea terminada" };
            tareaFinalizada.MarcarFinalizada();
            Etapa etapa = new Etapa();
            etapa.AgregarTarea(tareaNoFinalizada);
            etapa.AgregarTarea(tareaFinalizada);
            etapa.MarcarFinalizada();
            Assert.False(etapa.EstaFinalizada);
        }
    }
}
