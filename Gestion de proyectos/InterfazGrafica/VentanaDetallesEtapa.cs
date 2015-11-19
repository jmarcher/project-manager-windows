using Dominio;
using InterfazGrafica.Utiles;
using System;
using System.Windows.Forms;
using PersistenciaImp;
using System.Collections.Generic;
using System.Drawing;
using DominioInterfaz;
using PersistenciaInterfaz;

namespace InterfazGrafica
{
    public partial class VentanaDetallesEtapa : Form
    {
        private const int ICONO_TAREA_COMPUESTA = 0;
        private const int ICONO_TAREA_SIMPLE = 1;
        private const string MOSTRAR_TAREAS_CAMINO_CRITICO = "Mostrar solo tareas del camino crítico";
        private const string MOSTRAR_TODAS_TAREAS = "Mostrar todas las tareas";
        private IEtapa etapa;
        private IContextoGestorProyectos contexto;
        private bool mostrandoCaminoCritico =false;
        public VentanaDetallesEtapa()
        {
            InitializeComponent();
        }

        public VentanaDetallesEtapa(int etapaID, IContextoGestorProyectos contexto)
        {
            InitializeComponent();
            this.contexto = contexto;
            etapa=contexto.ObtenerEtapa(etapaID);
            inicializarComponentes();
        }

        private void inicializarComponentes()
        {
            inicializarArbolTareas();
            this.Text = "Detalles de la etapa: " + etapa.Nombre;
            labelIdentifiacion.Text = etapa.EtapaID.ToString();
            textBoxNombre.Text = etapa.Nombre;
            dateTimePickerFechaInicio.Value = etapa.FechaInicio;
            textBoxFechaFin.Text = etapa.FechaFinalizacion.ToString();
            labelDuracionPendiente.Text = etapa.CalcularDuracionPendiente().ToString() + " días.";
            buttonEliminar.Enabled = false;
            if (fueCambiadaDuracionEstimada())
                textBoxDuracionEstimada.ReadOnly = true;
            textBoxDuracionEstimada.Text = etapa.DuracionEstimada.ToString();
            inicializarBotonAsignarAntecesora();
            actualizarArbolTareas();
            inicializarAvance();
        }

        private void inicializarAvance()
        {
            if (etapa.DuracionEstimada != 0)
            {
                labelAvance.Text = "Avance: " + calcularAvance().ToString() + "%";
                inicializarColorAvance();
            }
            else
                labelAvance.Text = String.Empty;
        }

        private void inicializarColorAvance()
        {
            if (calcularAvance() < 10)
            {
                labelAvance.ForeColor = Color.Red;
            }
            else if (calcularAvance() > 50)
            {
                labelAvance.ForeColor = Color.Orange;
            }
            else if (calcularAvance() > 80)
            {
                labelAvance.ForeColor = Color.Green;
            }
        }

        private int calcularAvance()
        {
            return ((DateTime.Now.Subtract(etapa.FechaInicio).Days * 100)
                            / etapa.DuracionEstimada) % 101;
        }

        private bool fueCambiadaDuracionEstimada()
        {
            return etapa.DuracionEstimada > 0;
        }

        private void inicializarBotonAsignarAntecesora()
        {
            if (etapa.Tareas.Count > 1 && HayTareaSeleccionada())
                buttonAsignarAntecesora.Enabled = true;
            else
                buttonAsignarAntecesora.Enabled = false;
        }

        private void inicializarArbolTareas()
        {
            arbolDeTareas.ImageList = listaImagenes;
        }

        private void actualizarArbolTareas()
        {
            arbolDeTareas.Nodes.Clear();
            List<Tarea> listaARecorrer = obtenerListaARecorrer();
            foreach (ITarea tarea in listaARecorrer)
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

        private List<Tarea> obtenerListaARecorrer()
        {
            if (mostrandoCaminoCritico)
                return etapa.ObtenerCaminoCritico();
            else
                return etapa.Tareas;
        }

        private static void AsignarIconosTareaCompuesta(TreeNode nodoArbol)
        {
            nodoArbol.ImageIndex = ICONO_TAREA_COMPUESTA;
            nodoArbol.SelectedImageIndex = ICONO_TAREA_COMPUESTA;
        }

        private TreeNode GenerarNodoArbolTareaSimple(ITarea tarea)
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

        private static String GenerarTextoAMostrar(ITarea tarea)
        {
            return tarea.ToString();
        }

        private TreeNode[] GenerarNodoArbolTareaCompuesta(TareaCompuesta tareaCompuesta)
        {
            TreeNode[] arbolNodos = new TreeNode[tareaCompuesta.Subtareas.Count];
            int posicion = 0;
            foreach (ITarea tarea in tareaCompuesta.Subtareas)
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

        private void AgregarSubArbolTareaSimple(TreeNode[] arbolNodos, int posicion, ITarea tarea)
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

        private static bool EsUnaTareaSimple(ITarea tarea)
        {
            return tarea.GetType() == typeof(TareaSimple) || tarea.GetType().BaseType == typeof(TareaSimple);
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
                    eliminarTareaDeEtapa();
                }
                else
                {
                    AyudanteVisual.CartelExclamacion("La tarea seleccionada no es una hija directa de la etapa actual.\n" +
                        "Para eliminar la tarea, deberá ir a la tarea padre y desde ahí eliminarla.", "No tiene permisos");
                }
            }
        }

