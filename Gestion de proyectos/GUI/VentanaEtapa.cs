using Dominio;
using GUI.Utils;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class VentanaEtapa : Form
    {
        private const int MAX_NUMEROS_DURACION = 5000;
        public Etapa Etapa { get; private set; }
        private bool etapaGuardada = false;
        public VentanaEtapa(Etapa etapa)
        {
            InitializeComponent();
            Etapa = etapa;
        }

        private void VentanaEtapa_Load(object sender, EventArgs e)
        {
            this.Text = "Detalles de la etapa: " + Etapa.Nombre + "(id: " + Etapa.Identificacion + ")";
            AsignarTextoAControles();
        }

        private void AsignarTextoAControles()
        {
            idTextBox.Text = Etapa.Identificacion.ToString();
            nombreTextBox.Text = Etapa.Nombre;
            marcarFinalizadaCheckBox.Checked = Etapa.EstaFinalizada;
            fechaInicioTextBox.Text = Etapa.FechaInicio.ToString();
            fechaFinalizacionTextBox.Text = Etapa.FechaFinalizacion.ToString();
            AsignarListaNumerosDuracionPendiente();
            duracionPendienteDomainUpDown.SelectedItem = Etapa.DuracionPendiente;
            tareasAgregadasListBox.Items.AddRange(Etapa.Tareas.ToArray());
        }

        private void AsignarListaNumerosDuracionPendiente()
        {
            duracionPendienteDomainUpDown.Items.AddRange(CrearListaNumeros());
        }

        private static int[] CrearListaNumeros()
        {
            int[] listaNumeros = new int[MAX_NUMEROS_DURACION];
            for (int i = 0; i < MAX_NUMEROS_DURACION; i++)
            {
                listaNumeros[i] = (i);
            }

            return listaNumeros;
        }

        private void guardarButton_Click(object sender, EventArgs e)
        {
            Etapa.Nombre = this.nombreTextBox.Text;
            if (marcarFinalizadaCheckBox.Checked) {
                Etapa.MarcarFinalizada();
            }
            Etapa.DuracionPendiente = (int)this.duracionPendienteDomainUpDown.SelectedItem;
            etapaGuardada = true;
            this.Close();
        }

        private void VentanaEtapa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!etapaGuardada)
            {
                string Message = "¿Está seguro que desea salir?";
                string Title = "Confirmación de salida";
                if (!VisualHelper.CartelConfirmacion(Message, Title))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
