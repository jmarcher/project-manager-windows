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
using InterfazGrafica.Utiles;
namespace InterfazGrafica
{
    public partial class VentanaDetallesTarea : Form
    {
        private const int ICONO_TAREA_COMPUESTA = 0;
        private const int ICONO_TAREA_SIMPLE = 1;

        private Tarea tarea;
        private bool esNuevaTarea;
        public VentanaDetallesTarea()
        {
            InitializeComponent();
        }

        public VentanaDetallesTarea(Tarea tarea , bool esNueva)
        {
            InitializeComponent();
            this.tarea = tarea;
            InicializarComponentes(tarea);
            this.esNuevaTarea = esNueva;
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

            DeshabilitarControlesParaTareaCompuesta();

            InicializarListViewAntecesoras();
            InicializarArbolSubtareas();
        }

        private void InicializarArbolSubtareas()
        {
            if (EsCompuesta(tarea))
            {
                PopularArbolConNodosSubtareas();
            }
            else
            {
                DeshabilitarArbolSubtareas();
            }
        }

        private void DeshabilitarArbolSubtareas()
        {
            treeViewSubtareas.Enabled = false;
        }

        private void PopularArbolConNodosSubtareas()
        {
            foreach (Tarea tareaActual in ((TareaCompuesta)tarea).Subtareas)
            {
                treeViewSubtareas.Nodes.Add(GenerarNodoArbol(tareaActual));
            }
        }

        private TreeNode GenerarNodoArbol(Tarea tarea)
        {
            TreeNode nodoRetorno = new TreeNode();
            if (EsCompuesta(tarea))
            {
                nodoRetorno = GenerarNodoArbolTareaCompuesta((TareaCompuesta)tarea);
            }
            else
            {
                nodoRetorno = GenerarNodoArbolTareaSimple(tarea);
            }
            return nodoRetorno;
        }

        private TreeNode[] GenerarArregloNodosListaTareas(List<Tarea> tareas)
        {
            TreeNode[] rama = new TreeNode[tareas.Count];
            int indice = 0;
            foreach(Tarea tarea in tareas)
            {
                if (EsCompuesta(tarea))
                {
                    rama[indice] = GenerarNodoArbolTareaCompuesta((TareaCompuesta)tarea);
                }
                else
                {
                    rama[indice] = GenerarNodoArbolTareaSimple(tarea);
                }
                indice++;
            }
            return rama; 
        }

        private TreeNode GenerarNodoArbolTareaCompuesta(TareaCompuesta tarea)
        {
            TreeNode nodoRetorno = new TreeNode(GenerarTextoAMostrar(tarea),
                GenerarArregloNodosListaTareas(tarea.Subtareas));
            nodoRetorno.Tag = tarea;
            AsignarIconosTareaCompuesta(nodoRetorno);
            return nodoRetorno;
        }

        private void AsignarIconosTareaCompuesta(TreeNode nodoRetorno)
        {
            nodoRetorno.ImageIndex = ICONO_TAREA_COMPUESTA;
            nodoRetorno.SelectedImageIndex = ICONO_TAREA_COMPUESTA;
        }

        private TreeNode GenerarNodoArbolTareaSimple(Tarea tarea)
        {
            TreeNode nodoRetorno = new TreeNode(GenerarTextoAMostrar(tarea));
            nodoRetorno.Tag = tarea;
            AsignarIconosTareaSimple(nodoRetorno);
            return nodoRetorno;
        }



        private static void AsignarIconosTareaSimple(TreeNode nodoArbol)
        {
            nodoArbol.ImageIndex = ICONO_TAREA_SIMPLE;
            nodoArbol.SelectedImageIndex = ICONO_TAREA_SIMPLE;
        }
        private string GenerarTextoAMostrar(Tarea tarea)
        {
            return tarea.ToString();
        }

        private void DeshabilitarControlesParaTareaCompuesta()
        {
            if (EsCompuesta(tarea))
            {
                dateTimePickerFechaFinalizacion.Enabled = false;
                textBoxDuracionPendiente.ReadOnly = true;
            }
        }

        private void InicializarListViewAntecesoras()
        {
            foreach (Tarea tareaActual in tarea.Antecesoras)
            {
                AgregarElemento(tareaActual);
            }
        }

        private void AgregarElemento(Tarea tarea)
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

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            Tarea tareaAnterior = tarea.Clonar();
            

           
           
            bool confirmacion = AyudanteVisual.CartelConfirmacion(CrearMensaje(),"Impacto en la duracion del proyecto");
            if(!confirmacion && esNuevaTarea){
             EliminarTareaActual();
             this.Close();
            }
            else if(!(confirmacion || esNuevaTarea))
            {
                InicializarComponentes(tarea);
            }
            else if (confirmacion) {
                asignarValoresTarea();
            }
        }
       

        private void asignarValoresTarea()
        {
            tarea.Nombre = textBoxNombre.Text;
            tarea.Objetivo = textBoxNombre.Text;
            tarea.DefinirPrioridad(comboBoxPrioridad.Text);
            tarea.Descripcion = textBoxDescripcion.Text;
            tarea.FechaInicio = dateTimePickerFechaInicio.Value;

            if (!EsCompuesta(tarea))
            {
                TareaSimple tareaSimple = (TareaSimple)tarea;
                tareaSimple.DuracionPendiente = Int32.Parse(textBoxDuracionPendiente.Text);
                tareaSimple.FechaFinalizacion = dateTimePickerFechaFinalizacion.Value;
                tarea = tareaSimple;
            }
        }

        private string CrearMensaje()
        {
            string mensaje = "La fecha del Proyecto se modificara  a: " + tarea.ObtenerProyectoPadre().FechaFinalizacion + " y su duracion a: " + tarea.ObtenerProyectoPadre().CalcularDuracionPendiente()+" dias ";
            return mensaje;
        }
        private void EliminarTareaActual() 
        { 
            foreach(Proyecto proyecto in InstanciaUnica.Instancia.DevolverListaProyectos()){
            foreach(Etapa etapa in proyecto.Etapas){
            foreach(Tarea tareaRecorrida in etapa.Tareas){
                if (tareaRecorrida.Equals(this.tarea))
                {
                    etapa.Tareas.Remove(tareaRecorrida);
                    break;
                }
            }
            }
            }
        }

        private void DeshacerCambiosEnTarea(Tarea tareaAnterior)
        {
            foreach (Proyecto proyecto in InstanciaUnica.Instancia.DevolverListaProyectos())
            {
                foreach (Etapa etapa in proyecto.Etapas)
                {
                    foreach (Tarea tareaRecorrida in etapa.Tareas)
                    {
                        if (tareaRecorrida.Equals(this.tarea))
                        {
                            Tarea tareaAModificar = tareaRecorrida;
                            tareaAModificar = tareaAnterior;
                        }
                    }
                }
            }
        }
    }
}
