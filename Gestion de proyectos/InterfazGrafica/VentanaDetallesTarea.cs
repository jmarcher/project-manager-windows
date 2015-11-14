using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dominio;
using InterfazGrafica.Utiles;
using PersistenciaImp;

namespace InterfazGrafica
{
    public partial class VentanaDetallesTarea : Form
    {
        private const int ICONO_TAREA_COMPUESTA = 0;
        private const int ICONO_TAREA_SIMPLE = 1;

        private Tarea tarea;
        private bool esNuevaTarea;
        private IContextoGestorProyectos contexto;
        public VentanaDetallesTarea()
        {
            InitializeComponent();
        }

        public VentanaDetallesTarea(Tarea tarea, bool esNueva, IContextoGestorProyectos contexto)
        {
            InitializeComponent();
            this.contexto = contexto;
            this.tarea =  contexto.ObtenerTarea(tarea.TareaID);
            this.tarea.Contexto = contexto;
            this.esNuevaTarea = esNueva;
            InicializarComponentes(tarea);
        }

        private void InicializarComponentes(Tarea tarea)
        {
            refrescarPantalla();
        }

        private void refrescarPantalla()
        {
            Text = "Tarea: " + tarea.Nombre + ", fecha inicio: " + tarea.FechaInicio.ToShortDateString();
            textBoxNombre.Text = tarea.Nombre;
            textBoxObjetivo.Text = tarea.Objetivo;
            textBoxDuracionPendiente.Text = tarea.CalcularDuracionPendiente().ToString();
            textBoxDescripcion.Text = tarea.Descripcion;
            dateTimePickerFechaInicio.Value = tarea.FechaInicio;
            dateTimePickerFechaFinalizacion.Value = tarea.FechaFinalizacion;
            comboBoxPrioridad.SelectedIndex = tarea.Prioridad;
            textBoxDuracionEstimada.Text = tarea.DuracionEstimada.ToString();

            deshabilitarControlesParaTareaCompuesta();
            deshabilitarControlesSiEsTareaEditada();
            inicializarListViewAntecesoras();
            inicializarArbolSubtareas();
        }

        private void deshabilitarControlesSiEsTareaEditada()
        {
            if (!esNuevaTarea)
            {
                dateTimePickerFechaFinalizacion.Enabled = false;
                dateTimePickerFechaInicio.Enabled = false;
                textBoxDuracionEstimada.ReadOnly = true;
            }
            else
            {
                dateTimePickerFechaFinalizacion.Enabled = true;
                dateTimePickerFechaInicio.Enabled = true;
                textBoxDuracionEstimada.ReadOnly = false;
            }
        }

        private void inicializarArbolSubtareas()
        {
            treeViewSubtareas.Nodes.Clear();
            if (esCompuesta(tarea))
            {
                popularArbolConNodosSubtareas();
            }
            else
            {
                seshabilitarArbolSubtareas();
            }
        }

        private void seshabilitarArbolSubtareas()
        {
            treeViewSubtareas.Enabled = false;
        }

        private void popularArbolConNodosSubtareas()
        {
            foreach (Tarea tareaActual in ((TareaCompuesta)tarea).Subtareas)
            {
                treeViewSubtareas.Nodes.Add(generarNodoArbol(tareaActual));
            }
        }

        private TreeNode generarNodoArbol(Tarea tarea)
        {
            TreeNode nodoRetorno = new TreeNode();
            if (esCompuesta(tarea))
            {
                nodoRetorno = generarNodoArbolTareaCompuesta((TareaCompuesta)tarea);
            }
            else
            {
                nodoRetorno = generarNodoArbolTareaSimple(tarea);
            }
            return nodoRetorno;
        }

        private TreeNode[] generarArregloNodosListaTareas(List<Tarea> tareas)
        {
            TreeNode[] rama = new TreeNode[tareas.Count];
            int indice = 0;
            foreach (Tarea tarea in tareas)
            {
                if (esCompuesta(tarea))
                {
                    rama[indice] = generarNodoArbolTareaCompuesta((TareaCompuesta)tarea);
                }
                else
                {
                    rama[indice] = generarNodoArbolTareaSimple(tarea);
                }
                indice++;
            }
            return rama;
        }

        private TreeNode generarNodoArbolTareaCompuesta(TareaCompuesta tarea)
        {
            TreeNode nodoRetorno = new TreeNode(generarTextoAMostrar(tarea),
                generarArregloNodosListaTareas(tarea.Subtareas));
            nodoRetorno.Tag = tarea;
            asignarIconosTareaCompuesta(nodoRetorno);
            return nodoRetorno;
        }

        private void asignarIconosTareaCompuesta(TreeNode nodoRetorno)
        {
            nodoRetorno.ImageIndex = ICONO_TAREA_COMPUESTA;
            nodoRetorno.SelectedImageIndex = ICONO_TAREA_COMPUESTA;
        }

