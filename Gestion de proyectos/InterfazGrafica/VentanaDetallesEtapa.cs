using Dominio;
using InterfazGrafica.Utiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            labelIdentifiacion.Text = etapa.EtapaID.ToString();
            textBoxNombre.Text = etapa.Nombre;
            dateTimePickerFechaInicio.Value = etapa.FechaInicio;
            textBoxFechaFin.Text = etapa.FechaFinalizacion.ToString();
            labelDuracionPendiente.Text = etapa.CalcularDuracionPendiente().ToString() + " días.";
            buttonEliminar.Enabled = false;
            inicializarBotonAsignarAntecesora();
        }

        private void inicializarBotonAsignarAntecesora()
        {
            if (etapa.Tareas.Count > 1 && HayTareaSeleccionada())
                buttonAsignarAntecesora.Enabled = true;
            else
                buttonAsignarAntecesora.Enabled = false;
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
                    nodoArbol.Tag = tarea;
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
            nodoArbol.Tag = tarea;
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
            return tarea.ToString();
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
            hojaArbol.Tag = tarea;
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
            nodoSimple.Tag = tareaCompuesta;
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
                if (EstaTareaSeleccionadaEnEtapa())
                {
                    EliminarTareaDeEtapa();
                }
                else
                {
                    AyudanteVisual.CartelExclamacion("La tarea seleccionada no es una hija directa de la etapa actual.\n" +
                        "Para eliminar la tarea, deberá ir a la tarea padre y desde ahí eliminarla.", "No tiene permisos");
                }
            }
        }

        private void EliminarTareaDeEtapa()
        {
            if (ApretoSiEnMensajeDeConfirmacion())
            {
                etapa.EliminarTarea(TareaSeleccionada());
                ActualizarArbolTareas();
            }
        }

        private bool EstaTareaSeleccionadaEnEtapa()
        {
            return etapa.Tareas.Contains(TareaSeleccionada());
        }

        private bool ApretoSiEnMensajeDeConfirmacion()
        {
            return AyudanteVisual.CartelConfirmacion(MensajeConfirmacionEliminacionTarea(), "Eliminación");
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
           return (Tarea)arbolDeTareas.SelectedNode.Tag;
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            HabilitarBotonGuardar();
        }

        private void HabilitarBotonGuardar()
        {
            if (AlgunCampoCambio())
            {
                buttonGuardar.Enabled = true;
            }
            else
            {
                buttonGuardar.Enabled = false;
            }
        }

        private bool AlgunCampoCambio()
        {
            return !EsTextoIgualNombreEtapa() || NoCambioFechaInicio();
        }

        private bool NoCambioFechaInicio()
        {
            return !(dateTimePickerFechaInicio.Value.Date.CompareTo(etapa.FechaInicio.Date) == 0);
        }

        private bool EsTextoIgualNombreEtapa()
        {
            return textBoxNombre.Text.Equals(etapa.Nombre);
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            etapa.Nombre = textBoxNombre.Text;
            etapa.FechaInicio = dateTimePickerFechaInicio.Value;
            InicializarComponentes();
            buttonGuardar.Enabled = false;
            
        }

        private void arbolDeTareas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HayTareaSeleccionada())
            {
                if(TareaSeleccionada().GetType() == typeof(TareaCompuesta))
                {
                    EditarTareaVentana((TareaCompuesta)TareaSeleccionada(), false);
                }
                else
                {
                    EditarTareaVentana((TareaSimple)TareaSeleccionada(), false);
                }
                
            }
        }

        private void EditarTareaVentana(Tarea tarea , bool esNuevaTarea)
        {
            VentanaDetallesTarea ventanaDetalles = new VentanaDetallesTarea(tarea, esNuevaTarea);
            ventanaDetalles.ShowDialog(this);
            foreach (Form formulario in Application.OpenForms)
            {
                if (EstaCerradaVentanaDetallesTarea(formulario))
                {
                    ActualizarArbolTareas();
                    break;
                }
            }
        }

        private bool EstaCerradaVentanaDetallesTarea(Form formulario)
        {
            return !(formulario.GetType() == typeof(VentanaDetallesTarea));
        }

        private void buttonAgregarTarea_Click(object sender, EventArgs e)
        {
            TareaSimple tarea = new TareaSimple();
            etapa.AgregarTarea(tarea);
            EditarTareaVentana(tarea , true);
            ActualizarArbolTareas();

        }

        private void dateTimePickerFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            HabilitarBotonGuardar();
        }

        private void arbolDeTareas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (HayTareaSeleccionada())
            {
                buttonEliminar.Enabled = true;
                inicializarBotonAsignarAntecesora();
            }
        }

        private void VentanaDetallesEtapa_Load(object sender, EventArgs e)
        {

        }

        private void buttonAsignarAntecesora_Click(object sender, EventArgs e)
        {
            if (HayTareaSeleccionada())
            {
                VentanaAsignarAntecesoraVentanaDetallesEtapa ventana =
                    new VentanaAsignarAntecesoraVentanaDetallesEtapa(etapa, TareaSeleccionada());
                ventana.ShowDialog();
            }
        }

        private void buttonAgregarTareaCompuesta_Click(object sender, EventArgs e)
        {
            TareaCompuesta tarea = new TareaCompuesta();
            etapa.AgregarTarea(tarea);
            EditarTareaVentana(tarea, true);
            ActualizarArbolTareas();
        }
    }
}
