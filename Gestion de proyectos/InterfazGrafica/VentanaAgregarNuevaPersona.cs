using Dominio;
using System.Windows.Forms;
using System;
using InterfazGrafica.Utiles;

namespace InterfazGrafica
{
    public partial class VentanaAgregarNuevaPersona : Form
    {
        private IContextoGestorProyectos contexto;
        private Tarea tarea;

        public VentanaAgregarNuevaPersona(Tarea tarea, IContextoGestorProyectos contexto)
        {
            InitializeComponent();
            this.contexto = contexto;
            this.tarea = tarea;
        }

        private void buttonCancelar_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void buttonGuardar_Click(object sender, System.EventArgs e)
        {
            if (campoNombreEsVacio())
            {
                AyudanteVisual.CartelExclamacion("El nombre de una persona no puede ser vacío.", "Nombre vacío");
            }
            else
            {
                if (campoRolEsVacio())
                {
                    AyudanteVisual.CartelExclamacion("El rol de una persona no puede ser vacío.", "Rol vacío");
                }
                else
                {
                    Persona personaAAgregar = new Persona()
                    {
                        Nombre = textBoxNombre.Text,
                        Rol = textBoxRol.Text
                    };
                    tarea.AgregarPersona(personaAAgregar);
                    tarea.AgregarModificacion("Se agregó persona " + textBoxNombre.Text);
                    contexto.ModificarTarea(tarea);
                    Close();
                }
            }
        }

        private bool campoRolEsVacio()
        {
            return textBoxRol.Text.Trim().Equals(String.Empty);
        }

        private bool campoNombreEsVacio()
        {
            return textBoxNombre.Text.Trim().Equals(String.Empty);
        }
    }
}
