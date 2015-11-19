using Dominio;
using InterfazGrafica.Utiles;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using PersistenciaImp;
using System.Drawing.Imaging;
using System.Drawing;
using DominioInterfaz;
using PersistenciaInterfaz;

namespace InterfazGrafica
{
    public partial class VentanaDetallesProyecto : Form
    {
        private IProyecto proyecto;
        private int proyectoID;
        private OrdenadorListView ordenadorListView;
        private IContextoGestorProyectos Contexto;

        public VentanaDetallesProyecto(int proyectoID, IContextoGestorProyectos contexto)
        {
            this.Contexto = contexto;
            this.proyectoID = proyectoID;
            InitializeComponent();
            inicializarControles();
        }

        private void inicializarControles()
        {
            inicializarListViewSorter();
            actualizarListaEtapas();
            asignarTitulo();
            inicializarLista();
            inicializarCampos();
            inicializarAvance();
        }

        private void inicializarAvance()
        {
            if (proyecto.DuracionEstimada != 0)
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
            }else if (calcularAvance() > 80)
            {
                labelAvance.ForeColor = Color.Green;
            }
        }

        private int calcularAvance()
        {
            return ((DateTime.Now.Subtract(proyecto.FechaInicio).Days * 100)
                            / proyecto.DuracionEstimada) % 101;
        }

        private void inicializarCampos()
        {
            textBoxFFin.Text = proyecto.FechaFinalizacion.Date.ToString();
            dateTimePickerFechaInicio.Text = proyecto.FechaInicio.Date.ToString();
            textBoxObjetivo.Text = proyecto.Objetivo;
            textBoxNombre.Text = proyecto.Nombre;
            labelIdentificacion.Text = proyecto.ProyectoID.ToString();
            labelDuracionPendiente.Text = proyecto.CalcularDuracionPendiente().ToString() + " días";
            labelDuracionEstimada.Text = proyecto.DuracionEstimada.ToString() + " días";
        }

        private void inicializarListViewSorter()
        {
            ordenadorListView = new OrdenadorListView();
            etapasListView.ListViewItemSorter = ordenadorListView;
        }

        private void asignarTitulo()
        {
            this.Text = "Etapas de: " + proyecto.Nombre;
        }

        private void actualizarListaEtapas()
        {
            etapasListView.Items.Clear();
            proyecto = Contexto.ObtenerProyecto(proyectoID);
            foreach (IEtapa etapa in proyecto.Etapas)
            {
                ListViewItem elementoListView = CrearNuevoItemListView(etapa);
                etapasListView.Items.Add(elementoListView);
                
            }
        }

        private static ListViewItem CrearNuevoItemListView(IEtapa etapa)
        {
            ListViewItem elementoListView = new ListViewItem();
            elementoListView.Text = (etapa.EtapaID) + "";
            elementoListView.SubItems[0].Tag = OrdenadorListView.INT;
            elementoListView.SubItems.Add(etapa.Nombre).Tag = OrdenadorListView.STRING;
            elementoListView.SubItems.Add(etapa.CalcularDuracionPendiente().ToString()).Tag = OrdenadorListView.INT;
            elementoListView.SubItems.Add(etapa.FechaInicio.ToString()).Tag = OrdenadorListView.DATETIME;
            elementoListView.SubItems.Add(etapa.FechaFinalizacion.ToString()).Tag = OrdenadorListView.DATETIME;
            return elementoListView;
        }

        private void inicializarLista()
        {
            etapasListView.View = View.Details;
            etapasListView.FullRowSelect = true;
            etapasListView.GridLines = true;
            etapasListView.Sorting = SortOrder.Ascending;
            etapasListView.Columns.Add("Id", 40, HorizontalAlignment.Left);
            etapasListView.Columns.Add("Nombre", 100, HorizontalAlignment.Left);
            etapasListView.Columns.Add("Duracion pendiente (días)", 50, HorizontalAlignment.Left);
            etapasListView.Columns.Add("Fecha inicio", 140, HorizontalAlignment.Left);
            etapasListView.Columns.Add("Fecha finalización", 140, HorizontalAlignment.Left);
        }

        private void etapasListView_ColumnClick(object remitente, ColumnClickEventArgs evento)
        {
            ordenadorListView.HizoClickEnColumna(evento);
            etapasListView.Sort();
            etapasListView.AsignarIconoColumna(ordenadorListView.OrdenarColumna, ordenadorListView.Orden);
        }

        private void eliminarButton_Click(object sender, EventArgs e)
        {
            if (HayEtapaSeleccionada() && CartelConfirmacionEliminacionAceptado())
            {
                proyecto.QuitarEtapa(EtapaSeleccionada());
                actualizarListaEtapas();
                guardarCambiosProyecto();
            }
        }

        private void guardarCambiosProyecto()
        {
            Contexto.ModificarProyecto(proyecto);
            
        }

