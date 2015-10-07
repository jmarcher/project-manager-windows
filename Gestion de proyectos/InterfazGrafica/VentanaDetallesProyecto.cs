using Dominio;
using InterfazGrafica.Utiles;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class VentanaDetallesProyecto : Form
    {
        private Proyecto proyecto;
        private OrdenadorColumnaListView ordenadorListView;

        public VentanaDetallesProyecto(Proyecto proyecto)
        {
            this.proyecto = proyecto;
            InitializeComponent();
            InicializarControles();
        }

        private void InicializarControles()
        {
            InicializarListViewSorter();
            AsignarTitulo();
            InicializarLista();
            ActualizarLista();
            InicializarCampos();
        }

        private void InicializarCampos()
        {
            textBoxFFin.Text = proyecto.FechaFinalizacion.Date.ToString();
            dateTimePickerFechaInicio.Text = proyecto.FechaInicio.Date.ToString();
            textBoxObjetivo.Text = proyecto.Objetivo;
            textBoxNombre.Text = proyecto.Nombre;
            labelIdentificacion.Text = proyecto.Identificador.ToString();
            labelDuracionPendiente.Text = proyecto.CalcularDuracionPendiente().ToString() + " días";
        }

        private void InicializarListViewSorter()
        {
            ordenadorListView = new OrdenadorColumnaListView();
            etapasListView.ListViewItemSorter = ordenadorListView;
        }

        private void AsignarTitulo()
        {
            this.Text = "Etapas de: " + proyecto.Nombre;
        }

        private void ActualizarLista()
        {
            etapasListView.Items.Clear();
            foreach (Etapa etapa in proyecto.Etapas)
            {
                ListViewItem elementoListView = CrearNuevoItemListView(etapa);
                etapasListView.Items.Add(elementoListView);
                
            }
        }

        private static ListViewItem CrearNuevoItemListView(Etapa etapa)
        {
            ListViewItem elementoListView = new ListViewItem();
            elementoListView.Text = (etapa.Identificacion) + "";
            elementoListView.SubItems[0].Tag = "int";
            elementoListView.SubItems.Add(etapa.Nombre).Tag = "string";
            elementoListView.SubItems.Add(etapa.CalcularDuracionPendiente().ToString()).Tag = "int";
            elementoListView.SubItems.Add(etapa.FechaInicio.ToString()).Tag = "DateTime";
            elementoListView.SubItems.Add(etapa.FechaFinalizacion.ToString()).Tag = "DateTime";
            return elementoListView;
        }

        private void InicializarLista()
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
            if (evento.Column == ordenadorListView.OrdenarColumna)
            {
                if (ordenadorListView.Orden == SortOrder.Ascending)
                {
                    ordenadorListView.Orden = SortOrder.Descending;
                }
                else
                {
                    ordenadorListView.Orden = SortOrder.Ascending;
                }
            }
            else
            {
                ordenadorListView.OrdenarColumna = evento.Column;
                ordenadorListView.Orden = SortOrder.Ascending;
            }

            etapasListView.Sort();
            etapasListView.AsignarIconoColumna(ordenadorListView.OrdenarColumna, ordenadorListView.Orden);
        }

        private void eliminarButton_Click(object sender, EventArgs e)
        {
            if (HayEtapaSeleccionada() && CartelConfirmacionEliminacionAceptado())
            {
                proyecto.QuitarEtapa(EtapaSeleccionada());
                ActualizarLista();
            }
        }

        private Etapa EtapaSeleccionada()
        {
            return proyecto.Etapas.Find(x => x.Identificacion == DevolverIdentificadorSeleccionado());
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

        private void editarButton_Click(object sender, EventArgs e)
        {
            if (HayEtapaSeleccionada())
            {
                VentanaEtapa ventanaEtapa = new VentanaEtapa(EtapaSeleccionada());
                ventanaEtapa.Show();
            }
        }

        private void etapasListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HayEtapaSeleccionada())
            {
                VentanaDetallesEtapa ventanaDetalles = new VentanaDetallesEtapa(EtapaSeleccionada());
                ventanaDetalles.ShowDialog(this);
                foreach (Form formulario in Application.OpenForms)
                {
                    if (EstaCerradaVentanaDetallesEtapa(formulario))
                    {
                        ActualizarLista();
                        break;
                    }
                }
            }
        }

        private bool EstaCerradaVentanaDetallesEtapa(Form formulario)
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
            proyecto.Nombre = textBoxNombre.Text;
            proyecto.Objetivo = textBoxObjetivo.Text;
            proyecto.FechaInicio = dateTimePickerFechaInicio.Value;
            buttonGuardar.Enabled = false;
        }
    }
}
