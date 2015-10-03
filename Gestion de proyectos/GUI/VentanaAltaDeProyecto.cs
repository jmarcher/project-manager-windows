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

namespace InterfazGrafica
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
            Tarea tareaNuevoProyecto = new Tarea()
            {
                Nombre = this.textBoxNombreTareaNuevoProyecto.Text,
                Objetivo = this.textBoxObjetivoTareaNuevoProyecto.Text,
                Descripcion = this.richTextBoxDescripcionTareaNuevoProyecto.Text,
                FechaInicio = monthCalendarFechaInicioTareaNuevoProyecto.SelectionRange.Start,
                FechaFinalizacion = monthCalendarFechaFinTareaNuevoProyecto.SelectionRange.Start,
                DuracionPendiente = Int32.Parse(this.textBoxDuracionPendienteNuevoProyecto.Text),
                Prioridad = Int32.Parse(comboBoxPrioridadNuevoProyecto.SelectedItem.ToString())
            };
            Etapa etapaNuevoProyecto = new Etapa()
            {
                Nombre = this.textBoxNombreEtapaNuevoProyecto.Text,
                Identificacion = Int32.Parse(this.textBoxIdEtapaNuevoProyecto.Text),
                FechaInicio = this.monthCalendarFechaInicioEtapa.SelectionStart
            };
            Proyecto nuevoProyecto = new Proyecto()
            {
                Nombre = textBoxNombreDelNuevoProyecto.Text,
                Objetivo = this.richTextBoxObjetivoDelNuevoProyecto.Text,
                FechaInicio = this.monthCalendarFechaInicioProyecto.SelectionStart
            };

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
