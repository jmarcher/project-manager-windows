﻿using Xunit;
using PersistenciaImp;

namespace PruebasUnitarias
{
    public class ContextoGestorProyectosPrueba
    {
        [Fact]
        public void CrearNuevoContexto()
        {
            ContextoGestorProyectos cgp = new ContextoGestorProyectos();
            Assert.NotNull(cgp);
        }
    }
}
