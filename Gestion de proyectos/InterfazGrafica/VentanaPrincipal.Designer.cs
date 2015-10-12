namespace InterfazGrafica
{
    partial class VentanaPrincipal
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
            this.listViewProyectos = new System.Windows.Forms.ListView();
            this.buttonAgregarNuevoProyecto = new System.Windows.Forms.Button();
            this.barraMenu = new System.Windows.Forms.MenuStrip();
            this.proyectoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crearNuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leyendaDeColoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.barraMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewProyectos
            // 
            this.listViewProyectos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewProyectos.Location = new System.Drawing.Point(9, 27);
            this.listViewProyectos.Margin = new System.Windows.Forms.Padding(2);
            this.listViewProyectos.MultiSelect = false;
            this.listViewProyectos.Name = "listViewProyectos";
            this.listViewProyectos.Size = new System.Drawing.Size(659, 361);
            this.listViewProyectos.TabIndex = 0;
            this.listViewProyectos.UseCompatibleStateImageBehavior = false;
            this.listViewProyectos.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewProyectos_ColumnClick);
            this.listViewProyectos.DoubleClick += new System.EventHandler(this.listViewProyectos_DoubleClick);
            // 
            // buttonAgregarNuevoProyecto
            // 
            this.buttonAgregarNuevoProyecto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAgregarNuevoProyecto.Location = new System.Drawing.Point(564, 392);
            this.buttonAgregarNuevoProyecto.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAgregarNuevoProyecto.Name = "buttonAgregarNuevoProyecto";
            this.buttonAgregarNuevoProyecto.Size = new System.Drawing.Size(104, 24);
            this.buttonAgregarNuevoProyecto.TabIndex = 5;
            this.buttonAgregarNuevoProyecto.Text = "Crear proyecto";
            this.buttonAgregarNuevoProyecto.UseVisualStyleBackColor = true;
            this.buttonAgregarNuevoProyecto.Click += new System.EventHandler(this.buttonAgregarNuevoProyecto_Click);
            // 
            // barraMenu
            // 
            this.barraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.proyectoToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.barraMenu.Location = new System.Drawing.Point(0, 0);
            this.barraMenu.Name = "barraMenu";
            this.barraMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.barraMenu.Size = new System.Drawing.Size(679, 24);
            this.barraMenu.TabIndex = 6;
            this.barraMenu.Text = "menuStrip1";
            // 
            // proyectoToolStripMenuItem
            // 
            this.proyectoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.crearNuevoToolStripMenuItem});
            this.proyectoToolStripMenuItem.Name = "proyectoToolStripMenuItem";
            this.proyectoToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.proyectoToolStripMenuItem.Text = "Proyecto";
            // 
            // crearNuevoToolStripMenuItem
            // 
            this.crearNuevoToolStripMenuItem.Name = "crearNuevoToolStripMenuItem";
            this.crearNuevoToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.crearNuevoToolStripMenuItem.Text = "Crear nuevo";
            this.crearNuevoToolStripMenuItem.Click += new System.EventHandler(this.crearNuevoToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem,
            this.leyendaDeColoresToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            // 
            // leyendaDeColoresToolStripMenuItem
            // 
            this.leyendaDeColoresToolStripMenuItem.Name = "leyendaDeColoresToolStripMenuItem";
            this.leyendaDeColoresToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.leyendaDeColoresToolStripMenuItem.Text = "Leyenda de colores";
            this.leyendaDeColoresToolStripMenuItem.Click += new System.EventHandler(this.leyendaDeColoresToolStripMenuItem_Click);
            // 
            // VentanaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 423);
            this.Controls.Add(this.buttonAgregarNuevoProyecto);
            this.Controls.Add(this.listViewProyectos);
            this.Controls.Add(this.barraMenu);
            this.MainMenuStrip = this.barraMenu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VentanaPrincipal";
            this.Text = "Ventana Principal";
            this.barraMenu.ResumeLayout(false);
            this.barraMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewProyectos;
        private System.Windows.Forms.Button buttonAgregarNuevoProyecto;
        private System.Windows.Forms.MenuStrip barraMenu;
        private System.Windows.Forms.ToolStripMenuItem proyectoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crearNuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leyendaDeColoresToolStripMenuItem;
    }
}