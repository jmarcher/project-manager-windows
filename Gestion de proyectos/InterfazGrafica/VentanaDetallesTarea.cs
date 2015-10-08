using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
namespace InterfazGrafica
{
    public partial class VentanaDetallesTarea : Form
    {
        private const int ICONO_TAREA_COMPUESTA = 0;
        private const int ICONO_TAREA_SIMPLE = 1;

        private Tarea tarea;
        public VentanaDetallesTarea()
        {
            InitializeComponent();
        }

        public VentanaDetallesTarea(Tarea tarea)
        {
            InitializeComponent();
            this.tarea = tarea;
            InicializarComponentes(tarea);
        }

        private void InicializarComponentes(Tarea tarea)
        {
            textBoxNombre.Text = tarea.Nombre;
            textBoxObjetivo.Text = tarea.Objetivo;
            textBoxDuracionPendiente.Text = tarea.CalcularDuracionPendiente().ToString();
            textBoxDescripcion.Text = tarea.Descripcion;
            dateTimePickerFechaInicio.Value = tarea.FechaInicio;
            dateTimePickerFechaFinalizacion.Value = tarea.FechaFinalizacion;
            comboBoxPrioridad.SelectedIndex = tarea.Prioridad;
            DeshabilitarFechaFinalizacionParaTareaCompuesta();

            InicializarListViewAntecesoras();
            InicializarArbolSubtareas();
        }

        private void InicializarArbolSubtareas()
        {
            if (EsCompuesta(tarea))
            {
                PopularArbolConNodosSubtareas();
            }
            else
            {
                DeshabilitarArbolSubtareas();
            }
        }

        private void DeshabilitarArbolSubtareas()
        {
            treeViewSubtareas.Enabled = false;
        }

        private void PopularArbolConNodosSubtareas()
        {
            foreach (Tarea tarea in ((TareaCompuesta)tarea).Subtareas)
            {
                treeViewSubtareas.Nodes.Add(GenerarNodoArbol(tarea));
            }
        }

        private TreeNode GenerarNodoArbol(Tarea tarea)
        {
            TreeNode nodoRetorno = new TreeNode();
            if (EsCompuesta(tarea))
            {
                nodoRetorno = GenerarNodoArbolTareaCompuesta((TareaCompuesta)tarea);
            }
            else
            {
                nodoRetorno = GenerarNodoArbolTareaSimple(tarea);
            }
            return nodoRetorno;
        }

        private TreeNode[] GenerarArregloNodosListaTareas(List<Tarea> tareas)
        {
            TreeNode[] rama = new TreeNode[tareas.Count];
            int indice = 0;
            foreach(Tarea tarea in tareas)
            {
                if (EsCompuesta(tarea))
                {
                    rama[indice] = GenerarNodoArbolTareaCompuesta((TareaCompuesta)tarea);
                }
                else
                {
                    rama[indice] = GenerarNodoArbolTareaSimple(tarea);
                }
                indice++;
            }
            return rama; 
        }

        private TreeNode GenerarNodoArbolTareaCompuesta(TareaCompuesta tarea)
        {
            TreeNode nodoRetorno = new TreeNode(GenerarTextoAMostrar(tarea),
                GenerarArregloNodosListaTareas(tarea.Subtareas));
            nodoRetorno.Tag = tarea;
            AsignarIconosTareaCompuesta(nodoRetorno);
            return nodoRetorno;
        }

        private void AsignarIconosTareaCompuesta(TreeNode nodoRetorno)
        {
            nodoRetorno.ImageIndex = ICONO_TAREA_COMPUESTA;
            nodoRetorno.SelectedImageIndex = ICONO_TAREA_COMPUESTA;
        }

        private TreeNode GenerarNodoArbolTareaSimple(Tarea tarea)
        {
            TreeNode nodoRetorno = new TreeNode(GenerarTextoAMostrar(tarea));
            nodoRetorno.Tag = tarea;
            AsignarIconosTareaSimple(nodoRetorno);
            return nodoRetorno;
        }



        private static void AsignarIconosTareaSimple(TreeNode nodoArbol)
        {
            nodoArbol.ImageIndex = ICONO_TAREA_SIMPLE;
            nodoArbol.SelectedImageIndex = ICONO_TAREA_SIMPLE;
        }
        private string GenerarTextoAMostrar(Tarea tarea)
        {
            return tarea.ToString();
        }

        private void DeshabilitarFechaFinalizacionParaTareaCompuesta()
        {
            if (EsCompuesta(tarea))
            {
                dateTimePickerFechaFinalizacion.Enabled = false;
            }
        }

        private void InicializarListViewAntecesoras()
        {
            foreach (Tarea tarea in tarea.Antecesoras)
            {
                ArgegarElemento(tarea);
            }
        }

        private void ArgegarElemento(Tarea tarea)
        {
            ListViewItem elementoLista = new ListViewItem(tarea.ToString());
            elementoLista.ImageIndex = IconoTarea(tarea);
            listViewAntecesoras.Items.Add(elementoLista);
        }

        private int IconoTarea(Tarea tarea)
        {
            if (EsCompuesta(tarea))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private bool EsCompuesta(Tarea tarea)
        {
            return tarea.GetType() == typeof(TareaCompuesta);
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}
