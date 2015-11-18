using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Dominio;
using InterfazGrafica.Utiles;
using PersistenciaImp;
using System.Drawing;
using DominioInterfaz;
using PersistenciaInterfaz;

namespace InterfazGrafica
{
    public partial class VentanaDetallesTarea : Form
    {
        private const int ICONO_TAREA_COMPUESTA = 0;
        private const int ICONO_TAREA_SIMPLE = 1;

        private ITarea tarea;
        private bool esNuevaTarea;
        private IContextoGestorProyectos contexto;
        public VentanaDetallesTarea()
        {
            InitializeComponent();
        }

        public VentanaDetallesTarea(ITarea tarea, bool esNueva, IContextoGestorProyectos contexto)
        {
            InitializeComponent();
            this.contexto = contexto;
            this.tarea =  contexto.ObtenerTarea(tarea.TareaID);
            this.tarea.Contexto = contexto;
            this.esNuevaTarea = esNueva;
            InicializarComponentes();
        }

        private void InicializarComponentes()
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
            inicializarListViewPersonas();
            inicializarAvance();
        }

        private void inicializarAvance()
        {
            if (tarea.DuracionEstimada != 0)
            {
                labelAvance.Text = "Avance: " + calcularAvance().ToString() + "%";
                inicializarColorAvance();
            }
            else
                labelAvance.Text = String.Empty;
        }

        private void inicializarColorAvance()
        {
            if (calcularAvance() < 10)
            {
                labelAvance.ForeColor = Color.Red;
            }
            else if (calcularAvance() > 50)
            {
                labelAvance.ForeColor = Color.Yellow;
            }
            else if (calcularAvance() > 80)
            {
                labelAvance.ForeColor = Color.Green;
            }
        }

        private int calcularAvance()
        {
            return ((DateTime.Now.Subtract(tarea.FechaInicio).Days * 100)
                            / tarea.DuracionEstimada) % 101;
        }

        private void inicializarListViewPersonas()
        {
            listViewPersonas.Items.Clear();
            foreach (Persona personaActual in tarea.Personas)
            {
                agregarElementoListaPersonas(personaActual);
            }
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
            foreach (ITarea tareaActual in ((TareaCompuesta)tarea).Subtareas)
            {
                treeViewSubtareas.Nodes.Add(generarNodoArbol(tareaActual));
            }
        }

        private TreeNode generarNodoArbol(ITarea tarea)
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
            foreach (ITarea tarea in tareas)
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

