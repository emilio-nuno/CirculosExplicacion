namespace CirculosExplicacion
{
    partial class Form2
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.pcPrim = new System.Windows.Forms.PictureBox();
            this.pcKruskal = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcKruskal)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(487, 515);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "button1";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // pcPrim
            // 
            this.pcPrim.Location = new System.Drawing.Point(12, 35);
            this.pcPrim.Name = "pcPrim";
            this.pcPrim.Size = new System.Drawing.Size(494, 353);
            this.pcPrim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcPrim.TabIndex = 1;
            this.pcPrim.TabStop = false;
            // 
            // pcKruskal
            // 
            this.pcKruskal.Location = new System.Drawing.Point(570, 35);
            this.pcKruskal.Name = "pcKruskal";
            this.pcKruskal.Size = new System.Drawing.Size(494, 353);
            this.pcKruskal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcKruskal.TabIndex = 2;
            this.pcKruskal.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 550);
            this.Controls.Add(this.pcKruskal);
            this.Controls.Add(this.pcPrim);
            this.Controls.Add(this.btnCerrar);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pcPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcKruskal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.PictureBox pcPrim;
        private System.Windows.Forms.PictureBox pcKruskal;
    }
}