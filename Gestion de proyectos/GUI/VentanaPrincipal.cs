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
using GUI.Utils;

namespace GUI
{
    public partial class VentanaPrincipal : Form
    {
        Singleton patron = Singleton.Instance;
        List<Proyecto> Proyectos;

        public VentanaPrincipal()
        {
            InitializeComponent();
            try
            {

                DatosDePrueba dp = new DatosDePrueba();
                patron.agregarListaProyecto(dp.ObtenerUnaListaProyectos());
                Proyectos = patron.devolverListaProyectos();
                configurarListViewProyectos();
                actualizarLista();
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

        private void actualizarLista()
        {
            listViewProyectos.Items.Clear();
            for (int i = 0; i < Proyectos.Count(); i++)
            {
                ListViewItem nuevoItemLista = new ListViewItem();
                nuevoItemLista.Text = Proyectos[i].Nombre;
                nuevoItemLista.SubItems.Add(Proyectos[i].Objetivo);
                string id = Proyectos[i].Id.ToString();
                nuevoItemLista.SubItems.Add(id);
                if (Proyectos[i].Finalizado)
                {
                    nuevoItemLista.SubItems.Add("Proyecto Finalizado");
                    nuevoItemLista.ForeColor = Color.Red;
                }
                else
                {
                    string fechaFinalizacion = Proyectos[i].ObtenerFechaFinalizacion().ToString();
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
            int i = listViewProyectos.SelectedIndices[0];
            Proyecto p = Proyectos[i];
            VentanaDetallesProyecto ventana = new VentanaDetallesProyecto(p);
            ventana.Show();
        }

        private void buttonAgregarNuevoProyecto_Click(object sender, EventArgs e)
        {
            VentanaAltaDeProyecto ventanaAlta = new VentanaAltaDeProyecto();
            ventanaAlta.ShowDialog();
            foreach (Form frm in Application.OpenForms)
            {
                if (!(frm.GetType() == typeof(VentanaAltaDeProyecto)))
                {
                    this.actualizarLista();
                    break;
                }
            }
        }
    }
}