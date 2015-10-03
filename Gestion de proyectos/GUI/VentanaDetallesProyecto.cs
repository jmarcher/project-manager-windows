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
    public partial class VentanaDetallesProyecto : Form
    {
        private Proyecto proyecto;
        private OrdenadorColumnaListView lvwColumnSorter;

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
            lvwColumnSorter = new OrdenadorColumnaListView();
            etapasListView.ListViewItemSorter = lvwColumnSorter;
        }

        private void AsignarTitulo()
        {
            this.Text = "Etapas de: " + proyecto.Nombre;
        }

        private void ActualizarLista()
        {
            etapasListView.Items.Clear();
            foreach (Etapa e in proyecto.Etapas)
            {
                ListViewItem lvi = CrearNuevoItemListView(e);
                etapasListView.Items.Add(lvi);
                
            }
        }

        private static ListViewItem CrearNuevoItemListView(Etapa etapa)
        {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = (etapa.Identificacion) + "";
            listViewItem.SubItems[0].Tag = "int";
            listViewItem.SubItems.Add(etapa.Nombre).Tag = "string";
            listViewItem.SubItems.Add(etapa.Tareas.Count.ToString()).Tag = "int";
            listViewItem.SubItems.Add(etapa.EstaFinalizada ? etapa.FechaFinalizacion.ToString() : "").Tag = "DateTime";
            return listViewItem;
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

        private void etapasListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.OrdenarColumna)
            {
                if (lvwColumnSorter.Orden == SortOrder.Ascending)
                {
                    lvwColumnSorter.Orden = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Orden = SortOrder.Ascending;
                }
            }
            else
            {
                lvwColumnSorter.OrdenarColumna = e.Column;
                lvwColumnSorter.Orden = SortOrder.Ascending;
            }

            etapasListView.Sort();
            etapasListView.AsignarIconoColumna(lvwColumnSorter.OrdenarColumna, lvwColumnSorter.Orden);
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
            return proyecto.Etapas.Find(x => x.Identificacion == GetSelectedId());
        }

        private bool CartelConfirmacionEliminacionAceptado()
        {
            DialogResult dialogResult = MessageBox.Show("¿Seguro desea eliminar esta etapa?",
                "Eliminar etapa", MessageBoxButtons.YesNo);
            return dialogResult == DialogResult.Yes;
        }

        private int GetSelectedId()
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