        private Etapa EtapaSeleccionada()
        {
            return proyecto.Etapas.Find(x => x.EtapaID == DevolverIdentificadorSeleccionado());
        }

        private bool CartelConfirmacionEliminacionAceptado()
        {
            return AyudanteVisual.CartelConfirmacion("¿Seguro desea eliminar esta etapa?",
                "Eliminar etapa");
        }

        private int DevolverIdentificadorSeleccionado()
        {
            return Int32.Parse(etapasListView.SelectedItems[0].Text);
        }

        private bool HayEtapaSeleccionada()
        {
            return etapasListView.SelectedItems.Count > 0;
        }

        private void VentanaDetallesProyecto_Load(object sender, EventArgs e)
        {
            etapasListView_ColumnClick(null, new ColumnClickEventArgs(0));
        }

        private void etapasListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HayEtapaSeleccionada())
            {
                editarEtapaVentana(EtapaSeleccionada());
            }
        }

        private void editarEtapaVentana(IEtapa etapa)
        {
            VentanaDetallesEtapa ventanaDetalles = new VentanaDetallesEtapa(etapa.EtapaID,Contexto);
            ventanaDetalles.ShowDialog(this);
            foreach (Form formulario in Application.OpenForms)
            {
                if (estaCerradaVentanaDetallesEtapa(formulario))
                {
                    actualizarListaEtapas();
                    break;
                }
            }
        }

        private bool estaCerradaVentanaDetallesEtapa(Form formulario)
        {
            return !(formulario.GetType() == typeof(VentanaDetallesEtapa));
        }

        private bool AlgunCampoCambio()
        {
            return CambioNombreProyecto() || CambioObjetivoProyecto() || CambioFechaProyecto();
        }

        private bool CambioFechaProyecto()
        {
            return !(dateTimePickerFechaInicio.Value.Date.Year == proyecto.FechaInicio.Year &&
                dateTimePickerFechaInicio.Value.Date.Month == proyecto.FechaInicio.Month &&
                dateTimePickerFechaInicio.Value.Date.Day == proyecto.FechaInicio.Day);
        }

        private bool CambioObjetivoProyecto()
        {
            return !textBoxObjetivo.Text.Equals(proyecto.Objetivo);
        }

        private bool CambioNombreProyecto()
        {
            return !textBoxNombre.Text.Equals(proyecto.Nombre);
        }

        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
            HabilitarBotonGuardarSiHayCambios();
        }

        private void HabilitarBotonGuardarSiHayCambios()
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

        private void textBoxObjetivo_TextChanged(object sender, EventArgs e)
        {
            HabilitarBotonGuardarSiHayCambios();
        }

        private void dateTimePickerFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            HabilitarBotonGuardarSiHayCambios();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (!fechaDeInicioValida())
            {
                mostrarMensajeFechaInvalida();
            }
            else
            {
                if (textBoxNombre.Text.Trim().Equals(String.Empty))
                {
                    AyudanteVisual.CartelExclamacion("El nombre no puede ser vacío.", "Campo inválido");
                }
                else
                {
                    if (textBoxObjetivo.Text.Trim().Equals(String.Empty))
                    {
                        AyudanteVisual.CartelExclamacion("El objetivo no puede ser vacío.", "Campo inválido");
                    }
                    else
                        asignarCamposProyecto();
                }
            }
        }

        private void asignarCamposProyecto()
        {
            proyecto.Nombre = textBoxNombre.Text;
            proyecto.Objetivo = textBoxObjetivo.Text;
            proyecto.FechaInicio = dateTimePickerFechaInicio.Value;
            buttonGuardar.Enabled = false;
            guardarCambiosProyecto();
            Close();
        }

        private void mostrarMensajeFechaInvalida()
        {
            AyudanteVisual.CartelExclamacion("La fecha de inicio es inválida,"
                                + " un proyecto no puede empezar luego que una de sus etapas.",
                                "Fecha de inicio inválida.");
            inicializarCampos();
        }

        private bool fechaDeInicioValida()
        {
            foreach(IEtapa etapa in proyecto.Etapas)
            {
                if(etapa.FechaInicio < dateTimePickerFechaInicio.Value)
                {
                    return false;
                }
            }
            return true;
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            Etapa etapa = new Etapa() { FechaInicio = proyecto.FechaInicio };
            proyecto.AgregarEtapa(etapa);
            guardarCambiosProyecto();
            editarEtapaVentana(etapa);
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            VentanaVerHistorialProyecto ventanaHistorial = new VentanaVerHistorialProyecto(proyecto);
            ventanaHistorial.ShowDialog(this);
        }

        private void VentanaDetallesProyecto_Leave(object sender, EventArgs e)
        {
            
        }

        private void VentanaDetallesProyecto_FormClosing(object sender, FormClosingEventArgs e)
        { 
        }
    }
}
