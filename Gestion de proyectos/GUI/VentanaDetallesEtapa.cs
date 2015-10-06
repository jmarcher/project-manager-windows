using Dominio;
using InterfazGrafica.Utiles;
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
            labelIdentifiacion.Text = etapa.Identificacion.ToString();
            textBoxNombre.Text = etapa.Nombre;
            textBoxFechaInicio.Text = etapa.FechaInicio.ToString();
            textBoxFechaFin.Text = etapa.FechaFinalizacion.ToString();
        }

        private void InicializarArbolTareas()
        {
            arbolDeTareas.ImageList = listaImagenes;
        }

        private void ActualizarArbolTareas()
        {
            arbolDeTareas.Nodes.Clear();
            foreach (Tarea tarea in etapa.Tareas)
            {
                if (EsUnaTareaSimple(tarea))
                {
                    TreeNode nodoArbol = GenerarNodoArbolTareaSimple(tarea);
                    arbolDeTareas.Nodes.Add(nodoArbol);
                }
                else
                {
                    TreeNode nodoArbol = new TreeNode(GenerarTextoAMostrar(tarea), GenerarNodoArbolTareaCompuesta((TareaCompuesta)tarea));
                    AsignarIconosTareaCompuesta(nodoArbol);
                    arbolDeTareas.Nodes.Add(nodoArbol);
                }
            }
        }

        private static void AsignarIconosTareaCompuesta(TreeNode nodoArbol)
        {
            nodoArbol.ImageIndex = ICONO_TAREA_COMPUESTA;
            nodoArbol.SelectedImageIndex = ICONO_TAREA_COMPUESTA;
        }

        private TreeNode GenerarNodoArbolTareaSimple(Tarea tarea)
        {
            TreeNode nodoArbol = new TreeNode(GenerarTextoAMostrar(tarea));
            AsignarIconosTareaSimple(nodoArbol);
            return nodoArbol;
        }

        private static void AsignarIconosTareaSimple(TreeNode nodoArbol)
        {
            nodoArbol.ImageIndex = ICONO_TAREA_SIMPLE;
            nodoArbol.SelectedImageIndex = ICONO_TAREA_SIMPLE;
        }

        private static String GenerarTextoAMostrar(Tarea tarea)
        {
            StringBuilder valorRetorno = new StringBuilder();
            valorRetorno.Append(tarea.Nombre);
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
            foreach (Tarea tarea in tareaCompuesta.Subtareas)
            {
                if (EsUnaTareaSimple(tarea))
                {
                    AgregarSubArbolTareaSimple(arbolNodos, posicion, tarea);
                }
                else
                {
                    AgregarSubArbolTareaCompleja(arbolNodos, posicion, (TareaCompuesta)tarea);
                }
                posicion++;
            }
            return arbolNodos;
        }

        private void AgregarSubArbolTareaSimple(TreeNode[] arbolNodos, int posicion, Tarea tarea)
        {
            TreeNode hojaArbol = GenerarNodoArbolTareaSimple(tarea);
            AsignarIconosTareaSimple(hojaArbol);
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
            AsignarIconosTareaCompuesta(nodoSimple);
            return nodoSimple;
        }

        private static bool EsUnaTareaSimple(Tarea tarea)
        {
            return tarea.GetType() == typeof(TareaSimple);
        }

        private bool HayTareaSeleccionada()
        {
            return arbolDeTareas.SelectedNode != null;
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (HayTareaSeleccionada())
            {
                if (AyudanteVisual.CartelConfirmacion(MensajeConfirmacionEliminacionTarea(), "Eliminación"))
                {
                    etapa.EliminarTarea(TareaSeleccionada());
                    ActualizarArbolTareas();
                }
            }
        }

        private string MensajeConfirmacionEliminacionTarea()
        {
            if (TareaSeleccionada().GetType() == typeof(TareaSimple))
            {
                return "¿Seguro desea eliminar esta tarea?";
            }
            else
            {
                return "¿Seguro desea eliminar esta tarea compuesta?\nEsto eliminará la tarea con todas sus subtareas.";
            }
        }

        private Tarea TareaSeleccionada()
        {

            return etapa.Tareas[arbolDeTareas.SelectedNode.Index];
        }
    }
}
