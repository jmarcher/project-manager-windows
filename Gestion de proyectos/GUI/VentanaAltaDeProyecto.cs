﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;

namespace GUI
{
    public partial class VentanaAltaDeProyecto : Form
    {
        public VentanaAltaDeProyecto()
        {
            InitializeComponent();
            InicializarComboPrioridad();
        }

        private void InicializarComboPrioridad()
        {
            comboBoxPrioridadNuevoProyecto.Items.Clear();
            comboBoxPrioridadNuevoProyecto.Items.Add("Alta");
            comboBoxPrioridadNuevoProyecto.Items.Add("Media");
            comboBoxPrioridadNuevoProyecto.Items.Add("Baja");
            comboBoxPrioridadNuevoProyecto.SelectedIndex = 0;
        }

        private void buttonSiguienteNuevoProyecto_Click(object sender, EventArgs e)
        {
            this.panelEtapaDeNuevoProyecto.Visible = true;
        }

        private void buttonAtrasNuevoProyecto_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSiguienteEtapaNuevoProyecto_Click(object sender, EventArgs e)
        {
            this.panelTareaNuevoProyecto.Visible = true;
        }

        private void buttonAtrasEtapaNuevoProyecto_Click(object sender, EventArgs e)
        {
            this.panelTareaNuevoProyecto.Visible = false;
            this.panelEtapaDeNuevoProyecto.Visible = false;
        }

        private void buttonAtrasTareaNuevoProyecto_Click(object sender, EventArgs e)
        {
            this.panelTareaNuevoProyecto.Visible = false;
            this.panelEtapaDeNuevoProyecto.Visible = true;
           
        }

        private void buttonGuardarNuevoProyecto_Click(object sender, EventArgs e)
        {
            try
            {
                crearNuevoProyecto();
                this.Close();
            }
            catch (FormatException f)
            {
                Console.WriteLine("error no se ingreso un numero");
            }
        }

        private void crearNuevoProyecto()
        {
            Tarea tareaNuevoProyecto = new Tarea(this.textBoxNombreTareaNuevoProyecto.Text, this.textBoxObjetivoTareaNuevoProyecto.Text, this.richTextBoxDescripcionTareaNuevoProyecto.Text, this.monthCalendarFechaInicioTareaNuevoProyecto.SelectionRange.Start,this.monthCalendarFechaFinTareaNuevoProyecto.SelectionRange.Start, Int32.Parse(this.textBoxDuracionPendienteNuevoProyecto.Text), this.comboBoxPrioridadNuevoProyecto.SelectedItem.ToString());
            Etapa etapaNuevoProyecto = new Etapa(this.textBoxNombreEtapaNuevoProyecto.Text, Int32.Parse(this.textBoxIdEtapaNuevoProyecto.Text),this.monthCalendarFechaInicioEtapa.SelectionStart);
            Proyecto nuevoProyecto = new Proyecto(this.textBoxNombreDelNuevoProyecto.Text, this.richTextBoxObjetivoDelNuevoProyecto.Text,this.monthCalendarFechaInicioProyecto.SelectionStart);

            etapaNuevoProyecto.AgregarTarea(tareaNuevoProyecto);
            etapaNuevoProyecto.InsertarFechaFinalizacion();
            etapaNuevoProyecto.InsertarDuracion();

            nuevoProyecto.AgregarEtapa(etapaNuevoProyecto);
            nuevoProyecto.InsertarDuracion();
            nuevoProyecto.InsertarFechaFin();
            Singleton.Instance.agregarProyecto(nuevoProyecto);

        }
    }
}