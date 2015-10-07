namespace InterfazGrafica
{
    partial class VentanaDetallesProyecto
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
            this.label1 = new System.Windows.Forms.Label();
            this.etapasListView = new System.Windows.Forms.ListView();
            this.eliminarButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelIdentificacion = new System.Windows.Forms.Label();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxObjetivo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFFin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DuracionPendiente = new System.Windows.Forms.Label();
            this.labelDuracionPendiente = new System.Windows.Forms.Label();
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.buttonAgregar = new System.Windows.Forms.Button();
            this.dateTimePickerFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Etapas:";
            // 
            // etapasListView
            // 
            this.etapasListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.etapasListView.Location = new System.Drawing.Point(13, 183);
            this.etapasListView.Name = "etapasListView";
            this.etapasListView.Size = new System.Drawing.Size(512, 245);
            this.etapasListView.TabIndex = 1;
            this.etapasListView.UseCompatibleStateImageBehavior = false;
            this.etapasListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.etapasListView_ColumnClick);
            this.etapasListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.etapasListView_MouseDoubleClick);
            // 
            // eliminarButton
            // 
            this.eliminarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.eliminarButton.Location = new System.Drawing.Point(12, 432);
            this.eliminarButton.Name = "eliminarButton";
            this.eliminarButton.Size = new System.Drawing.Size(104, 23);
            this.eliminarButton.TabIndex = 4;
            this.eliminarButton.Text = "Elminar etapa";
            this.eliminarButton.UseVisualStyleBackColor = true;
            this.eliminarButton.Click += new System.EventHandler(this.eliminarButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Identificación: ";
            // 
            // labelIdentificacion
            // 
            this.labelIdentificacion.AutoSize = true;
            this.labelIdentificacion.Location = new System.Drawing.Point(121, 13);
            this.labelIdentificacion.Name = "labelIdentificacion";
            this.labelIdentificacion.Size = new System.Drawing.Size(92, 13);
            this.labelIdentificacion.TabIndex = 6;
            this.labelIdentificacion.Text = "labelIdentificacion";
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(124, 30);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.Size = new System.Drawing.Size(214, 20);
            this.textBoxNombre.TabIndex = 7;
            this.textBoxNombre.TextChanged += new System.EventHandler(this.textBoxNombre_TextChanged);
            // 
            // textBoxObjetivo
            // 
            this.textBoxObjetivo.Location = new System.Drawing.Point(124, 56);
            this.textBoxObjetivo.Name = "textBoxObjetivo";
            this.textBoxObjetivo.Size = new System.Drawing.Size(214, 20);
            this.textBoxObjetivo.TabIndex = 8;
            this.textBoxObjetivo.TextChanged += new System.EventHandler(this.textBoxObjetivo_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Nombre:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Objetivo:";
            // 
            // textBoxFFin
            // 
            this.textBoxFFin.Location = new System.Drawing.Point(124, 108);
            this.textBoxFFin.Name = "textBoxFFin";
            this.textBoxFFin.ReadOnly = true;
            this.textBoxFFin.Size = new System.Drawing.Size(214, 20);
            this.textBoxFFin.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Fecha inicio:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Fecha finalizacion:";
            // 
            // DuracionPendiente
            // 
            this.DuracionPendiente.AutoSize = true;
            this.DuracionPendiente.Location = new System.Drawing.Point(13, 137);
            this.DuracionPendiente.Name = "DuracionPendiente";
            this.DuracionPendiente.Size = new System.Drawing.Size(103, 13);
            this.DuracionPendiente.TabIndex = 15;
            this.DuracionPendiente.Text = "Duracion pendiente:";
            // 
            // labelDuracionPendiente
            // 
            this.labelDuracionPendiente.AutoSize = true;
            this.labelDuracionPendiente.Location = new System.Drawing.Point(121, 137);
            this.labelDuracionPendiente.Name = "labelDuracionPendiente";
            this.labelDuracionPendiente.Size = new System.Drawing.Size(49, 13);
            this.labelDuracionPendiente.TabIndex = 16;
            this.labelDuracionPendiente.Text = "Objetivo:";
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGuardar.Enabled = false;
            this.buttonGuardar.Location = new System.Drawing.Point(450, 432);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(75, 23);
            this.buttonGuardar.TabIndex = 17;
            this.buttonGuardar.Text = "&Guardar";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // buttonAgregar
            // 
            this.buttonAgregar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAgregar.Location = new System.Drawing.Point(124, 432);
            this.buttonAgregar.Name = "buttonAgregar";
            this.buttonAgregar.Size = new System.Drawing.Size(104, 23);
            this.buttonAgregar.TabIndex = 18;
            this.buttonAgregar.Text = "Agregar etapa";
            this.buttonAgregar.UseVisualStyleBackColor = true;
            this.buttonAgregar.Click += new System.EventHandler(this.buttonAgregar_Click);
            // 
            // dateTimePickerFechaInicio
            // 
            this.dateTimePickerFechaInicio.Location = new System.Drawing.Point(124, 83);
            this.dateTimePickerFechaInicio.Name = "dateTimePickerFechaInicio";
            this.dateTimePickerFechaInicio.Size = new System.Drawing.Size(214, 20);
            this.dateTimePickerFechaInicio.TabIndex = 19;
            this.dateTimePickerFechaInicio.ValueChanged += new System.EventHandler(this.dateTimePickerFechaInicio_ValueChanged);
            // 
            // VentanaDetallesProyecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 467);
            this.Controls.Add(this.dateTimePickerFechaInicio);
            this.Controls.Add(this.buttonAgregar);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.labelDuracionPendiente);
            this.Controls.Add(this.DuracionPendiente);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxFFin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxObjetivo);
            this.Controls.Add(this.textBoxNombre);
            this.Controls.Add(this.labelIdentificacion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.eliminarButton);
            this.Controls.Add(this.etapasListView);
            this.Controls.Add(this.label1);
            this.Name = "VentanaDetallesProyecto";
            this.Text = "VentanaDetallesProyecto";
            this.Load += new System.EventHandler(this.VentanaDetallesProyecto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView etapasListView;
        private System.Windows.Forms.Button eliminarButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelIdentificacion;
        private System.Windows.Forms.TextBox textBoxNombre;
        private System.Windows.Forms.TextBox textBoxObjetivo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFFin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label DuracionPendiente;
        private System.Windows.Forms.Label labelDuracionPendiente;
        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.Button buttonAgregar;
        private System.Windows.Forms.DateTimePicker dateTimePickerFechaInicio;
    }
}