using System;
using System.Windows.Forms;
using Dominio;
using PersistenciaImp;

namespace InterfazGrafica
{
    public partial class VentanaAltaDeProyecto : Form
    {
        private IContextoGestorProyectos contexto;
        public VentanaAltaDeProyecto(IContextoGestorProyectos contexto)
        {
            this.contexto = contexto;
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
                DuracionPendiente = Int32.Parse(this.textBoxDuracionPendienteNuevoProyecto.Text),
                DuracionEstimada = Int32.Parse(textBoxDuracionEstimadaTarea.Text)
            };
            tareaNuevoProyecto.DefinirPrioridad(comboBoxPrioridadNuevoProyecto.SelectedItem.ToString());
            Etapa etapaNuevoProyecto = new Etapa()
            {
                Nombre = this.textBoxNombreEtapaNuevoProyecto.Text,
                FechaInicio = this.monthCalendarFechaInicioEtapa.SelectionStart,
                DuracionEstimada = Int32.Parse(textBoxDurcionEstimadaEtapa.Text)
            };
            Proyecto nuevoProyecto = new Proyecto(contexto)
            {
                Nombre = textBoxNombreDelNuevoProyecto.Text,
                Objetivo = this.richTextBoxObjetivoDelNuevoProyecto.Text,
                FechaInicio = this.monthCalendarFechaInicioProyecto.SelectionStart,
                DuracionEstimada = Int32.Parse(textBoxDuracionEstimadaProyecto.Text)
            };

            etapaNuevoProyecto.AgregarTarea(tareaNuevoProyecto);

            nuevoProyecto.AgregarEtapa(etapaNuevoProyecto);
            contexto.AgregarProyecto(nuevoProyecto);
            

        }

        private void textBoxDuracionPendienteNuevoProyecto_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDuracionPendienteNuevoProyecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBoxDuracionEstimadaProyecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBoxDuracionEstimadaTarea_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void buttonGuardarNuevoProyecto_Click_1(object sender, EventArgs e)
        {
            try
            {
                crearNuevoProyecto();
                this.Close();
            }
            catch (FormatException)
            {
                Utiles.AyudanteVisual.CartelExclamacion("Los números no pueden ser superiores a  2.147.483.647", "Numero grande");
            }
        }

        private void textBoxDuracionPendienteNuevoProyecto_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