        private TreeNode generarNodoArbolTareaSimple(Tarea tarea)
        {
            TreeNode nodoRetorno = new TreeNode(generarTextoAMostrar(tarea));
            nodoRetorno.Tag = tarea;
            asignarIconosTareaSimple(nodoRetorno);
            return nodoRetorno;
        }



        private static void asignarIconosTareaSimple(TreeNode nodoArbol)
        {
            nodoArbol.ImageIndex = ICONO_TAREA_SIMPLE;
            nodoArbol.SelectedImageIndex = ICONO_TAREA_SIMPLE;
        }
        private string generarTextoAMostrar(Tarea tarea)
        {
            return tarea.ToString();
        }

        private void deshabilitarControlesParaTareaCompuesta()
        {
            if (esCompuesta(tarea))
            {
                dateTimePickerFechaFinalizacion.Enabled = false;
                textBoxDuracionPendiente.ReadOnly = true;
            }
            else
            {
                buttonAgregarSubtarea.Enabled = false;
                buttonEliminarSubtarea.Enabled = false;
            }
        }

        private void inicializarListViewAntecesoras()
        {
            listViewAntecesoras.Items.Clear();
            foreach (Tarea tareaActual in tarea.Antecesoras)
            {
                agregarElemento(tareaActual);
            }
        }

        private void agregarElemento(Tarea tarea)
        {
            ListViewItem elementoLista = new ListViewItem(tarea.ToString());
            elementoLista.ImageIndex = iconoTarea(tarea);
            elementoLista.Tag = tarea;
            listViewAntecesoras.Items.Add(elementoLista);
        }

