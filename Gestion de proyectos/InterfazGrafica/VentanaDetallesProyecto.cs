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
        private OrdenadorListView ordenadorListView;

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
            ordenadorListView = new OrdenadorListView();
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
            elementoListView.SubItems[0].Tag = OrdenadorListView.INT;
            elementoListView.SubItems.Add(etapa.Nombre).Tag = OrdenadorListView.STRING;
            elementoListView.SubItems.Add(etapa.CalcularDuracionPendiente().ToString()).Tag = OrdenadorListView.INT;
            elementoListView.SubItems.Add(etapa.FechaInicio.ToString()).Tag = OrdenadorListView.DATETIME;
            elementoListView.SubItems.Add(etapa.FechaFinalizacion.ToString()).Tag = OrdenadorListView.DATETIME;
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

        private void etapasListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (HayEtapaSeleccionada())
            {
                EditarEtapaVentana(EtapaSeleccionada());
            }
        }

        private void EditarEtapaVentana(Etapa etapa)
        {
            VentanaDetallesEtapa ventanaDetalles = new VentanaDetallesEtapa(etapa);
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
            if (!fechaDeInicioValida())
            {
                AyudanteVisual.CartelExclamacion("La fecha de inicio es inválida,"
                    +" un proyecto no puede empezar luego que una de sus etapas.",
                    "Fecha de inicio inválida.");
                InicializarCampos();
            }
            else
            { 
                proyecto.Nombre = textBoxNombre.Text;
                proyecto.Objetivo = textBoxObjetivo.Text;
                proyecto.FechaInicio = dateTimePickerFechaInicio.Value;
                buttonGuardar.Enabled = false;
             }
        }

        private bool fechaDeInicioValida()
        {
            foreach(Etapa etapa in proyecto.Etapas)
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
            Etapa etapa = new Etapa()
            {
                Identificacion = ObtenerSiguienteIdEtapa()
            };
            proyecto.AgregarEtapa(etapa);
            EditarEtapaVentana(etapa);
            
        }

        private int ObtenerSiguienteIdEtapa()
        {
            int mayorId = -1;
            foreach(Etapa etapa in proyecto.Etapas)
            {
                if(etapa.Identificacion > mayorId)
                {
                    mayorId = etapa.Identificacion;
                }
            }
            return mayorId + 1;
        }
    }
}
