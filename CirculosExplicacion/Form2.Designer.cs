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
            this.pcPrim = new System.Windows.Forms.PictureBox();
            this.pcKruskal = new System.Windows.Forms.PictureBox();
            this.lstPrim = new System.Windows.Forms.ListBox();
            this.lstKruskal = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcPrim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcKruskal)).BeginInit();
            this.SuspendLayout();
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
            // lstPrim
            // 
            this.lstPrim.FormattingEnabled = true;
            this.lstPrim.ItemHeight = 16;
            this.lstPrim.Location = new System.Drawing.Point(12, 394);
            this.lstPrim.Name = "lstPrim";
            this.lstPrim.Size = new System.Drawing.Size(494, 148);
            this.lstPrim.TabIndex = 3;
            // 
            // lstKruskal
            // 
            this.lstKruskal.FormattingEnabled = true;
            this.lstKruskal.ItemHeight = 16;
            this.lstKruskal.Location = new System.Drawing.Point(570, 394);
            this.lstKruskal.Name = "lstKruskal";
            this.lstKruskal.Size = new System.Drawing.Size(494, 148);
            this.lstKruskal.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 550);
            this.Controls.Add(this.lstKruskal);
            this.Controls.Add(this.lstPrim);
            this.Controls.Add(this.pcKruskal);
            this.Controls.Add(this.pcPrim);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pcPrim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcKruskal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pcPrim;
        private System.Windows.Forms.PictureBox pcKruskal;
        private System.Windows.Forms.ListBox lstPrim;
        private System.Windows.Forms.ListBox lstKruskal;
    }
}