        private int iconoTarea(Tarea tarea)
        {
            if (esCompuesta(tarea))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private bool esCompuesta(Tarea tarea)
        {
            return tarea.GetType() == typeof(TareaCompuesta) || tarea.GetType().BaseType == typeof(TareaCompuesta);
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            Tarea tareaAnterior = tarea.Clonar();
            bool confirmacion = AyudanteVisual.CartelConfirmacion(crearMensaje(), "Impacto en la duracion del proyecto");
            if (!confirmacion && esNuevaTarea)
            {
                eliminarTareaActual();
                this.Close();
            }
            else if (!(confirmacion || esNuevaTarea))
            {
                InicializarComponentes(tarea);
            }
            else if (confirmacion)
            {
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

            if (!esCompuesta(tarea))
            {
                TareaSimple tareaSimple = (TareaSimple)tarea;
                tareaSimple.DuracionPendiente = Int32.Parse(textBoxDuracionPendiente.Text);
                tareaSimple.FechaFinalizacion = dateTimePickerFechaFinalizacion.Value;
                tarea = tareaSimple;
            }
        }

        private string crearMensaje()
        {
            Proyecto padre = tarea.ObtenerProyectoPadre();
            string mensaje;
            if (padre == null)
                mensaje = "La fecha del proyecto se modificará.";
            else
                mensaje = "La fecha del Proyecto se modificara  a: " + padre.FechaFinalizacion + " y su duracion a: " + padre.CalcularDuracionPendiente() + " dias ";
            return mensaje;
        }
        private void eliminarTareaActual()
        {
            foreach (Proyecto proyecto in InstanciaUnica.Instancia.DevolverProyectos())
            {
                foreach (Etapa etapa in proyecto.Etapas)
                {
                    foreach (Tarea tareaRecorrida in etapa.Tareas)
                    {
                        if (tareaRecorrida.Equals(this.tarea))
                        {
                            etapa.Tareas.Remove(tareaRecorrida);
                            break;
                        }
                    }
                }
            }
        }

        private void deshacerCambiosEnTarea(Tarea tareaAnterior)
        {
            foreach (Proyecto proyecto in InstanciaUnica.Instancia.DevolverProyectos())
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

        private void treeViewSubtareas_DoubleClick(object sender, EventArgs e)
        {
            if (hayTareaSeleccionadaTreeView())
            {
                if(tareaSeleccionada().GetType() == typeof(TareaCompuesta))
                {
                    editarTareaVentana((TareaCompuesta)tareaSeleccionada(), false);
                }
                else
                {
                    editarTareaVentana((TareaSimple)tareaSeleccionada(), false);
                }
                
            }
        }

        private Tarea tareaSeleccionada()
        {
            return (Tarea)treeViewSubtareas.SelectedNode.Tag;
        }

        private bool hayTareaSeleccionadaTreeView()
        {
            return treeViewSubtareas.SelectedNode != null;
        }

        private void editarTareaVentana(Tarea tarea, bool esNuevaTarea)
        {
            VentanaDetallesTarea ventanaDetalles = new VentanaDetallesTarea(tarea, esNuevaTarea, contexto);
            ventanaDetalles.ShowDialog(this);
            refrescarVentanaAlCerrarseDialogo();
        }

        private void refrescarVentanaAlCerrarseDialogo()
        {
            foreach (Form formulario in Application.OpenForms)
            {
                if (estaCerradaVentanaDetallesTarea(formulario))
                {
                    refrescarPantalla();
                    break;
                }
            }
        }

        private bool estaCerradaVentanaDetallesTarea(Form formulario)
        {
            return !(formulario.GetType() == typeof(VentanaDetallesTarea));
        }

        private void buttonEliminarAntecesora_Click(object sender, EventArgs e)
        {
            if (HayAntecesoraSeleccionadaListView())
            {
                if (AyudanteVisual.CartelConfirmacion("¿Seguro desea eliminar esta tarea antecesora?", "Eliminación"))
                {
                    borrarAntecesora();
                    inicializarListViewAntecesoras();
                    inicializarBotonEliminarAntecesora();
                }
            }
        }

        private void borrarAntecesora()
        {
            Tarea seleccionada = antecesoraSeleccionada();
            agregarAntecesorasAntesElminacion(seleccionada);
            tarea.Antecesoras.Remove(antecesoraSeleccionada());
        }

        private void agregarAntecesorasAntesElminacion(Tarea seleccionada)
        {
            foreach (Tarea antecesora in seleccionada.Antecesoras)
            {
                if (!tarea.Antecesoras.Contains(antecesora))
                {
                    tarea.Antecesoras.Add(antecesora);
                }
            }
        }

        private Tarea antecesoraSeleccionada()
        {
            return (Tarea)listViewAntecesoras.SelectedItems[0].Tag;
        }

        private bool HayAntecesoraSeleccionadaListView()
        {
            return listViewAntecesoras.SelectedItems.Count > 0;
        }

        private void listViewAntecesoras_SelectedIndexChanged(object sender, EventArgs e)
        {
            inicializarBotonEliminarAntecesora();
        }

        private void inicializarBotonEliminarAntecesora()
        {
            if (listViewAntecesoras.Items.Count > 0 && HayAntecesoraSeleccionadaListView())
            {
                buttonEliminarAntecesora.Enabled = true;
            }
            else
            {
                buttonEliminarAntecesora.Enabled = false;
            }
        }

        private void buttonEliminarSubtarea_Click(object sender, EventArgs e)
        {
            if (hayTareaSeleccionadaTreeView())
            {
                if(AyudanteVisual.CartelConfirmacion(crearMensajeElminacion(tareaSeleccionada()), "Eliminación"))
                {
                    TareaCompuesta tareaCompuesta = (TareaCompuesta)tarea;
                    DateTime fechaFinalizacion = tareaCompuesta.FechaFinalizacion;
                    int duracionPendiente = tareaCompuesta.CalcularDuracionPendiente();
                    tareaCompuesta.EliminarSubtarea(tareaSeleccionada());
                    cambiarATareaSimpleSimple(tareaCompuesta, fechaFinalizacion, duracionPendiente);
                    refrescarPantalla();
                }
            }
        }

        private void cambiarATareaSimpleSimple(TareaCompuesta tareaCompuesta, DateTime fechaFinalizacion, int duracionPendiente)
        {
            if (tareaCompuesta.Subtareas.Count == 0)
            {
                tarea = new TareaSimple(new ContextoGestorProyectos())
                {
                    Nombre = tarea.Nombre,
                    FechaInicio = tareaCompuesta.FechaInicio,
                    FechaFinalizacion = fechaFinalizacion,
                    DuracionPendiente = duracionPendiente,
                    Prioridad = tareaCompuesta.Prioridad,
                    Antecesoras = tareaCompuesta.Antecesoras,
                    Descripcion = tareaCompuesta.Descripcion,
                    Objetivo = tareaCompuesta.Objetivo
                };
            }
        }


        private string crearMensajeElminacion(Tarea tarea)
        {
            if (esCompuesta(tarea))
            {
                return "¿Seguro desea eliminar esta subtarea compuesta?\n" +
                    "Esto eliminará todas las subtareas que la componen.";
            }
            else
            {
                return "¿Seguro desea eliminar esta subtarea?";
            }
        }


        private void buttonAgregarSubtarea_Click(object sender, EventArgs e)
        {
            if (esCompuesta(tarea))
            {
                Tarea tareaNueva = new TareaSimple(contexto);
                ((TareaCompuesta)tarea).Subtareas.Add(tareaNueva);
                contexto.ModificarTarea(tarea);
                VentanaDetallesTarea ventana = new VentanaDetallesTarea((TareaSimple)tareaNueva, true, contexto);
                ventana.ShowDialog();
                refrescarVentanaAlCerrarseDialogo();
            }
            else
            {
                AyudanteVisual.CartelExclamacion("Esta es una tarea simple no se le pueden agregar subtareas.", "Acción no posible.");
            }
        }

    }
}
