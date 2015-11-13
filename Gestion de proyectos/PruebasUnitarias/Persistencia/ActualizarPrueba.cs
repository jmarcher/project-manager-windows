using Xunit;
using Dominio;
using PersistenciaImp;
using System;

namespace PruebasUnitarias.Persistencia
{
    [Collection("Pruebas de persistencia")]
    public class ActualizarPrueba
    {
        [Theory]
        [InlineData("Proyecto 1", "Objetivo")]
        [InlineData("Proyecto 2", "Objetivo 2")]
        [InlineData("Proyecto 3", "Objetivo 3")]
        [InlineData("Proyecto 4", "Objetivo 4")]
        [InlineData("Proyecto 5", "Objetivo 5")]
        public void ActualizarProyecto(string nombre, string objetivo)
        {
            Proyecto p = new Proyecto()
            {
                Nombre = nombre,
                Objetivo = objetivo,
                FechaInicio = DateTime.Now
            };
            using (var db = new ContextoGestorProyectos())
            {
                db.AgregarProyecto(p);
                p.Nombre = "Uno nuevo";
                p.Objetivo = "Objetivooooo";
                p.FechaInicio = DateTime.Now.AddDays(-10);
                p.AgregarEtapa(new Etapa());
                db.ModificarProyecto(p);
                Assert.Equal(p.Nombre, db.ObtenerProyecto(p.ProyectoID).Nombre);
                Assert.Equal(p.Objetivo, db.ObtenerProyecto(p.ProyectoID).Objetivo);
                Assert.Equal(p.FechaInicio, db.ObtenerProyecto(p.ProyectoID).FechaInicio);
            }
        }
    }
}
