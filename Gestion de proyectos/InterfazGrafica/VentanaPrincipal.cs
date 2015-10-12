using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using InterfazGrafica.Utiles;

namespace InterfazGrafica
{
    public partial class VentanaPrincipal : Form
    {
        InstanciaUnica patron = InstanciaUnica.Instancia;
        List<Proyecto> Proyectos;

        public VentanaPrincipal()
        {
            InitializeComponent();
            try
            {

                DatosDePrueba2 dp = new DatosDePrueba2();
                patron.AgregarListaProyecto(dp.ObtenerUnaListaProyectos());
                Proyectos = patron.DevolverListaProyectos();
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
            //propiedades de la listview
            listViewProyectos.View = View.Details;
            listViewProyectos.FullRowSelect = true;
            listViewProyectos.GridLines = true;

            listViewProyectos.Columns.Add("Nombre", 100, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("Objetivo", 200, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("ID", 70, HorizontalAlignment.Left);
            listViewProyectos.Columns.Add("Fecha estimada de finalizacion", 200, HorizontalAlignment.Left);
        }

        private void ActualizarListaDeProyectos()
        {
            listViewProyectos.Items.Clear();
            for (int i = 0; i < Proyectos.Count(); i++)
            {
                ListViewItem nuevoItemLista = new ListViewItem();
                nuevoItemLista.Text = Proyectos[i].Nombre;
                nuevoItemLista.SubItems.Add(Proyectos[i].Objetivo);
                string id = Proyectos[i].Identificador.ToString();
                nuevoItemLista.SubItems.Add(id);
                if (Proyectos[i].EstaFinalizado)
                {
                    nuevoItemLista.SubItems.Add("Proyecto Finalizado");
                    nuevoItemLista.ForeColor = Color.Red;
                }
                else if (Proyectos[i].EstaAtrasado)
                {
                    nuevoItemLista.SubItems.Add("Proyecto Atrasado");
                    nuevoItemLista.ForeColor = Color.Orange;
                }
                else
                {
                    string fechaFinalizacion = Proyectos[i].FechaFinalizacion.ToString();
                    nuevoItemLista.SubItems.Add(fechaFinalizacion);
                    nuevoItemLista.ForeColor = Color.Green;
                }
                listViewProyectos.Items.Add(nuevoItemLista);
            }
            {

            }
        }

        private void listViewProyectos_DoubleClick(object sender, EventArgs e)
        {
            int indice = listViewProyectos.SelectedIndices[0];
            Proyecto proyecto = Proyectos[indice];
            VentanaDetallesProyecto ventana = new VentanaDetallesProyecto(proyecto);
            ventana.ShowDialog(this);
            ActualizarListaDeProyectosConCondicion(new CondicionDeActualizacion(EstaCerradaVentanaDetallesProyecto));
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
            buttonAgregarNuevoProyecto_Click(sender,e);
        }

        private void leyendaDeColoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VentanaLeyendaDeColoresVentanaPrincipal ventanaLeyendas =
                new VentanaLeyendaDeColoresVentanaPrincipal();
            ventanaLeyendas.ShowDialog();

        }
    }
}