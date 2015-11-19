using System.Windows.Forms;
using DominioInterfaz;

namespace InterfazGrafica
{
    public partial class VentanaVerHistorialTarea : Form
    {
        public VentanaVerHistorialTarea(ITarea tarea)
        {
            InitializeComponent();
            textBoxHistorial.Text = tarea.Historial;
        }
    }
}
