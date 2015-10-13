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
        private List<Proyecto> Proyectos;

        private  InstanciaUnica() 
        {
            this.Proyectos = new List<Proyecto>();
        }
        public void AgregarProyecto(Proyecto nuevo) 
        {
            this.Proyectos.Add(nuevo);
        }
        public void AgregarProyectos(List<Proyecto> nuevo)
        {
            this.Proyectos=nuevo;
        }
        public List<Proyecto> DevolverProyectos()
        {
            return this.Proyectos;
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

