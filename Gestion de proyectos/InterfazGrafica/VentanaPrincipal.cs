﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Dominio;
using InterfazGrafica.Utiles;

namespace InterfazGrafica
{
    public partial class VentanaPrincipal : Form
    {
        private OrdenadorColumnaListView ordenadorListView;

        public VentanaPrincipal()
        {
            InitializeComponent();
            try
            {

                DatosDePrueba2 dp = new DatosDePrueba2();
                InstanciaUnica.Instancia.AgregarListaProyecto(dp.ObtenerUnaListaProyectos());
                configurarListViewProyectos();
                ActualizarListaDeProyectos();
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("error objeto vacio");
            }
        }

        private void configurarListViewProyectos()
        {
            InicializarOrdenadorListView();

            listViewProyectos.View = View.Details;
            listViewProyectos.FullRowSelect = true;
            listViewProyectos.GridLines = true;


            listViewProyectos.Columns.Add("ID", 70, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("Nombre", 100, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("Objetivo", 200, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("Fecha estimada de finalizacion", 200, HorizontalAlignment.Left);
        }

        private void InicializarOrdenadorListView()
        {
            this.ordenadorListView = new OrdenadorColumnaListView();
            listViewProyectos.ListViewItemSorter = ordenadorListView;
        }

        private void ActualizarListaDeProyectos()
        {
            listViewProyectos.Items.Clear();
            foreach(Proyecto proyecto in InstanciaUnica.Instancia.DevolverListaProyectos())
            {
                ListViewItem nuevoItemLista = new ListViewItem();
                string identificador = proyecto.Identificador.ToString();
                nuevoItemLista.Text = identificador;
                nuevoItemLista.SubItems[0].Tag = "int";
                nuevoItemLista.SubItems.Add(proyecto.Nombre).Tag = "string";
                nuevoItemLista.SubItems.Add(proyecto.Objetivo).Tag = "string";
                if (proyecto.EstaFinalizado)
                {
                    nuevoItemLista.SubItems.Add("Proyecto Finalizado").Tag = "string";
                    nuevoItemLista.ForeColor = Color.Red;
                }
                else if (proyecto.EstaAtrasado)
                {
                    nuevoItemLista.SubItems.Add("Proyecto Atrasado").Tag = "string";
                    nuevoItemLista.ForeColor = Color.Orange;
                }
                else
                {
                    string fechaFinalizacion = proyecto.FechaFinalizacion.ToString();
                    nuevoItemLista.SubItems.Add(fechaFinalizacion).Tag = "DateTime";
                    nuevoItemLista.ForeColor = Color.Green;
                }
                listViewProyectos.Items.Add(nuevoItemLista);
            }
        }

        private void listViewProyectos_DoubleClick(object sender, EventArgs e)
        {
            Proyecto proyecto = proyectoSeleccionado();
            abrirVentanaDetallesProyecto(proyecto);
        }

        private void abrirVentanaDetallesProyecto(Proyecto proyecto)
        {
            VentanaDetallesProyecto ventana = new VentanaDetallesProyecto(proyecto);
            ventana.ShowDialog(this);
            ActualizarListaDeProyectosConCondicion(new CondicionDeActualizacion(EstaCerradaVentanaDetallesProyecto));
        }

        private Proyecto proyectoSeleccionado()
        {
            return InstanciaUnica.Instancia.DevolverListaProyectos().Find(x => x.Identificador == devolverIdentificadorSeleccionado());
        }

        private int devolverIdentificadorSeleccionado()
        {
            return Int32.Parse(listViewProyectos.SelectedItems[0].Text);
        }

        private void buttonAgregarNuevoProyecto_Click(object sender, EventArgs e)
        {
            VentanaAltaDeProyecto ventanaAlta = new VentanaAltaDeProyecto();
            ventanaAlta.ShowDialog();
            ActualizarListaDeProyectosConCondicion(new CondicionDeActualizacion(EstaCerradaVentanaAltaDeProyecto));
        }

        public delegate bool CondicionDeActualizacion(Form formulario);
        private void ActualizarListaDeProyectosConCondicion(CondicionDeActualizacion metodo)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (metodo(frm))
                {
                    ActualizarListaDeProyectos();
                    break;
                }
            }
        }

        private bool EstaCerradaVentanaAltaDeProyecto(Form frm)
        {
            return !(frm.GetType() == typeof(VentanaAltaDeProyecto));
        }

        private bool EstaCerradaVentanaDetallesProyecto(Form frm)
        {
            return !(frm.GetType() == typeof(VentanaDetallesProyecto));
        }
        private void crearNuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonAgregarNuevoProyecto_Click(sender, e);
        }

        private void leyendaDeColoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VentanaLeyendaDeColoresVentanaPrincipal ventanaLeyendas =
                new VentanaLeyendaDeColoresVentanaPrincipal();
            ventanaLeyendas.ShowDialog();

        }

        private void listViewProyectos_ColumnClick(object remitente, ColumnClickEventArgs evento)
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

            listViewProyectos.Sort();
            listViewProyectos.AsignarIconoColumna(ordenadorListView.OrdenarColumna, ordenadorListView.Orden);
        }

        private void buttonEliminarProyecto_Click(object sender, EventArgs e)
        {
            if (hayProyectoSeleccionado())
            {
                if (AyudanteVisual.CartelConfirmacion("¿Seguro desea eliminar este proyecto?\n" +
                    "La siguiente acción, eliminará el pryecto con todas sus etapas y tareas.", "Eliminación de proyecto"))
                {
                    InstanciaUnica.Instancia.DevolverListaProyectos().Remove(proyectoSeleccionado());
                    ActualizarListaDeProyectos();
                }
            }
        }

        private bool hayProyectoSeleccionado()
        {
            return listViewProyectos.SelectedItems.Count > 0;
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AyudanteVisual.CartelInformacion("Obligatorio 1\n"+
                "Diseño de aplicaciones 1\n"+
                "Universidad ORT\n"+
                "Joaquín Marcher\n"+
                "Joaquín Musé", "Acerca de");
        }
    }
}