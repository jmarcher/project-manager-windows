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
        private const int ICONO_TAREA_COMPUESTA = 0;
        private const int ICONO_TAREA_SIMPLE = 1;
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
            InicializarArbolTareas();
            this.Text = "Detalles de la etapa: " + etapa.Nombre;
        }

        private void InicializarArbolTareas()
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
                    arbolDeTareas.Nodes.Add(nodoArbol);
                }
                else
                {
                    TreeNode nodoArbol = new TreeNode(GenerarTextoAMostrar(tarea), GenerarNodoArbolTareaCompuesta((TareaCompuesta)tarea));
                    nodoArbol.ImageIndex = ICONO_TAREA_COMPUESTA;
                    nodoArbol.SelectedImageIndex = ICONO_TAREA_COMPUESTA;
                    arbolDeTareas.Nodes.Add(nodoArbol);
                }
            }
        }

        private TreeNode GenerarNodoArbolTareaSimple(Tarea tarea)
        {
            TreeNode nodoArbol = new TreeNode(GenerarTextoAMostrar(tarea));
            nodoArbol.ImageIndex = ICONO_TAREA_SIMPLE;
            nodoArbol.SelectedImageIndex = ICONO_TAREA_SIMPLE;
            return nodoArbol;
        }

        private static String GenerarTextoAMostrar(Tarea tarea)
        {
            StringBuilder valorRetorno = new StringBuilder();
            valorRetorno.Append( tarea.Nombre);
            valorRetorno.Append(" [Prioridad: ");
            valorRetorno.Append(tarea.Prioridad);
            valorRetorno.Append(", Inicio: ");
            valorRetorno.Append(tarea.FechaInicio);
            valorRetorno.Append(", Fin: ");
            valorRetorno.Append(tarea.FechaFinalizacion);
            if (!EsUnaTareaSimple(tarea))
            {
                TareaCompuesta tareaCompuesta = (TareaCompuesta)tarea;
                valorRetorno.Append(", Subtareas: ");
                valorRetorno.Append(tareaCompuesta.Subtareas.Count.ToString());
            }
            valorRetorno.Append("]");
            return valorRetorno.ToString();
        }

        private TreeNode[] GenerarNodoArbolTareaCompuesta(TareaCompuesta tareaCompuesta)
        {
            TreeNode[] arbolNodos = new TreeNode[tareaCompuesta.Subtareas.Count];
            int posicion = 0;
            foreach(Tarea tarea in tareaCompuesta.Subtareas)
            {
                if (EsUnaTareaSimple(tarea))
                {
                    AgregarSubArbolTareaSimple(arbolNodos, posicion, tarea);
                }
                else
                {
                    AgregarSubArbolTareaCompleja(arbolNodos, posicion,(TareaCompuesta) tarea);
                }
                posicion++;
            }
            return arbolNodos;
        }

        private void AgregarSubArbolTareaSimple(TreeNode[] arbolNodos, int posicion, Tarea tarea)
        {
            TreeNode hojaArbol = GenerarNodoArbolTareaSimple(tarea);
            hojaArbol.ImageIndex = ICONO_TAREA_SIMPLE;
            hojaArbol.SelectedImageIndex = ICONO_TAREA_SIMPLE;
            arbolNodos[posicion] = hojaArbol;
        }

        private void AgregarSubArbolTareaCompleja(TreeNode[] arbolNodos, int posicion, TareaCompuesta tarea)
        {
            TreeNode nodoSimple = GenerarNodoTareaCompleja(tarea);
            arbolNodos[posicion] = (nodoSimple);
        }

        private TreeNode GenerarNodoTareaCompleja(TareaCompuesta tareaCompuesta)
        {
            TreeNode nodoSimple = new TreeNode(GenerarTextoAMostrar(tareaCompuesta),
                GenerarNodoArbolTareaCompuesta(tareaCompuesta));
            nodoSimple.ImageIndex = ICONO_TAREA_COMPUESTA;
            nodoSimple.SelectedImageIndex = ICONO_TAREA_COMPUESTA;
            return nodoSimple;
        }

        private static bool EsUnaTareaSimple(Tarea tarea)
        {
            return tarea.GetType() == typeof(TareaSimple);
        }
    }
}