        private void eliminarTareaDeEtapa()
        {
            if (ApretoSiEnMensajeDeConfirmacion())
            {
                etapa.EliminarTarea(TareaSeleccionada());
                contexto.ModificarEtapa(etapa);
                inicializarComponentes();
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
            habilitarBotonGuardar();
        }

        private void habilitarBotonGuardar()
        {
            if (algunCampoCambio())
            {
                buttonGuardar.Enabled = true;
            }
            else
            {
                buttonGuardar.Enabled = false;
            }
        }

        private bool algunCampoCambio()
        {
            return !esTextoIgualNombreEtapa() || noCambioFechaInicio() || cambioDuracionEstimada();
        }

        private bool cambioDuracionEstimada()
        {
            try {
                return Int32.Parse(textBoxDuracionEstimada.Text) != etapa.DuracionEstimada;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool noCambioFechaInicio()
        {
            return !(dateTimePickerFechaInicio.Value.Date.CompareTo(etapa.FechaInicio.Date) == 0);
        }

        private bool esTextoIgualNombreEtapa()
        {
            return textBoxNombre.Text.Equals(etapa.Nombre);
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            etapa.Nombre = textBoxNombre.Text;
            etapa.FechaInicio = dateTimePickerFechaInicio.Value;
            etapa.DuracionEstimada = Int32.Parse(textBoxDuracionEstimada.Text);
            contexto.ModificarEtapa(etapa);
            inicializarComponentes();
            buttonGuardar.Enabled = false;
            Close();
            
        }

        private void arbolDeTareas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HayTareaSeleccionada())
            {
                    editarTareaVentana(TareaSeleccionada(), false);
                
            }
        }

        private void editarTareaVentana(ITarea tarea , bool esNuevaTarea)
        {
            VentanaDetallesTarea ventanaDetalles = new VentanaDetallesTarea(tarea, esNuevaTarea, contexto);
            ventanaDetalles.ShowDialog(this);
            foreach (Form formulario in Application.OpenForms)
            {
                if (estaCerradaVentanaDetallesTarea(formulario))
                {
                    inicializarComponentes();
                    break;
                }
            }
        }

        private bool estaCerradaVentanaDetallesTarea(Form formulario)
        {
            return !(formulario.GetType() == typeof(VentanaDetallesTarea));
        }

        private void buttonAgregarTarea_Click(object sender, EventArgs e)
        {
            TareaSimple tarea = new TareaSimple(new ContextoGestorProyectos());
            etapa.AgregarTarea(tarea);
            contexto.ModificarEtapa(etapa);
            editarTareaVentana(tarea , true);
            inicializarComponentes();

        }

        private void dateTimePickerFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            habilitarBotonGuardar();
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
                    new VentanaAsignarAntecesoraVentanaDetallesEtapa(etapa, TareaSeleccionada(), contexto);
                ventana.ShowDialog();
            }
        }

        private void buttonAgregarTareaCompuesta_Click(object sender, EventArgs e)
        {
            TareaCompuesta tarea = new TareaCompuesta(new ContextoGestorProyectos());
            etapa.AgregarTarea(tarea);
            contexto.ModificarEtapa(etapa);
            editarTareaVentana(tarea, true);
            inicializarComponentes();
        }

        private void textBoxDuracionEstimada_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxDuracionEstimada_TextChanged(object sender, EventArgs e)
        {
            habilitarBotonGuardar();
        }

        private void buttonMostrarCaminoCritico_Click(object sender, EventArgs e)
        {
            if (mostrandoCaminoCritico)
            {
                mostrandoCaminoCritico = false;
                buttonMostrarCaminoCritico.Text = MOSTRAR_TAREAS_CAMINO_CRITICO;
            }
            else
            {
                mostrandoCaminoCritico = true;
                buttonMostrarCaminoCritico.Text = MOSTRAR_TODAS_TAREAS;
            }
            inicializarComponentes();
        }

        private void VentanaDetallesEtapa_Leave(object sender, EventArgs e)
        {
            
        }

        private void VentanaDetallesEtapa_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
