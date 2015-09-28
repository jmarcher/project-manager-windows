namespace GUI
{
    partial class VentanaEtapa
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
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nombreTextBox = new System.Windows.Forms.TextBox();
            this.marcarFinalizadaCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fechaFinalizacionTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.agregarTareaButton = new System.Windows.Forms.Button();
            this.quitarTareaButton = new System.Windows.Forms.Button();
            this.tareasAgregadasListBox = new System.Windows.Forms.ListBox();
            this.guardarButton = new System.Windows.Forms.Button();
            this.fechaInicioTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.duracionPendienteDomainUpDown = new System.Windows.Forms.DomainUpDown();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id:";
            // 
            // idTextBox
            // 
            this.idTextBox.Enabled = false;
            this.idTextBox.Location = new System.Drawing.Point(12, 26);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(100, 20);
            this.idTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre:";
            // 
            // nombreTextBox
            // 
            this.nombreTextBox.Location = new System.Drawing.Point(12, 69);
            this.nombreTextBox.Name = "nombreTextBox";
            this.nombreTextBox.Size = new System.Drawing.Size(100, 20);
            this.nombreTextBox.TabIndex = 3;
            // 
            // marcarFinalizadaCheckBox
            // 
            this.marcarFinalizadaCheckBox.AutoSize = true;
            this.marcarFinalizadaCheckBox.Location = new System.Drawing.Point(12, 153);
            this.marcarFinalizadaCheckBox.Name = "marcarFinalizadaCheckBox";
            this.marcarFinalizadaCheckBox.Size = new System.Drawing.Size(165, 17);
            this.marcarFinalizadaCheckBox.TabIndex = 4;
            this.marcarFinalizadaCheckBox.Text = "Marcar etapa como finalizada";
            this.marcarFinalizadaCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Fecha de finalización:";
            // 
            // fechaFinalizacionTextBox
            // 
            this.fechaFinalizacionTextBox.Enabled = false;
            this.fechaFinalizacionTextBox.Location = new System.Drawing.Point(12, 242);
            this.fechaFinalizacionTextBox.Name = "fechaFinalizacionTextBox";
            this.fechaFinalizacionTextBox.Size = new System.Drawing.Size(100, 20);
            this.fechaFinalizacionTextBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tareas:";
            // 
            // agregarTareaButton
            // 
            this.agregarTareaButton.Location = new System.Drawing.Point(348, 295);
            this.agregarTareaButton.Name = "agregarTareaButton";
            this.agregarTareaButton.Size = new System.Drawing.Size(30, 23);
            this.agregarTareaButton.TabIndex = 9;
            this.agregarTareaButton.Text = "+";
            this.agregarTareaButton.UseVisualStyleBackColor = true;
            // 
            // quitarTareaButton
            // 
            this.quitarTareaButton.Location = new System.Drawing.Point(348, 325);
            this.quitarTareaButton.Name = "quitarTareaButton";
            this.quitarTareaButton.Size = new System.Drawing.Size(30, 23);
            this.quitarTareaButton.TabIndex = 10;
            this.quitarTareaButton.Text = "-";
            this.quitarTareaButton.UseVisualStyleBackColor = true;
            // 
            // tareasAgregadasListBox
            // 
            this.tareasAgregadasListBox.FormattingEnabled = true;
            this.tareasAgregadasListBox.Location = new System.Drawing.Point(12, 295);
            this.tareasAgregadasListBox.Name = "tareasAgregadasListBox";
            this.tareasAgregadasListBox.Size = new System.Drawing.Size(330, 160);
            this.tareasAgregadasListBox.TabIndex = 11;
            // 
            // guardarButton
            // 
            this.guardarButton.Location = new System.Drawing.Point(303, 462);
            this.guardarButton.Name = "guardarButton";
            this.guardarButton.Size = new System.Drawing.Size(75, 23);
            this.guardarButton.TabIndex = 12;
            this.guardarButton.Text = "Guardar";
            this.guardarButton.UseVisualStyleBackColor = true;
            this.guardarButton.Click += new System.EventHandler(this.guardarButton_Click);
            // 
            // fechaInicioTextBox
            // 
            this.fechaInicioTextBox.Enabled = false;
            this.fechaInicioTextBox.Location = new System.Drawing.Point(12, 198);
            this.fechaInicioTextBox.Name = "fechaInicioTextBox";
            this.fechaInicioTextBox.Size = new System.Drawing.Size(100, 20);
            this.fechaInicioTextBox.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Fecha de inicio:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Duracion pendiente:";
            // 
            // duracionPendienteDomainUpDown
            // 
            this.duracionPendienteDomainUpDown.Location = new System.Drawing.Point(12, 113);
            this.duracionPendienteDomainUpDown.Name = "duracionPendienteDomainUpDown";
            this.duracionPendienteDomainUpDown.Size = new System.Drawing.Size(120, 20);
            this.duracionPendienteDomainUpDown.TabIndex = 17;
            // 
            // VentanaEtapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 514);
            this.Controls.Add(this.duracionPendienteDomainUpDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.fechaInicioTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.guardarButton);
            this.Controls.Add(this.tareasAgregadasListBox);
            this.Controls.Add(this.quitarTareaButton);
            this.Controls.Add(this.agregarTareaButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fechaFinalizacionTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.marcarFinalizadaCheckBox);
            this.Controls.Add(this.nombreTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.label1);
            this.Name = "VentanaEtapa";
            this.Text = "VentanaEtapa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VentanaEtapa_FormClosing);
            this.Load += new System.EventHandler(this.VentanaEtapa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nombreTextBox;
        private System.Windows.Forms.CheckBox marcarFinalizadaCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fechaFinalizacionTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button agregarTareaButton;
        private System.Windows.Forms.Button quitarTareaButton;
        private System.Windows.Forms.ListBox tareasAgregadasListBox;
        private System.Windows.Forms.Button guardarButton;
        private System.Windows.Forms.TextBox fechaInicioTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DomainUpDown duracionPendienteDomainUpDown;
    }
}