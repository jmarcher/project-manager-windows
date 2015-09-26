namespace GUI
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
            this.verDetallesButton = new System.Windows.Forms.Button();
            this.editarButton = new System.Windows.Forms.Button();
            this.eliminarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tareas:";
            // 
            // etapasListView
            // 
            this.etapasListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.etapasListView.Location = new System.Drawing.Point(13, 30);
            this.etapasListView.Name = "etapasListView";
            this.etapasListView.Size = new System.Drawing.Size(512, 276);
            this.etapasListView.TabIndex = 1;
            this.etapasListView.UseCompatibleStateImageBehavior = false;
            this.etapasListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.etapasListView_ColumnClick);
            // 
            // verDetallesButton
            // 
            this.verDetallesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.verDetallesButton.Location = new System.Drawing.Point(450, 313);
            this.verDetallesButton.Name = "verDetallesButton";
            this.verDetallesButton.Size = new System.Drawing.Size(75, 23);
            this.verDetallesButton.TabIndex = 2;
            this.verDetallesButton.Text = "Ver detalles";
            this.verDetallesButton.UseVisualStyleBackColor = true;
            // 
            // editarButton
            // 
            this.editarButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editarButton.Location = new System.Drawing.Point(369, 313);
            this.editarButton.Name = "editarButton";
            this.editarButton.Size = new System.Drawing.Size(75, 23);
            this.editarButton.TabIndex = 3;
            this.editarButton.Text = "Editar";
            this.editarButton.UseVisualStyleBackColor = true;
            // 
            // eliminarButton
            // 
            this.eliminarButton.Location = new System.Drawing.Point(16, 313);
            this.eliminarButton.Name = "eliminarButton";
            this.eliminarButton.Size = new System.Drawing.Size(75, 23);
            this.eliminarButton.TabIndex = 4;
            this.eliminarButton.Text = "Elminar";
            this.eliminarButton.UseVisualStyleBackColor = true;
            this.eliminarButton.Click += new System.EventHandler(this.eliminarButton_Click);
            // 
            // VentanaDetallesProyecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 345);
            this.Controls.Add(this.eliminarButton);
            this.Controls.Add(this.editarButton);
            this.Controls.Add(this.verDetallesButton);
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
        private System.Windows.Forms.Button verDetallesButton;
        private System.Windows.Forms.Button editarButton;
        private System.Windows.Forms.Button eliminarButton;
    }
}