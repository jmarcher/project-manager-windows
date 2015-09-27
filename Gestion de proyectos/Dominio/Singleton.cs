using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   public class Singleton
    {
        private static Singleton instanciaSingleton = null;
        private List<Proyecto> listaProyectos;

        private  Singleton() 
        {
            this.listaProyectos = new List<Proyecto>();
        }
        public void agregarProyecto(Proyecto nuevo) 
        {
            this.listaProyectos.Add(nuevo);
        }
        public void agregarListaProyecto(List<Proyecto> nuevo)
        {
            this.listaProyectos=nuevo;
        }
        public List<Proyecto> devolverListaProyectos()
        {
            return this.listaProyectos;
        }
       
        public static Singleton Instance
        {
            get
            {
                if (instanciaSingleton == null)
                instanciaSingleton = new Singleton();
 
                return instanciaSingleton;
            }
        }
    }   
}

