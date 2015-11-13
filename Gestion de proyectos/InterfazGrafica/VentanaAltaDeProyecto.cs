using System;
using System.Windows.Forms;
using Dominio;
using PersistenciaImp;

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
                Console.WriteLine("Error no se ingreso un numero: " + f.Message);
            }
        }

        private void crearNuevoProyecto()
        {
            Tarea tareaNuevoProyecto = new TareaSimple(new ContextoGestorProyectos())
            {
                Nombre = this.textBoxNombreTareaNuevoProyecto.Text,
                Objetivo = this.textBoxObjetivoTareaNuevoProyecto.Text,
                Descripcion = this.richTextBoxDescripcionTareaNuevoProyecto.Text,
                FechaInicio = monthCalendarFechaInicioTareaNuevoProyecto.SelectionStart,
                FechaFinalizacion = monthCalendarFechaFinTareaNuevoProyecto.SelectionStart,
                DuracionPendiente = Int32.Parse(this.textBoxDuracionPendienteNuevoProyecto.Text) 
            };
            tareaNuevoProyecto.DefinirPrioridad(comboBoxPrioridadNuevoProyecto.SelectedItem.ToString());
            Etapa etapaNuevoProyecto = new Etapa()
            {
                Nombre = this.textBoxNombreEtapaNuevoProyecto.Text,
                FechaInicio = this.monthCalendarFechaInicioEtapa.SelectionStart
            };
            Proyecto nuevoProyecto = new Proyecto()
            {
                Nombre = textBoxNombreDelNuevoProyecto.Text,
                Objetivo = this.richTextBoxObjetivoDelNuevoProyecto.Text,
                FechaInicio = this.monthCalendarFechaInicioProyecto.SelectionStart
            };

            etapaNuevoProyecto.AgregarTarea(tareaNuevoProyecto);

            nuevoProyecto.AgregarEtapa(etapaNuevoProyecto);
            using(var db = new ContextoGestorProyectos())
            {
                db.AgregarProyecto(nuevoProyecto);
            }

        }
    }
}
