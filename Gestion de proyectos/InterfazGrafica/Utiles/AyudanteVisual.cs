using System.Windows.Forms;

namespace InterfazGrafica.Utiles
{
    public class AyudanteVisual
    {
        public static bool CartelConfirmacion(string mensaje, string titulo)
        {
            DialogResult resultadoDialogo = MessageBox.Show(mensaje,
                titulo, MessageBoxButtons.YesNo);
            return resultadoDialogo == DialogResult.Yes;
        }

        public static void CartelInformacion(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje,
                            titulo,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button1);
        }
    }
}
