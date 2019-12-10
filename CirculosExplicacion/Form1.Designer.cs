namespace CirculosExplicacion
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.selectedImage = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.botonSelect = new System.Windows.Forms.Button();
            this.botonFindCenter = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.nodosConectados = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.origenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.destinoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botonConectar = new System.Windows.Forms.Button();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnDijkstra = new System.Windows.Forms.Button();
            this.btnProbar = new System.Windows.Forms.Button();
            this.objetivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.selectedImage)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectedImage
            // 
            this.selectedImage.Location = new System.Drawing.Point(12, 11);
            this.selectedImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.selectedImage.Name = "selectedImage";
            this.selectedImage.Size = new System.Drawing.Size(653, 486);
            this.selectedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.selectedImage.TabIndex = 0;
            this.selectedImage.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // botonSelect
            // 
            this.botonSelect.Location = new System.Drawing.Point(705, 11);
            this.botonSelect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.botonSelect.Name = "botonSelect";
            this.botonSelect.Size = new System.Drawing.Size(149, 50);
            this.botonSelect.TabIndex = 1;
            this.botonSelect.Text = "Seleccionar Imagen";
            this.botonSelect.UseVisualStyleBackColor = true;
            this.botonSelect.Click += new System.EventHandler(this.botonSelect_Click);
            // 
            // botonFindCenter
            // 
            this.botonFindCenter.Location = new System.Drawing.Point(705, 65);
            this.botonFindCenter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.botonFindCenter.Name = "botonFindCenter";
            this.botonFindCenter.Size = new System.Drawing.Size(149, 50);
            this.botonFindCenter.TabIndex = 2;
            this.botonFindCenter.Text = "Encontrar Centros";
            this.botonFindCenter.UseVisualStyleBackColor = true;
            this.botonFindCenter.Click += new System.EventHandler(this.botonFindCenter_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(879, 11);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(149, 84);
            this.listBox1.TabIndex = 3;
            // 
            // nodosConectados
            // 
            this.nodosConectados.ContextMenuStrip = this.contextMenuStrip1;
            this.nodosConectados.Location = new System.Drawing.Point(879, 101);
            this.nodosConectados.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nodosConectados.Name = "nodosConectados";
            this.nodosConectados.Size = new System.Drawing.Size(149, 98);
            this.nodosConectados.TabIndex = 4;
            this.nodosConectados.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.nodosConectados_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.origenToolStripMenuItem,
            this.destinoToolStripMenuItem,
            this.objetivoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(211, 104);
            // 
            // origenToolStripMenuItem
            // 
            this.origenToolStripMenuItem.Name = "origenToolStripMenuItem";
            this.origenToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.origenToolStripMenuItem.Text = "Presa";
            this.origenToolStripMenuItem.Click += new System.EventHandler(this.origenToolStripMenuItem_Click);
            // 
            // destinoToolStripMenuItem
            // 
            this.destinoToolStripMenuItem.Name = "destinoToolStripMenuItem";
            this.destinoToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.destinoToolStripMenuItem.Text = "Depredador";
            this.destinoToolStripMenuItem.Click += new System.EventHandler(this.destinoToolStripMenuItem_Click);
            // 
            // botonConectar
            // 
            this.botonConectar.Location = new System.Drawing.Point(705, 121);
            this.botonConectar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.botonConectar.Name = "botonConectar";
            this.botonConectar.Size = new System.Drawing.Size(149, 50);
            this.botonConectar.TabIndex = 5;
            this.botonConectar.Text = "Conectar";
            this.botonConectar.UseVisualStyleBackColor = true;
            this.botonConectar.Click += new System.EventHandler(this.botonConectar_Click);
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(838, 315);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(100, 22);
            this.txtDestino.TabIndex = 12;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(814, 343);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(149, 50);
            this.btnBuscar.TabIndex = 13;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnDijkstra
            // 
            this.btnDijkstra.Location = new System.Drawing.Point(814, 259);
            this.btnDijkstra.Name = "btnDijkstra";
            this.btnDijkstra.Size = new System.Drawing.Size(149, 50);
            this.btnDijkstra.TabIndex = 15;
            this.btnDijkstra.Text = "Dijkstra";
            this.btnDijkstra.UseVisualStyleBackColor = true;
            this.btnDijkstra.Click += new System.EventHandler(this.btnDijkstra_Click);
            // 
            // btnProbar
            // 
            this.btnProbar.Location = new System.Drawing.Point(705, 422);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.Size = new System.Drawing.Size(127, 52);
            this.btnProbar.TabIndex = 16;
            this.btnProbar.Text = "PROBAR";
            this.btnProbar.UseVisualStyleBackColor = true;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // objetivoToolStripMenuItem
            // 
            this.objetivoToolStripMenuItem.Name = "objetivoToolStripMenuItem";
            this.objetivoToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.objetivoToolStripMenuItem.Text = "Objetivo";
            this.objetivoToolStripMenuItem.Click += new System.EventHandler(this.objetivoToolStripMenuItem_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 550);
            this.Controls.Add(this.btnProbar);
            this.Controls.Add(this.btnDijkstra);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.botonConectar);
            this.Controls.Add(this.nodosConectados);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.botonFindCenter);
            this.Controls.Add(this.botonSelect);
            this.Controls.Add(this.selectedImage);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.selectedImage)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox selectedImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button botonSelect;
        private System.Windows.Forms.Button botonFindCenter;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TreeView nodosConectados;
        private System.Windows.Forms.Button botonConectar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem origenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem destinoToolStripMenuItem;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnDijkstra;
        private System.Windows.Forms.Button btnProbar;
        private System.Windows.Forms.ToolStripMenuItem objetivoToolStripMenuItem;
    }
}