        private TreeNode generarNodoArbolTareaSimple(ITarea tarea)
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
        private string generarTextoAMostrar(ITarea tarea)
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
            foreach (ITarea tareaActual in tarea.Antecesoras)
            {
                agregarElementoListaAntecesoras(tareaActual);
            }
        }

        private void agregarElementoListaAntecesoras(ITarea tarea)
        {
            ListViewItem elementoLista = generarElementoListView(tarea);
            elementoLista.ImageIndex = iconoTarea(tarea);
            listViewAntecesoras.Items.Add(elementoLista);
        }

        private ListViewItem generarElementoListView(object obj)
        {
            ListViewItem elementoLista = new ListViewItem(obj.ToString());
            elementoLista.Tag = obj;
            return elementoLista;
        }

        private void agregarElementoListaPersonas(Persona personaActual)
        {
            ListViewItem elementoLista = generarElementoListView(personaActual);
            listViewPersonas.Items.Add(elementoLista);
        }

        private int iconoTarea(ITarea tarea)
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

        private bool esCompuesta(ITarea tarea)
        {
            return tarea.GetType() == typeof(TareaCompuesta) || tarea.GetType().BaseType == typeof(TareaCompuesta);
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            ITarea tareaAnterior = tarea.Clonar();
            bool confirmacion = AyudanteVisual.CartelConfirmacion(crearMensaje(), "Impacto en la duracion del proyecto");
            if (!confirmacion && esNuevaTarea)
            {
                eliminarTareaActual();
                this.Close();
            }
            else if (!(confirmacion || esNuevaTarea))
            {
                InicializarComponentes();
            }
            else if (confirmacion)
            {
                asignarValoresTarea();
            }
            Close();
        }


        private void asignarValoresTarea()
        {
            cambiarNombreTarea();
            cambiarObjetivoTarea();
            cambiarPrioridadTarea();
            cambiarDescripcionTarea();
            cambiarFechaDeInicio();
            IProyecto padre = tarea.ObtenerProyectoPadre();
            String modificacion = "La duración pendiente se modificó de " + padre.CalcularDuracionPendiente();
            if (!esCompuesta(tarea))
            {
                
                TareaSimple tareaSimple = (TareaSimple)tarea;
                cambiarDuracionPendiente(tareaSimple);
                cambiarFechaFinalizacion(tareaSimple);
                tarea = tareaSimple;
            }
            tarea.DuracionEstimada = Int32.Parse(textBoxDuracionEstimada.Text);
            contexto.ModificarTarea(tarea);
            padre = tarea.ObtenerProyectoPadre();
            modificacion += " a " + padre.CalcularDuracionPendiente();
            padre.AgregarModificacion(modificacion);
            contexto.ModificarProyecto(padre);
        }

        private void cambiarFechaFinalizacion(TareaSimple tareaSimple)
        {
            if(tareaSimple.FechaFinalizacion != dateTimePickerFechaFinalizacion.Value)
            {
                tareaSimple.AgregarModificacion("Cambiada la fecha de finalización de " + tareaSimple.FechaFinalizacion.ToShortDateString()
                    + " a " + dateTimePickerFechaFinalizacion.Value.ToShortDateString());
                tareaSimple.FechaFinalizacion = dateTimePickerFechaFinalizacion.Value;
            }
        }

        private void cambiarDuracionPendiente(TareaSimple tareaSimple)
        {
            if(tareaSimple.DuracionPendiente != Int32.Parse(textBoxDuracionPendiente.Text))
            {
                tareaSimple.AgregarModificacion("Cambiada la duración de "+tareaSimple.DuracionPendiente.ToString()
                    +" a "+ textBoxDuracionPendiente.Text);
                tareaSimple.DuracionPendiente = Int32.Parse(textBoxDuracionPendiente.Text);
            }
        }

        private void cambiarFechaDeInicio()
        {
            if (tarea.FechaInicio != dateTimePickerFechaInicio.Value)
            {
                tarea.AgregarModificacion("Cambiada la fecha de inicio de "+tarea.FechaInicio.ToShortDateString()
                    + " a "+ dateTimePickerFechaInicio.Value.ToShortDateString());
                tarea.FechaInicio = dateTimePickerFechaInicio.Value;
            }
        }

        private void cambiarDescripcionTarea()
        {
            if (!tarea.Descripcion.Equals(textBoxDescripcion.Text))
            {
                tarea.AgregarModificacion("Cambiada la descripcion de ''"
                    + tarea.Descripcion + "'' a ''" + textBoxDescripcion.Text + "''");
                tarea.Descripcion = textBoxDescripcion.Text;
            }
        }

        private void cambiarPrioridadTarea()
        {
            if (!tarea.prioridadAString().Equals(comboBoxPrioridad.Text))
            {
                tarea.AgregarModificacion("Prioridad cambiada de " + tarea.prioridadAString()
                    + " a " + comboBoxPrioridad.Text);
                tarea.DefinirPrioridad(comboBoxPrioridad.Text);
            }
        }

        private void cambiarObjetivoTarea()
        {
            if (!tarea.Objetivo.Equals(textBoxObjetivo.Text))
            {
                tarea.AgregarModificacion("Cambiado el objetivo de ''"
                    + tarea.Objetivo + "'' a ''" + textBoxObjetivo.Text + "''");
                tarea.Objetivo = textBoxObjetivo.Text;
            }
        }

        private void cambiarNombreTarea()
        {
            if (!tarea.Nombre.Equals(textBoxNombre.Text))
            {
                tarea.AgregarModificacion("Cambiado nombre de ''"
                    + tarea.Nombre + "'' a ''" + textBoxNombre.Text + "''");
                    tarea.Nombre = textBoxNombre.Text;
            }
        }

        private string crearMensaje()
        {
            IProyecto padre = tarea.ObtenerProyectoPadre();
            string mensaje;
            if (padre == null)
                mensaje = "La fecha del proyecto se modificará.";
            else
                mensaje = "La fecha del Proyecto se modificara  a: " + padre.FechaFinalizacion + " y su duracion a: " + padre.CalcularDuracionPendiente() + " dias ";
            return mensaje;
        }
        private void eliminarTareaActual()
        {
            foreach (IProyecto proyecto in contexto.DevolverProyectos())
            {
                foreach (IEtapa etapa in proyecto.Etapas)
                {
                    foreach (Tarea tareaRecorrida in etapa.Tareas)
                    {
                        if (tareaRecorrida.Equals(this.tarea))
                        {
                            etapa.Tareas.Remove(tareaRecorrida);
                            contexto.ModificarEtapa(etapa);
                            break;
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

        private void editarTareaVentana(ITarea tarea, bool esNuevaTarea)
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
            if (hayAntecesoraSeleccionadaListView())
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
            ITarea seleccionada = antecesoraSeleccionada();
            agregarAntecesorasAntesElminacion(seleccionada);
            tarea.Antecesoras.Remove(antecesoraSeleccionada());
            tarea.AgregarModificacion("Eliminada antecesora " + seleccionada.ToString());
            contexto.ModificarTarea(tarea);
        }

        private void agregarAntecesorasAntesElminacion(ITarea seleccionada)
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

        private bool hayAntecesoraSeleccionadaListView()
        {
            return listViewAntecesoras.SelectedItems.Count > 0;
        }

        private void listViewAntecesoras_SelectedIndexChanged(object sender, EventArgs e)
        {
            inicializarBotonEliminarAntecesora();
        }

        private void inicializarBotonEliminarAntecesora()
        {
            if (listViewAntecesoras.Items.Count > 0 && hayAntecesoraSeleccionadaListView())
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
                    tareaCompuesta.AgregarModificacion("Eliminada subtarea " + tareaSeleccionada().ToString());
                    contexto.ModificarTarea(tareaCompuesta);
                    refrescarPantalla();
                }
            }
        }

        private void cambiarATareaSimpleSimple(TareaCompuesta tareaCompuesta, DateTime fechaFinalizacion, int duracionPendiente)
        {
            if (tareaCompuesta.Subtareas.Count == 0)
            {
                tarea = new TareaSimple(contexto)
                {
                    Nombre = tarea.Nombre,
                    FechaInicio = tareaCompuesta.FechaInicio,
                    FechaFinalizacion = fechaFinalizacion,
                    DuracionPendiente = duracionPendiente,
                    Prioridad = tareaCompuesta.Prioridad,
                    Antecesoras = tareaCompuesta.Antecesoras,
                    Descripcion = tareaCompuesta.Descripcion,
                    Objetivo = tareaCompuesta.Objetivo,
                    Personas = tareaCompuesta.Personas
                   
                };
            }
        }


        private string crearMensajeElminacion(ITarea tarea)
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
                tarea.AgregarModificacion("Agregada una subtarea.");
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

        private void buttonAgregarPersona_Click(object sender, EventArgs e)
        {
            VentanaAgregarNuevaPersona ventanaAgregarPersona = new VentanaAgregarNuevaPersona(tarea, contexto);
            ventanaAgregarPersona.ShowDialog(this);
            refrescarVentanaCuandoCierraVentanaPersona();
        }

        private void refrescarVentanaCuandoCierraVentanaPersona()
        {
            foreach (Form formulario in Application.OpenForms)
            {
                if (estaCerradaVentanaPersona(formulario))
                {
                    refrescarPantalla();
                    break;
                }
            }
        }

        private static bool estaCerradaVentanaPersona(Form formulario)
        {
            return !(formulario.GetType() == typeof(VentanaAgregarNuevaPersona));
        }

        private void buttonVerHistorial_Click(object sender, EventArgs e)
        {
            VentanaVerHistorialTarea ventanaHistorial = new VentanaVerHistorialTarea(tarea);
            ventanaHistorial.ShowDialog(this);
        }

        private void buttonEliminarPersona_Click(object sender, EventArgs e)
        {
            if (hayPersonaSeleccionada())
            {
                if (AyudanteVisual.CartelConfirmacion("¿Seguro desea eliminar esta persona?", "Eliminación"))
                {
                    borrarPersona();
                    inicializarListViewPersonas();
                }
            }
        }

        private void borrarPersona()
        {
            tarea.Personas.Remove(personaSeleccionada());
            tarea.AgregarModificacion("Eliminada la persona " + personaSeleccionada().ToString());
            contexto.ModificarTarea(tarea);
        }

        private Persona personaSeleccionada()
        {
            return (Persona) listViewPersonas.SelectedItems[0].Tag;
        }

        private bool hayPersonaSeleccionada()
        {
            return listViewPersonas.SelectedItems.Count > 0;
        }

        private void textBoxDuracionPendiente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxDuracionEstimada_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
