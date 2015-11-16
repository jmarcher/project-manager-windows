using System;
using Dominio;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class VentanaVerHistorialProyecto : Form
    {
        public VentanaVerHistorialProyecto(Proyecto proyecto)
        {
            InitializeComponent();
            textBoxHistorialProyecto.Text = proyecto.Historial;
        }

        private void VentanaVerHistorialTarea_Load(object sender, EventArgs e)
        {

        }
    }
}
