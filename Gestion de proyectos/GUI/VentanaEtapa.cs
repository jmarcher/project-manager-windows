using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class VentanaEtapa : Form
    {
        public Etapa Etapa { get; private set; }
        public VentanaEtapa(Etapa etapa)
        {
            InitializeComponent();
            this.Etapa = etapa;
        }

        private void VentanaEtapa_Load(object sender, EventArgs e)
        {
            tareasAgregadasListBox.Items.AddRange(Etapa.Tareas.ToArray());
        }
    }
}
