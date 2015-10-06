using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class VentanaDetallesEtapa : Form
    {
        private Etapa etapa;

        public VentanaDetallesEtapa()
        {
            InitializeComponent();
        }

        public VentanaDetallesEtapa(Etapa etapa)
        {
            InitializeComponent();
            this.etapa = etapa;
            InicializarComponentes();
            ActualizarArbolTareas();
        }

        private void InicializarComponentes()
        {
            arbolDeTareas.ImageList = listaImagenes;
        }

        private void ActualizarArbolTareas()
        {
            foreach(Tarea tarea in etapa.Tareas)
            {
                if (EsUnaTareaSimple(tarea))
                {
                    TreeNode nodoArbol = GenerarNodoArbolTareaSimple(tarea);
                    nodoArbol.ImageIndex = 1;
                    arbolDeTareas.Nodes.Add(nodoArbol);
                }
                else
                {
                    //TareaCompuesta tareaCompuesta = new TareaCompuesta(tarea);
                    TreeNode nodoArbol = new TreeNode(tarea.Nombre, GenerarNodoArbolTareaCompuesta((TareaCompuesta)tarea));
                    nodoArbol.ImageIndex = 0;
                    arbolDeTareas.Nodes.Add(nodoArbol);
                }
            }
        }

        private TreeNode GenerarNodoArbolTareaSimple(Tarea tarea)
        {
            TreeNode nodoArbol = new TreeNode();
            if (EsUnaTareaSimple(tarea))
            {
                nodoArbol = new TreeNode(tarea.Nombre);
            }
            else
            {
                nodoArbol = new TreeNode(tarea.Nombre);
            }

            return nodoArbol;
        }

        private TreeNode[] GenerarNodoArbolTareaCompuesta(TareaCompuesta tareaCompuesta)
        {
            TreeNode[] arbolNodos = new TreeNode[tareaCompuesta.Subtareas.Count];
            int i = 0;
            foreach(Tarea tarea in tareaCompuesta.Subtareas)
            {
                if (EsUnaTareaSimple(tarea))
                {
                    TreeNode hojaArbol = GenerarNodoArbolTareaSimple(tarea);
                    hojaArbol.ImageIndex = 1;
                    arbolNodos[i] = hojaArbol;
                }
                else
                {
                    TareaCompuesta tareaCompuestaHija = (TareaCompuesta)(tarea);
                    TreeNode[] ramaArbol = GenerarNodoArbolTareaCompuesta(tareaCompuestaHija);
                    TreeNode nodoSimple = new TreeNode(tareaCompuestaHija.Nombre, ramaArbol);
                    nodoSimple.ImageIndex = 0;
                    arbolNodos[i] = (nodoSimple);
                }
                i++;
            }
            return arbolNodos;
        }

        private static bool EsUnaTareaSimple(Tarea tarea)
        {
            return tarea.GetType() == typeof(TareaSimple);
        }
    }
}
