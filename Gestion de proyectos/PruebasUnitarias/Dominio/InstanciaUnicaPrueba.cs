using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dominio;

namespace PruebasUnitarias
{
    public class InstanciaUnicaPrueba
    {
        [Fact]
        public void AgregarUnProyecto()
        {
            Proyecto proyecto = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto"
            };
            InstanciaUnica.Instancia.AgregarProyecto(proyecto);
            Assert.True(InstanciaUnica.Instancia.DevolverProyectos().Contains(proyecto));
        }


        [Fact]
        public void AgregarListaProyectosAInstancia()
        {
            Proyecto proyecto = new Proyecto()
            {
                ProyectoID = 1,
                Nombre = "Proyecto"
            };
            List<Proyecto> proyectos = new List<Proyecto>();
            proyectos.Add(proyecto);
            InstanciaUnica.Instancia.AgregarProyectos(proyectos);
            Assert.True(InstanciaUnica.Instancia.DevolverProyectos().Contains(proyecto));
        }
    }
}
