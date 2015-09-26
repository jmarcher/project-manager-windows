﻿using Dominio;
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

        private static ListViewItem CrearNuevoItemListView(Etapa etapa)
        {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Text = (etapa.Id) + "";
            listViewItem.SubItems.Add(etapa.Nombre);
            listViewItem.SubItems.Add(etapa.Tareas.Count.ToString());
            listViewItem.SubItems.Add(etapa.Finalizada ? etapa.FechaFinalizacion.ToString() : "");
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
            Etapa etapa = new Etapa()
            {
                Id = GetSelectedId()
            };
            return etapa;
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
    }
}
