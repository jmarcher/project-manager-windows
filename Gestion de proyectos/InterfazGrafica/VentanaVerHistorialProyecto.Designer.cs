namespace InterfazGrafica
{
    partial class VentanaVerHistorialProyecto
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
            this.textBoxHistorialProyecto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxHistorialProyecto
            // 
            this.textBoxHistorialProyecto.Location = new System.Drawing.Point(13, 13);
            this.textBoxHistorialProyecto.Multiline = true;
            this.textBoxHistorialProyecto.Name = "textBoxHistorialProyecto";
            this.textBoxHistorialProyecto.ReadOnly = true;
            this.textBoxHistorialProyecto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHistorialProyecto.Size = new System.Drawing.Size(558, 266);
            this.textBoxHistorialProyecto.TabIndex = 0;
            // 
            // VentanaVerHistorialProyecto
            // 
            this.ClientSize = new System.Drawing.Size(583, 291);
            this.Controls.Add(this.textBoxHistorialProyecto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VentanaVerHistorialProyecto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Historial proyecto";
            this.Load += new System.EventHandler(this.VentanaVerHistorialTarea_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxHistorial;
        private System.Windows.Forms.TextBox textBoxHistorialProyecto;
    }
}