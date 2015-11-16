using System;
using Dominio;
using System.Windows.Forms;

namespace InterfazGrafica
{
    public partial class VentanaVerHistorialTarea : Form
    {
        public VentanaVerHistorialTarea(Tarea tarea)
        {
            InitializeComponent();
            textBoxHistorial.Text = tarea.Historial;
        }
    }
}
