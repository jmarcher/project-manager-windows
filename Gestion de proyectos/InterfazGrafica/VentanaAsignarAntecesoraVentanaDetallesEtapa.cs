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

namespace InterfazGrafica
{
    public partial class VentanaAsignarAntecesoraVentanaDetallesEtapa : Form
    {
        private Etapa etapa;
        private Tarea tarea;
        public VentanaAsignarAntecesoraVentanaDetallesEtapa(Etapa etapa, Tarea tarea)
        {
            InitializeComponent();
            this.etapa = etapa;
            this.tarea = tarea;
            inicializarComboBoxTareas();
        }

        private void inicializarComboBoxTareas()
        {
            foreach(Tarea tareaActual in etapa.Tareas)
            {
                if(tareaActual.FechaFinalizacion < tarea.FechaInicio)
                {
                    comboBoxTareas.Items.Add(tareaActual);
                }
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            this.tarea.AgregarAntecesora(tareaSeleccionada());
            this.Close();
        }

        private Tarea tareaSeleccionada()
        {
            return (Tarea)comboBoxTareas.SelectedItem;
        }
    }
}
