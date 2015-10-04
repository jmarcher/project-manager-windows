using Xunit;
using Dominio;

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
    }
}
