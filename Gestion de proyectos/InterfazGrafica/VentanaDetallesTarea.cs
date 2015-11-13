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
        public VentanaDetallesTarea()
        {
            InitializeComponent();
        }

        public VentanaDetallesTarea(Tarea tarea, bool esNueva)
        {
            InitializeComponent();
            this.tarea = tarea;
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

            DeshabilitarControlesParaTareaCompuesta();
            deshabilitarControlesSiEsTareaEditada();
            InicializarListViewAntecesoras();
            InicializarArbolSubtareas();
        }

        private void deshabilitarControlesSiEsTareaEditada()
        {
            if (!esNuevaTarea)
            {
                dateTimePickerFechaFinalizacion.Enabled = false;
                dateTimePickerFechaInicio.Enabled = false;
            }
            else
            {
                dateTimePickerFechaFinalizacion.Enabled = true;
                dateTimePickerFechaInicio.Enabled = true;
            }
        }

        private void InicializarArbolSubtareas()
        {
            treeViewSubtareas.Nodes.Clear();
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
            foreach (Tarea tarea in tareas)
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
            else
            {
                buttonAgregarSubtarea.Enabled = false;
                buttonEliminarSubtarea.Enabled = false;
            }
        }

        private void InicializarListViewAntecesoras()
        {
            listViewAntecesoras.Items.Clear();
            foreach (Tarea tareaActual in tarea.Antecesoras)
            {
                AgregarElemento(tareaActual);
            }
        }

        private void AgregarElemento(Tarea tarea)
        {
            ListViewItem elementoLista = new ListViewItem(tarea.ToString());
            elementoLista.ImageIndex = IconoTarea(tarea);
            elementoLista.Tag = tarea;
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




            bool confirmacion = AyudanteVisual.CartelConfirmacion(CrearMensaje(), "Impacto en la duracion del proyecto");
            if (!confirmacion && esNuevaTarea)
            {
                EliminarTareaActual();
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
            Proyecto padre = tarea.ObtenerProyectoPadre();
            string mensaje;
            if (padre == null)
                mensaje = "La fecha del proyecto se modificará.";
            else
                mensaje = "La fecha del Proyecto se modificara  a: " + padre.FechaFinalizacion + " y su duracion a: " + padre.CalcularDuracionPendiente() + " dias ";
            return mensaje;
        }
        private void EliminarTareaActual()
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

        private void DeshacerCambiosEnTarea(Tarea tareaAnterior)
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
            if (HayTareaSeleccionadaTreeView())
            {
                if(TareaSeleccionada().GetType() == typeof(TareaCompuesta))
                {
                    EditarTareaVentana((TareaCompuesta)TareaSeleccionada(), false);
                }
                else
                {
                    EditarTareaVentana((TareaSimple)TareaSeleccionada(), false);
                }
                
            }
        }

        private Tarea TareaSeleccionada()
        {
            return (Tarea)treeViewSubtareas.SelectedNode.Tag;
        }

        private bool HayTareaSeleccionadaTreeView()
        {
            return treeViewSubtareas.SelectedNode != null;
        }

        private void EditarTareaVentana(Tarea tarea, bool esNuevaTarea)
        {
            VentanaDetallesTarea ventanaDetalles = new VentanaDetallesTarea(tarea, esNuevaTarea);
            ventanaDetalles.ShowDialog(this);
            refrescarVentanaAlCerrarseDialogo();
        }

        private void refrescarVentanaAlCerrarseDialogo()
        {
            foreach (Form formulario in Application.OpenForms)
            {
                if (EstaCerradaVentanaDetallesTarea(formulario))
                {
                    refrescarPantalla();
                    break;
                }
            }
        }

        private bool EstaCerradaVentanaDetallesTarea(Form formulario)
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
                    InicializarListViewAntecesoras();
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
            if (HayTareaSeleccionadaTreeView())
            {
                if(AyudanteVisual.CartelConfirmacion(CrearMensajeElminacion(TareaSeleccionada()), "Eliminación"))
                {
                    TareaCompuesta tareaCompuesta = (TareaCompuesta)tarea;
                    DateTime fechaFinalizacion = tareaCompuesta.FechaFinalizacion;
                    int duracionPendiente = tareaCompuesta.CalcularDuracionPendiente();
                    tareaCompuesta.EliminarSubtarea(TareaSeleccionada());
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


        private string CrearMensajeElminacion(Tarea tarea)
        {
            if (esTareaCompuesta(tarea))
            {
                return "¿Seguro desea eliminar esta subtarea compuesta?\n" +
                    "Esto eliminará todas las subtareas que la componen.";
            }
            else
            {
                return "¿Seguro desea eliminar esta subtarea?";
            }
        }

        private static bool esTareaCompuesta(Tarea tarea)
        {
            return tarea.GetType() == typeof(TareaCompuesta);
        }

        private void buttonAgregarSubtarea_Click(object sender, EventArgs e)
        {
            if (esTareaCompuesta(tarea))
            {
                Tarea tareaNueva = new TareaSimple(new ContextoGestorProyectos());
                ((TareaCompuesta)tarea).Subtareas.Add(tareaNueva);
                VentanaDetallesTarea ventana = new VentanaDetallesTarea((TareaSimple)tareaNueva, true);
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
