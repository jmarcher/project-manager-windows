using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
   public class InstanciaUnica
    {
        private static InstanciaUnica instancia = null;
        private List<Proyecto> listaProyectos;

        private  InstanciaUnica() 
        {
            this.listaProyectos = new List<Proyecto>();
        }
        public void AgregarProyecto(Proyecto nuevo) 
        {
            this.listaProyectos.Add(nuevo);
        }
        public void AgregarListaProyecto(List<Proyecto> nuevo)
        {
            this.listaProyectos=nuevo;
        }
        public List<Proyecto> DevolverListaProyectos()
        {
            return this.listaProyectos;
        }
       
        public static InstanciaUnica Instancia
        {
            get
            {
                if (instancia == null)
                instancia = new InstanciaUnica();
 
                return instancia;
            }
        }
    }   
}

