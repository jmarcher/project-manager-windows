using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    class Singleton
    {
        private static Singleton instanciaSingleton = null;
        private List<Proyecto> listaProyectos;
       
        public static Singleton GetInstance()
        {
            if (instanciaSingleton == null)
            {
                instanciaSingleton = new Singleton();
            }
            return instanciaSingleton;
        }
    }
}
