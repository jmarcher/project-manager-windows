using System;
using Dominio;
using System.Windows.Forms;
using DominioInterfaz;

namespace InterfazGrafica
{
    public partial class VentanaVerHistorialProyecto : Form
    {
        public VentanaVerHistorialProyecto(IProyecto proyecto)
        {
            InitializeComponent();
            textBoxHistorialProyecto.Text = proyecto.Historial;
        }

        private void VentanaVerHistorialTarea_Load(object sender, EventArgs e)
        {

        }
    }
}
