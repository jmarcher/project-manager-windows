﻿using Dominio;
using InterfazGrafica.Utiles;
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
        private IContextoGestorProyectos contexto;
        public VentanaAsignarAntecesoraVentanaDetallesEtapa(Etapa etapa, Tarea tarea,IContextoGestorProyectos contexto)
        {
            this.contexto = contexto;
            InitializeComponent();
            this.etapa = etapa;
            this.tarea = tarea;
            inicializarComboBoxTareas();
        }

        private void cerrarVentanaSiListaVacia()
        {
            if(comboBoxTareas.Items.Count == 0)
            {
                AyudanteVisual.CartelExclamacion("No es fue posible encontrar tareas que cumplan el criterio de poder ser antecesoras de esta tarea.", "No hay tareas.");
                this.Close();
            }
        }

        private void inicializarComboBoxTareas()
        {
            foreach(Tarea tareaActual in etapa.Tareas)
            {
                if (terminaAntes(tareaActual) && noEsAntecesora(tareaActual))
                {
                    comboBoxTareas.Items.Add(tareaActual);
                }
            }
        }

        private bool noEsAntecesora(Tarea tareaActual)
        {
            return !tarea.Antecesoras.Contains(tareaActual);
        }

        private bool terminaAntes(Tarea tareaActual)
        {
            return tareaActual.FechaFinalizacion < tarea.FechaInicio;
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (hayTareaSeleccionada())
            {
                this.tarea.AgregarAntecesora(tareaSeleccionada());
                contexto.ModificarTarea(tarea);
                this.Close();
            }
            else
            {
                AyudanteVisual.CartelExclamacion("No se seleccionó ninguna tarea.","Problema selección");
            }
        }

        private bool hayTareaSeleccionada()
        {
            return comboBoxTareas.SelectedItem != null;
        }

        private Tarea tareaSeleccionada()
        {
            return (Tarea)comboBoxTareas.SelectedItem;
        }

        private void VentanaAsignarAntecesoraVentanaDetallesEtapa_Load(object sender, EventArgs e)
        {
            cerrarVentanaSiListaVacia();
        }
    }
}
