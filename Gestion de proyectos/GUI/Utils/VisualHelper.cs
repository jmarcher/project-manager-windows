using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Utils
{
    public class VisualHelper
    {
        public static bool CartelConfirmacion(string Message, string Title)
        {
            DialogResult dialogResult = MessageBox.Show(Message,
                Title, MessageBoxButtons.YesNo);
            return dialogResult == DialogResult.Yes;
        }
    }
}
