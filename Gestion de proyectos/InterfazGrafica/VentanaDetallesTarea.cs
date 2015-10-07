using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
namespace InterfazGrafica
{
    public partial class VentanaDetallesTarea : Form
    {
        private Tarea tarea;
        public VentanaDetallesTarea()
        {
            InitializeComponent();
        }

        public VentanaDetallesTarea(Tarea tarea)
        {
            InitializeComponent();
            this.tarea = tarea;
            InicializarComponentes(tarea);
        }

        private void InicializarComponentes(Tarea tarea)
        {
            textBoxNombre.Text = tarea.Nombre;
            textBoxObjetivo.Text = tarea.Objetivo;
            textBoxDuracionPendiente.Text = tarea.CalcularDuracionPendiente().ToString();
            textBoxDescripcion.Text = tarea.Descripcion;
            dateTimePickerFechaInicio.Value = tarea.FechaInicio;
            dateTimePickerFechaFinalizacion.Value = tarea.FechaFinalizacion;
            comboBoxPrioridad.SelectedIndex = tarea.Prioridad;
            InicualizarListViewAntecesoras();
        }

        private void InicualizarListViewAntecesoras()
        {
            foreach(Tarea tarea in tarea.Antecesoras)
            {            
                    ArgegarElemento(tarea);
            }
        }

        private void ArgegarElemento(Tarea tarea)
        {
            ListViewItem elementoLista = new ListViewItem(tarea.ToString());
            elementoLista.ImageIndex = IconoTarea(tarea);
            listViewAntecesoras.Items.Add(elementoLista);
        }

        private int IconoTarea(Tarea tarea)
        {
            if (EsCompuesta(tarea))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private bool EsCompuesta(Tarea tarea)
        {
            return tarea.GetType() == typeof(TareaCompuesta);
        }
    }
}
