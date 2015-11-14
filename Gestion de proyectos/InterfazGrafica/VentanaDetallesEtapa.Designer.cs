namespace InterfazGrafica
{
    partial class VentanaDetallesEtapa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaDetallesEtapa));
            this.arbolDeTareas = new System.Windows.Forms.TreeView();
            this.listaImagenes = new System.Windows.Forms.ImageList(this.components);
            this.buttonAgregarTarea = new System.Windows.Forms.Button();
            this.buttonEliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelIdentifiacion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFechaFin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.labelDuracionPendiente = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePickerFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.buttonAsignarAntecesora = new System.Windows.Forms.Button();
            this.buttonAgregarTareaCompuesta = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDuracionEstimada = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // arbolDeTareas
            // 
            this.arbolDeTareas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arbolDeTareas.ImageIndex = 0;
            this.arbolDeTareas.ImageList = this.listaImagenes;
            this.arbolDeTareas.Location = new System.Drawing.Point(13, 174);
            this.arbolDeTareas.Name = "arbolDeTareas";
            this.arbolDeTareas.SelectedImageIndex = 0;
            this.arbolDeTareas.Size = new System.Drawing.Size(688, 211);
            this.arbolDeTareas.TabIndex = 0;
            this.arbolDeTareas.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.arbolDeTareas_AfterSelect);
            this.arbolDeTareas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.arbolDeTareas_MouseDoubleClick);
            // 
            // listaImagenes
            // 
            this.listaImagenes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listaImagenes.ImageStream")));
            this.listaImagenes.TransparentColor = System.Drawing.Color.Transparent;
            this.listaImagenes.Images.SetKeyName(0, "TareaCompuesta");
            this.listaImagenes.Images.SetKeyName(1, "TareaSimple");
            // 
            // buttonAgregarTarea
            // 
            this.buttonAgregarTarea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAgregarTarea.Location = new System.Drawing.Point(111, 391);
            this.buttonAgregarTarea.Name = "buttonAgregarTarea";
            this.buttonAgregarTarea.Size = new System.Drawing.Size(127, 23);
            this.buttonAgregarTarea.TabIndex = 1;
            this.buttonAgregarTarea.Text = "Agregar &tarea simple";
            this.buttonAgregarTarea.UseVisualStyleBackColor = true;
            this.buttonAgregarTarea.Click += new System.EventHandler(this.buttonAgregarTarea_Click);
            // 
            // buttonEliminar
            // 
            this.buttonEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEliminar.Location = new System.Drawing.Point(13, 391);
            this.buttonEliminar.Name = "buttonEliminar";
            this.buttonEliminar.Size = new System.Drawing.Size(92, 23);
            this.buttonEliminar.TabIndex = 3;
            this.buttonEliminar.Text = "&Eliminar tarea";
            this.buttonEliminar.UseVisualStyleBackColor = true;
            this.buttonEliminar.Click += new System.EventHandler(this.buttonEliminar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nombre:";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(123, 35);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(200, 20);
            this.textBoxNombre.TabIndex = 5;
            this.textBoxNombre.TextChanged += new System.EventHandler(this.textBoxNombre_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Identificación:";
            // 
            // labelIdentifiacion
            // 
            this.labelIdentifiacion.AutoSize = true;
            this.labelIdentifiacion.Location = new System.Drawing.Point(120, 13);
            this.labelIdentifiacion.Name = "labelIdentifiacion";
            this.labelIdentifiacion.Size = new System.Drawing.Size(35, 13);
            this.labelIdentifiacion.TabIndex = 7;
            this.labelIdentifiacion.Text = "label3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Fecha inicio:";
            // 
            // textBoxFechaFin
            // 
            this.textBoxFechaFin.Location = new System.Drawing.Point(123, 88);
            this.textBoxFechaFin.Name = "textBoxFechaFin";
            this.textBoxFechaFin.ReadOnly = true;
            this.textBoxFechaFin.Size = new System.Drawing.Size(200, 20);
            this.textBoxFechaFin.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Fecha finalización:";
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGuardar.Enabled = false;
            this.buttonGuardar.Location = new System.Drawing.Point(626, 391);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(75, 23);
            this.buttonGuardar.TabIndex = 12;
            this.buttonGuardar.Text = "&Guardar";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // labelDuracionPendiente
            // 
            this.labelDuracionPendiente.AutoSize = true;
            this.labelDuracionPendiente.Location = new System.Drawing.Point(123, 117);
            this.labelDuracionPendiente.Name = "labelDuracionPendiente";
            this.labelDuracionPendiente.Size = new System.Drawing.Size(35, 13);
            this.labelDuracionPendiente.TabIndex = 14;
            this.labelDuracionPendiente.Text = "label3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Duracion pendiente:";
            // 
            // dateTimePickerFechaInicio
            // 
            this.dateTimePickerFechaInicio.Location = new System.Drawing.Point(123, 62);
            this.dateTimePickerFechaInicio.Name = "dateTimePickerFechaInicio";
            this.dateTimePickerFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFechaInicio.TabIndex = 15;
            this.dateTimePickerFechaInicio.ValueChanged += new System.EventHandler(this.dateTimePickerFechaInicio_ValueChanged);
            // 
            // buttonAsignarAntecesora
            // 
            this.buttonAsignarAntecesora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAsignarAntecesora.Enabled = false;
            this.buttonAsignarAntecesora.Location = new System.Drawing.Point(508, 391);
            this.buttonAsignarAntecesora.Name = "buttonAsignarAntecesora";
            this.buttonAsignarAntecesora.Size = new System.Drawing.Size(112, 23);
            this.buttonAsignarAntecesora.TabIndex = 16;
            this.buttonAsignarAntecesora.Text = "&Asignar antecesora";
            this.buttonAsignarAntecesora.UseVisualStyleBackColor = true;
            this.buttonAsignarAntecesora.Click += new System.EventHandler(this.buttonAsignarAntecesora_Click);
            // 
            // buttonAgregarTareaCompuesta
            // 
            this.buttonAgregarTareaCompuesta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAgregarTareaCompuesta.Location = new System.Drawing.Point(244, 391);
            this.buttonAgregarTareaCompuesta.Name = "buttonAgregarTareaCompuesta";
            this.buttonAgregarTareaCompuesta.Size = new System.Drawing.Size(147, 23);
            this.buttonAgregarTareaCompuesta.TabIndex = 17;
            this.buttonAgregarTareaCompuesta.Text = "Agregar tarea compuesta";
            this.buttonAgregarTareaCompuesta.UseVisualStyleBackColor = true;
            this.buttonAgregarTareaCompuesta.Click += new System.EventHandler(this.buttonAgregarTareaCompuesta_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Duracion estimada:";
            // 
            // textBoxDuracionEstimada
            // 
            this.textBoxDuracionEstimada.Location = new System.Drawing.Point(123, 141);
            this.textBoxDuracionEstimada.Name = "textBoxDuracionEstimada";
            this.textBoxDuracionEstimada.Size = new System.Drawing.Size(200, 20);
            this.textBoxDuracionEstimada.TabIndex = 19;
            this.textBoxDuracionEstimada.TextChanged += new System.EventHandler(this.textBoxDuracionEstimada_TextChanged);
            this.textBoxDuracionEstimada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDuracionEstimada_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(333, 144);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "días.";
            // 
            // VentanaDetallesEtapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 426);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxDuracionEstimada);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonAgregarTareaCompuesta);
            this.Controls.Add(this.buttonAsignarAntecesora);
            this.Controls.Add(this.dateTimePickerFechaInicio);
            this.Controls.Add(this.labelDuracionPendiente);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.textBoxFechaFin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelIdentifiacion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNombre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonEliminar);
            this.Controls.Add(this.buttonAgregarTarea);
            this.Controls.Add(this.arbolDeTareas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VentanaDetallesEtapa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VentanaTarea";
            this.Load += new System.EventHandler(this.VentanaDetallesEtapa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView arbolDeTareas;
        private System.Windows.Forms.ImageList listaImagenes;
        private System.Windows.Forms.Button buttonAgregarTarea;
        private System.Windows.Forms.Button buttonEliminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelIdentifiacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFechaFin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.Label labelDuracionPendiente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaInicio;
        private System.Windows.Forms.Button buttonAsignarAntecesora;
        private System.Windows.Forms.Button buttonAgregarTareaCompuesta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDuracionEstimada;
        private System.Windows.Forms.Label label7;
    }
}