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
            this.botonOrdenar = new System.Windows.Forms.Button();
            this.listaAntes = new System.Windows.Forms.ListBox();
            this.listaDespues = new System.Windows.Forms.ListBox();
            this.botonPrim = new System.Windows.Forms.Button();
            this.botonKruskal = new System.Windows.Forms.Button();
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
            this.listBox1.Location = new System.Drawing.Point(880, 31);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(149, 84);
            this.listBox1.TabIndex = 3;
            // 
            // nodosConectados
            // 
            this.nodosConectados.ContextMenuStrip = this.contextMenuStrip1;
            this.nodosConectados.Location = new System.Drawing.Point(880, 121);
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
            this.destinoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 52);
            // 
            // origenToolStripMenuItem
            // 
            this.origenToolStripMenuItem.Name = "origenToolStripMenuItem";
            this.origenToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
            this.origenToolStripMenuItem.Text = "Origen";
            this.origenToolStripMenuItem.Click += new System.EventHandler(this.origenToolStripMenuItem_Click);
            // 
            // destinoToolStripMenuItem
            // 
            this.destinoToolStripMenuItem.Name = "destinoToolStripMenuItem";
            this.destinoToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
            this.destinoToolStripMenuItem.Text = "Destino";
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
            // botonOrdenar
            // 
            this.botonOrdenar.Location = new System.Drawing.Point(705, 177);
            this.botonOrdenar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.botonOrdenar.Name = "botonOrdenar";
            this.botonOrdenar.Size = new System.Drawing.Size(149, 50);
            this.botonOrdenar.TabIndex = 6;
            this.botonOrdenar.Text = "Ordenar Vértices";
            this.botonOrdenar.UseVisualStyleBackColor = true;
            this.botonOrdenar.Click += new System.EventHandler(this.botonOrdenar_Click);
            // 
            // listaAntes
            // 
            this.listaAntes.FormattingEnabled = true;
            this.listaAntes.ItemHeight = 16;
            this.listaAntes.Location = new System.Drawing.Point(704, 332);
            this.listaAntes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listaAntes.Name = "listaAntes";
            this.listaAntes.Size = new System.Drawing.Size(149, 84);
            this.listaAntes.TabIndex = 7;
            // 
            // listaDespues
            // 
            this.listaDespues.FormattingEnabled = true;
            this.listaDespues.ItemHeight = 16;
            this.listaDespues.Location = new System.Drawing.Point(879, 332);
            this.listaDespues.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listaDespues.Name = "listaDespues";
            this.listaDespues.Size = new System.Drawing.Size(149, 84);
            this.listaDespues.TabIndex = 8;
            // 
            // botonPrim
            // 
            this.botonPrim.Location = new System.Drawing.Point(705, 232);
            this.botonPrim.Name = "botonPrim";
            this.botonPrim.Size = new System.Drawing.Size(149, 50);
            this.botonPrim.TabIndex = 9;
            this.botonPrim.Text = "Prim";
            this.botonPrim.UseVisualStyleBackColor = true;
            this.botonPrim.Click += new System.EventHandler(this.botonPrim_Click);
            // 
            // botonKruskal
            // 
            this.botonKruskal.Location = new System.Drawing.Point(879, 232);
            this.botonKruskal.Name = "botonKruskal";
            this.botonKruskal.Size = new System.Drawing.Size(149, 50);
            this.botonKruskal.TabIndex = 10;
            this.botonKruskal.Text = "Kruskal";
            this.botonKruskal.UseVisualStyleBackColor = true;
            this.botonKruskal.Click += new System.EventHandler(this.botonKruskal_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 550);
            this.Controls.Add(this.botonKruskal);
            this.Controls.Add(this.botonPrim);
            this.Controls.Add(this.listaDespues);
            this.Controls.Add(this.listaAntes);
            this.Controls.Add(this.botonOrdenar);
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

        }

        #endregion

        private System.Windows.Forms.PictureBox selectedImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button botonSelect;
        private System.Windows.Forms.Button botonFindCenter;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TreeView nodosConectados;
        private System.Windows.Forms.Button botonConectar;
        private System.Windows.Forms.Button botonOrdenar;
        private System.Windows.Forms.ListBox listaAntes;
        private System.Windows.Forms.ListBox listaDespues;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem origenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem destinoToolStripMenuItem;
        private System.Windows.Forms.Button botonPrim;
        private System.Windows.Forms.Button botonKruskal;
    }
}
