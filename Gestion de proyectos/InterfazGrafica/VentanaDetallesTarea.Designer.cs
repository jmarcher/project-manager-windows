﻿namespace InterfazGrafica
{
    partial class VentanaDetallesTarea
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaDetallesTarea));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxObjetivo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDescripcion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDuracionPendiente = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFechaFinalizacion = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxPrioridad = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.listViewAntecesoras = new System.Windows.Forms.ListView();
            this.listaImagenes = new System.Windows.Forms.ImageList(this.components);
            this.treeViewSubtareas = new System.Windows.Forms.TreeView();
            this.buttonEliminarAntecesora = new System.Windows.Forms.Button();
            this.buttonAgregarSubtarea = new System.Windows.Forms.Button();
            this.buttonEliminarSubtarea = new System.Windows.Forms.Button();
            this.textBoxDuracionEstimada = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.listViewPersonas = new System.Windows.Forms.ListView();
            this.buttonAgregarPersona = new System.Windows.Forms.Button();
            this.buttonEliminarPersona = new System.Windows.Forms.Button();
            this.buttonVerHistorial = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.labelAvance = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre:";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(133, 13);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(257, 20);
            this.textBoxNombre.TabIndex = 1;
            // 
            // textBoxObjetivo
            // 
            this.textBoxObjetivo.Location = new System.Drawing.Point(133, 39);
            this.textBoxObjetivo.Name = "textBoxObjetivo";
            this.textBoxObjetivo.Size = new System.Drawing.Size(257, 20);
            this.textBoxObjetivo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Objetivo:";
            // 
            // textBoxDescripcion
            // 
            this.textBoxDescripcion.Location = new System.Drawing.Point(133, 65);
            this.textBoxDescripcion.Name = "textBoxDescripcion";
            this.textBoxDescripcion.Size = new System.Drawing.Size(257, 20);
            this.textBoxDescripcion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Descripción:";
            // 
            // textBoxDuracionPendiente
            // 
            this.textBoxDuracionPendiente.Location = new System.Drawing.Point(133, 91);
            this.textBoxDuracionPendiente.Name = "textBoxDuracionPendiente";
            this.textBoxDuracionPendiente.Size = new System.Drawing.Size(119, 20);
            this.textBoxDuracionPendiente.TabIndex = 7;
            this.textBoxDuracionPendiente.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDuracionPendiente_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Duracion pendiente:";
            // 
            // dateTimePickerFechaInicio
            // 
            this.dateTimePickerFechaInicio.Location = new System.Drawing.Point(533, 10);
            this.dateTimePickerFechaInicio.Name = "dateTimePickerFechaInicio";
            this.dateTimePickerFechaInicio.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFechaInicio.TabIndex = 8;
            this.dateTimePickerFechaInicio.ValueChanged += new System.EventHandler(this.dateTimePickerFechaInicio_ValueChanged);
            // 
            // dateTimePickerFechaFinalizacion
            // 
            this.dateTimePickerFechaFinalizacion.Location = new System.Drawing.Point(533, 37);
            this.dateTimePickerFechaFinalizacion.Name = "dateTimePickerFechaFinalizacion";
            this.dateTimePickerFechaFinalizacion.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFechaFinalizacion.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(414, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Fecha inicio:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(414, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Fecha finalización:";
            // 
            // comboBoxPrioridad
            // 
            this.comboBoxPrioridad.FormattingEnabled = true;
            this.comboBoxPrioridad.Items.AddRange(new object[] {
            "Baja",
            "Media",
            "Alta"});
            this.comboBoxPrioridad.Location = new System.Drawing.Point(533, 64);
            this.comboBoxPrioridad.Name = "comboBoxPrioridad";
            this.comboBoxPrioridad.Size = new System.Drawing.Size(200, 21);
            this.comboBoxPrioridad.TabIndex = 12;
            this.comboBoxPrioridad.Text = "Media";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(414, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Prioridad:";
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGuardar.Location = new System.Drawing.Point(850, 411);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(75, 23);
            this.buttonGuardar.TabIndex = 14;
            this.buttonGuardar.Text = "&Guardar";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // listViewAntecesoras
            // 
            this.listViewAntecesoras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewAntecesoras.Location = new System.Drawing.Point(17, 141);
            this.listViewAntecesoras.Name = "listViewAntecesoras";
            this.listViewAntecesoras.Size = new System.Drawing.Size(266, 256);
            this.listViewAntecesoras.SmallImageList = this.listaImagenes;
            this.listViewAntecesoras.TabIndex = 15;
            this.listViewAntecesoras.UseCompatibleStateImageBehavior = false;
            this.listViewAntecesoras.View = System.Windows.Forms.View.List;
            this.listViewAntecesoras.SelectedIndexChanged += new System.EventHandler(this.listViewAntecesoras_SelectedIndexChanged);
            // 
            // listaImagenes
            // 
            this.listaImagenes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listaImagenes.ImageStream")));
            this.listaImagenes.TransparentColor = System.Drawing.Color.Transparent;
            this.listaImagenes.Images.SetKeyName(0, "TareaCompuesta");
            this.listaImagenes.Images.SetKeyName(1, "TareaSimple");
            // 
            // treeViewSubtareas
            // 
            this.treeViewSubtareas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewSubtareas.ImageIndex = 0;
            this.treeViewSubtareas.ImageList = this.listaImagenes;
            this.treeViewSubtareas.Location = new System.Drawing.Point(290, 141);
            this.treeViewSubtareas.Name = "treeViewSubtareas";
            this.treeViewSubtareas.SelectedImageIndex = 0;
            this.treeViewSubtareas.Size = new System.Drawing.Size(390, 256);
            this.treeViewSubtareas.TabIndex = 16;
            this.treeViewSubtareas.DoubleClick += new System.EventHandler(this.treeViewSubtareas_DoubleClick);
            // 
            // buttonEliminarAntecesora
            // 
            this.buttonEliminarAntecesora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEliminarAntecesora.Enabled = false;
            this.buttonEliminarAntecesora.Location = new System.Drawing.Point(17, 411);
            this.buttonEliminarAntecesora.Name = "buttonEliminarAntecesora";
            this.buttonEliminarAntecesora.Size = new System.Drawing.Size(27, 23);
            this.buttonEliminarAntecesora.TabIndex = 17;
            this.buttonEliminarAntecesora.Text = "-";
            this.buttonEliminarAntecesora.UseVisualStyleBackColor = true;
            this.buttonEliminarAntecesora.Click += new System.EventHandler(this.buttonEliminarAntecesora_Click);
            // 
            // buttonAgregarSubtarea
            // 
            this.buttonAgregarSubtarea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAgregarSubtarea.Location = new System.Drawing.Point(290, 411);
            this.buttonAgregarSubtarea.Name = "buttonAgregarSubtarea";
            this.buttonAgregarSubtarea.Size = new System.Drawing.Size(28, 23);
            this.buttonAgregarSubtarea.TabIndex = 18;
            this.buttonAgregarSubtarea.Text = "+";
            this.buttonAgregarSubtarea.UseVisualStyleBackColor = true;
            this.buttonAgregarSubtarea.Click += new System.EventHandler(this.buttonAgregarSubtarea_Click);
            // 
            // buttonEliminarSubtarea
            // 
            this.buttonEliminarSubtarea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEliminarSubtarea.Location = new System.Drawing.Point(324, 411);
            this.buttonEliminarSubtarea.Name = "buttonEliminarSubtarea";
            this.buttonEliminarSubtarea.Size = new System.Drawing.Size(28, 23);
            this.buttonEliminarSubtarea.TabIndex = 19;
            this.buttonEliminarSubtarea.Text = "-";
            this.buttonEliminarSubtarea.UseVisualStyleBackColor = true;
            this.buttonEliminarSubtarea.Click += new System.EventHandler(this.buttonEliminarSubtarea_Click);
            // 
            // textBoxDuracionEstimada
            // 
            this.textBoxDuracionEstimada.Location = new System.Drawing.Point(533, 91);
            this.textBoxDuracionEstimada.Name = "textBoxDuracionEstimada";
            this.textBoxDuracionEstimada.Size = new System.Drawing.Size(200, 20);
            this.textBoxDuracionEstimada.TabIndex = 21;
            this.textBoxDuracionEstimada.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDuracionEstimada_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(414, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Duracion estimada:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Antecesoras:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(287, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Subtareas:";
            // 
            // listViewPersonas
            // 
            this.listViewPersonas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewPersonas.Location = new System.Drawing.Point(686, 141);
            this.listViewPersonas.Name = "listViewPersonas";
            this.listViewPersonas.Size = new System.Drawing.Size(239, 256);
            this.listViewPersonas.SmallImageList = this.listaImagenes;
            this.listViewPersonas.TabIndex = 24;
            this.listViewPersonas.UseCompatibleStateImageBehavior = false;
            this.listViewPersonas.View = System.Windows.Forms.View.List;
            // 
            // buttonAgregarPersona
            // 
            this.buttonAgregarPersona.Location = new System.Drawing.Point(686, 411);
            this.buttonAgregarPersona.Name = "buttonAgregarPersona";
            this.buttonAgregarPersona.Size = new System.Drawing.Size(29, 23);
            this.buttonAgregarPersona.TabIndex = 25;
            this.buttonAgregarPersona.Text = "+";
            this.buttonAgregarPersona.UseVisualStyleBackColor = true;
            this.buttonAgregarPersona.Click += new System.EventHandler(this.buttonAgregarPersona_Click);
            // 
            // buttonEliminarPersona
            // 
            this.buttonEliminarPersona.Location = new System.Drawing.Point(721, 411);
            this.buttonEliminarPersona.Name = "buttonEliminarPersona";
            this.buttonEliminarPersona.Size = new System.Drawing.Size(29, 23);
            this.buttonEliminarPersona.TabIndex = 26;
            this.buttonEliminarPersona.Text = "-";
            this.buttonEliminarPersona.UseVisualStyleBackColor = true;
            this.buttonEliminarPersona.Click += new System.EventHandler(this.buttonEliminarPersona_Click);
            // 
            // buttonVerHistorial
            // 
            this.buttonVerHistorial.Location = new System.Drawing.Point(850, 13);
            this.buttonVerHistorial.Name = "buttonVerHistorial";
            this.buttonVerHistorial.Size = new System.Drawing.Size(75, 23);
            this.buttonVerHistorial.TabIndex = 27;
            this.buttonVerHistorial.Text = "Ver historial";
            this.buttonVerHistorial.UseVisualStyleBackColor = true;
            this.buttonVerHistorial.Click += new System.EventHandler(this.buttonVerHistorial_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(686, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Personas:";
            // 
            // labelAvance
            // 
            this.labelAvance.AutoSize = true;
            this.labelAvance.Location = new System.Drawing.Point(258, 94);
            this.labelAvance.Name = "labelAvance";
            this.labelAvance.Size = new System.Drawing.Size(41, 13);
            this.labelAvance.TabIndex = 29;
            this.labelAvance.Text = "label12";
            // 
            // VentanaDetallesTarea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 446);
            this.Controls.Add(this.labelAvance);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.buttonVerHistorial);
            this.Controls.Add(this.buttonEliminarPersona);
            this.Controls.Add(this.buttonAgregarPersona);
            this.Controls.Add(this.listViewPersonas);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxDuracionEstimada);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonEliminarSubtarea);
            this.Controls.Add(this.buttonAgregarSubtarea);
            this.Controls.Add(this.buttonEliminarAntecesora);
            this.Controls.Add(this.treeViewSubtareas);
            this.Controls.Add(this.listViewAntecesoras);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxPrioridad);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dateTimePickerFechaFinalizacion);
            this.Controls.Add(this.dateTimePickerFechaInicio);
            this.Controls.Add(this.textBoxDuracionPendiente);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxDescripcion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxObjetivo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxNombre);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VentanaDetallesTarea";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VentanaDetallesTarea";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VentanaDetallesTarea_FormClosing);
            this.Leave += new System.EventHandler(this.VentanaDetallesTarea_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxObjetivo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDescripcion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDuracionPendiente;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaInicio;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaFinalizacion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPrioridad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.ListView listViewAntecesoras;
        private System.Windows.Forms.TreeView treeViewSubtareas;
        private System.Windows.Forms.ImageList listaImagenes;
        private System.Windows.Forms.Button buttonEliminarAntecesora;
        private System.Windows.Forms.Button buttonAgregarSubtarea;
        private System.Windows.Forms.Button buttonEliminarSubtarea;
        private System.Windows.Forms.TextBox textBoxDuracionEstimada;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView listViewPersonas;
        private System.Windows.Forms.Button buttonAgregarPersona;
        private System.Windows.Forms.Button buttonEliminarPersona;
        private System.Windows.Forms.Button buttonVerHistorial;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelAvance;
    }
}