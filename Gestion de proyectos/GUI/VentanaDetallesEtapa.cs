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
            ActualizarArbolTareas();
        }

        private void ActualizarArbolTareas()
        {
            foreach(Tarea tarea in etapa.Tareas)
            {
                if (EsUnaTareaSimple(tarea))
                {
                    arbolDeTareas.Nodes.Add(GenerarNodoArbolTareaSimple(tarea));
                }
                else
                {
                    //TareaCompuesta tareaCompuesta = new TareaCompuesta(tarea);
                    TreeNode nodoArbol = new TreeNode(tarea.Nombre, GenerarNodoArbolTareaCompuesta((TareaCompuesta)tarea));
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
                    arbolNodos[i] = hojaArbol;
                }
                else
                {
                    TareaCompuesta tareaCompuestaHija = new TareaCompuesta(tarea);
                    TreeNode[] ramaArbol = GenerarNodoArbolTareaCompuesta(tareaCompuestaHija);
                    Console.WriteLine(tareaCompuestaHija.Nombre + " cant subtareas " + tareaCompuestaHija.Subtareas.Count);
                    TreeNode nodoSimple = new TreeNode(tareaCompuestaHija.Nombre, ramaArbol);
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
