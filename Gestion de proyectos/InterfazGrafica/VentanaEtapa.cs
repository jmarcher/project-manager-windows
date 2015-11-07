using Dominio;
using InterfazGrafica.Utiles;
using System;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class VentanaEtapa : Form
    {
        private const int MAXIMO_NUMEROS_DURACION = 5000;
        public Etapa Etapa { get; private set; }
        private bool estaEtapaGuardada = false;
        public VentanaEtapa(Etapa etapa)
        {
            InitializeComponent();
            Etapa = etapa;
        }

        private void VentanaEtapa_Load(object sender, EventArgs e)
        {
            this.Text = "Detalles de la etapa: " + Etapa.Nombre + "(id: " + Etapa.EtapaID + ")";
            AsignarTextoAControles();
        }

        private void AsignarTextoAControles()
        {
            idTextBox.Text = Etapa.EtapaID.ToString();
            nombreTextBox.Text = Etapa.Nombre;
            marcarFinalizadaCheckBox.Checked = Etapa.EstaFinalizada;
            fechaInicioTextBox.Text = Etapa.FechaInicio.ToString();
            fechaFinalizacionTextBox.Text = Etapa.FechaFinalizacion.ToString();
            AsignarListaNumerosDuracionPendiente();
            duracionPendienteDomainUpDown.SelectedItem = Etapa.CalcularDuracionPendiente();
            tareasAgregadasListBox.Items.AddRange(Etapa.Tareas.ToArray());
        }

        private void AsignarListaNumerosDuracionPendiente()
        {
            duracionPendienteDomainUpDown.Items.AddRange(CrearListaNumeros());
        }

        private static int[] CrearListaNumeros()
        {
            int[] listaNumeros = new int[MAXIMO_NUMEROS_DURACION];
            for (int i = 0; i < MAXIMO_NUMEROS_DURACION; i++)
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
            estaEtapaGuardada = true;
            this.Close();
        }

        private void VentanaEtapa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!estaEtapaGuardada)
            {
                string Message = "¿Está seguro que desea salir?";
                string Title = "Confirmación de salida";
                if (!AyudanteVisual.CartelConfirmacion(Message, Title))
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
