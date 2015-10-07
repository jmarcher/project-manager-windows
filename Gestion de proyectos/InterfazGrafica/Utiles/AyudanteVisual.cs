using System.Windows.Forms;

namespace InterfazGrafica.Utiles
{
    public class AyudanteVisual
    {
        public static bool CartelConfirmacion(string Message, string Title)
        {
            DialogResult dialogResult = MessageBox.Show(Message,
                Title, MessageBoxButtons.YesNo);
            return dialogResult == DialogResult.Yes;
        }
    }
}
