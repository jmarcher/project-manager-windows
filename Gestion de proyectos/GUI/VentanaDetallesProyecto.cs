using Dominio;
using InterfazGrafica.Utiles;
using System;
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
            elementoListView.SubItems.Add(etapa.Tareas.Count.ToString()).Tag = "int";
            elementoListView.SubItems.Add(etapa.EstaFinalizada ? etapa.FechaFinalizacion.ToString() : "").Tag = "DateTime";
            return elementoListView;
        }

        private void InicializarLista()
        {
            //propiedades de la listview
            etapasListView.View = View.Details;
            etapasListView.FullRowSelect = true;
            etapasListView.GridLines = true;
            etapasListView.Sorting = SortOrder.Ascending;
            etapasListView.Columns.Add("Id", 40, HorizontalAlignment.Left);
            etapasListView.Columns.Add("Nombre", 100, HorizontalAlignment.Left);
            etapasListView.Columns.Add("Cant. Tareas", 50, HorizontalAlignment.Left);
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
    }
}
