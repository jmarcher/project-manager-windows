﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Dominio;
using InterfazGrafica.Utiles;
using System.Collections.Generic;

namespace InterfazGrafica
{
    public partial class VentanaPrincipal : Form
    {
        private OrdenadorListView ordenadorListView;
        private const int ID_COLUMNA_FECHA_FIN = 4;

        public VentanaPrincipal()
        {
            
            InitializeComponent();
            try
            {
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
            listViewProyectos.Columns.Add("Objetivo", 130, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("Fecha inicio", 130, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("Fecha finalizacion", 130, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("Duración pendiente (dias)", 80, HorizontalAlignment.Left);
        }

        private void InicializarOrdenadorListView()
        {
            this.ordenadorListView = new OrdenadorListView();
            listViewProyectos.ListViewItemSorter = ordenadorListView;
        }

        private void ActualizarListaDeProyectos()
        {
            listViewProyectos.Items.Clear();
            foreach(Proyecto proyecto in InstanciaUnica.Instancia.DevolverProyectos())
            {
                ListViewItem nuevoItemLista = crearNuevoItemListaProyectos(proyecto);
                listViewProyectos.Items.Add(nuevoItemLista);
            }
        }

        private static ListViewItem crearNuevoItemListaProyectos(Proyecto proyecto)
        {
            ListViewItem nuevoItemLista = new ListViewItem();
            string identificador = proyecto.ProyectoID.ToString();
            agregarIdentificadorALista(nuevoItemLista, identificador);
            agregarNombreALista(proyecto, nuevoItemLista);
            agregarObjetivoALista(proyecto, nuevoItemLista);
            agregarFechasALista(proyecto, nuevoItemLista);
            agregarDuracionPendienteALista(proyecto, nuevoItemLista);
            return nuevoItemLista;
        }

        private static void agregarDuracionPendienteALista(Proyecto proyecto, ListViewItem nuevoItemLista)
        {
            nuevoItemLista.SubItems.Add(proyecto.CalcularDuracionPendiente().ToString()).Tag 
                = OrdenadorListView.INT;
        }

        private static void agregarObjetivoALista(Proyecto proyecto, ListViewItem nuevoItemLista)
        {
            nuevoItemLista.SubItems.Add(proyecto.Objetivo).Tag = OrdenadorListView.STRING;
        }

        private static void agregarNombreALista(Proyecto proyecto, ListViewItem nuevoItemLista)
        {
            nuevoItemLista.SubItems.Add(proyecto.Nombre).Tag = OrdenadorListView.STRING;
        }

        private static void agregarIdentificadorALista(ListViewItem nuevoItemLista, string identificador)
        {
            nuevoItemLista.Text = identificador;
            nuevoItemLista.SubItems[0].Tag = OrdenadorListView.INT;
        }

        private static void agregarFechasALista(Proyecto proyecto, ListViewItem nuevoItemLista)
        {
            nuevoItemLista.SubItems.Add(proyecto.FechaInicio.ToString()).Tag = OrdenadorListView.DATETIME;
            cambiarFechaPorEstadoProyecto(proyecto, nuevoItemLista);
        }

        private static void cambiarFechaPorEstadoProyecto(Proyecto proyecto, ListViewItem nuevoItemLista)
        {
            if (proyecto.EstaFinalizado)
            {
                nuevoItemLista.SubItems.Add("Proyecto Finalizado").Tag = OrdenadorListView.STRING;
                nuevoItemLista.ForeColor = Color.Red;
            }
            else if (proyecto.EstaAtrasado)
            {
                nuevoItemLista.SubItems.Add("Proyecto Atrasado").Tag = OrdenadorListView.STRING;
                nuevoItemLista.ForeColor = Color.Orange;
            }
            else
            {
                string fechaFinalizacion = proyecto.FechaFinalizacion.ToString();
                nuevoItemLista.SubItems.Add(fechaFinalizacion).Tag = OrdenadorListView.DATETIME;
                nuevoItemLista.ForeColor = Color.Green;
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
            return InstanciaUnica.Instancia.DevolverProyectos().Find(x => x.ProyectoID == devolverIdentificadorSeleccionado());
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

        private void listViewProyectos_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ordenadorListView.HizoClickEnColumna(e);
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
                    InstanciaUnica.Instancia.DevolverProyectos().Remove(proyectoSeleccionado());
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

        private void VentanaPrincipal_Load(object sender, EventArgs e)
        {
            listViewProyectos_ColumnClick(sender, new ColumnClickEventArgs(ID_COLUMNA_FECHA_FIN));
        }

        private void borrarDatosDePruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstanciaUnica.Instancia.AgregarProyectos(listaVacia());
            ActualizarListaDeProyectos();
        }

        private static List<Proyecto> listaVacia()
        {
            return new List<Proyecto>();
        }

        private void cargarDatosDePruebaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatosDePrueba dp = new DatosDePrueba();
            InstanciaUnica.Instancia.AgregarProyectos(dp.ObtenerUnaListaProyectos());
            ActualizarListaDeProyectos();
        }
    }
}