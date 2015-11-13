using Xunit;
using Dominio;
using PersistenciaImp;
using System;

namespace PruebasUnitarias.Persistencia
{
    [Collection("Pruebas de persistencia")]
    public class ObtenerPruebas
    {
        [Fact]
        public void DevolverProyectosPrueba()
        {
            using (var db = new ContextoGestorProyectos())
            {
                db.VaciarBaseDeDatos();
                Proyecto proyecto = new Proyecto()
                {
                    Nombre = "Nombew",
                    Objetivo = "O",
                    FechaInicio = DateTime.Now,

                };
                db.AgregarProyecto(proyecto);
                var consultaOtra = db.DevolverProyectos();
                foreach(Proyecto p in consultaOtra)
                {
                    Assert.Equal(proyecto.ProyectoID,p.ProyectoID);
                }
            }
        }
    }
}
