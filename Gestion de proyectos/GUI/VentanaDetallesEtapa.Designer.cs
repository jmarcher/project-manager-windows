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
            this.SuspendLayout();
            // 
            // arbolDeTareas
            // 
            this.arbolDeTareas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arbolDeTareas.ImageIndex = 0;
            this.arbolDeTareas.ImageList = this.listaImagenes;
            this.arbolDeTareas.Location = new System.Drawing.Point(13, 13);
            this.arbolDeTareas.Name = "arbolDeTareas";
            this.arbolDeTareas.SelectedImageIndex = 0;
            this.arbolDeTareas.Size = new System.Drawing.Size(688, 295);
            this.arbolDeTareas.TabIndex = 0;
            // 
            // listaImagenes
            // 
            this.listaImagenes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listaImagenes.ImageStream")));
            this.listaImagenes.TransparentColor = System.Drawing.Color.Transparent;
            this.listaImagenes.Images.SetKeyName(0, "TareaCompuesta");
            this.listaImagenes.Images.SetKeyName(1, "TareaSimple");
            // 
            // VentanaDetallesEtapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 320);
            this.Controls.Add(this.arbolDeTareas);
            this.Name = "VentanaDetallesEtapa";
            this.Text = "VentanaTarea";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView arbolDeTareas;
        private System.Windows.Forms.ImageList listaImagenes;
    }
}