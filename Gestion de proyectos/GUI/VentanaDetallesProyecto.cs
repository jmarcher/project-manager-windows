using Dominio;
using GUI.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class VentanaDetallesProyecto : Form
    {
        private Proyecto proyecto;
        private ListViewColumnSorter lvwColumnSorter;

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
            lvwColumnSorter = new ListViewColumnSorter();
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

        private static ListViewItem CrearNuevoItemListView(Etapa e)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = e.Nombre;
            lvi.SubItems.Add(e.Tareas.Count.ToString());
            lvi.SubItems.Add(e.Finalizada ? e.FechaFinalizacion.ToString() : "");
            return lvi;
        }

        private void InicializarLista()
        {
            //propiedades de la listview
            etapasListView.View = View.Details;
            etapasListView.FullRowSelect = true;
            etapasListView.GridLines = true;
            etapasListView.Sorting = SortOrder.Ascending;
            etapasListView.Columns.Add("Nombre", 100, HorizontalAlignment.Left);
            etapasListView.Columns.Add("Cant. Tareas", 50, HorizontalAlignment.Left);
            etapasListView.Columns.Add("Fecha finalización", 140, HorizontalAlignment.Left);
        }

        private void etapasListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            etapasListView.Sort();
        }
    }
}
