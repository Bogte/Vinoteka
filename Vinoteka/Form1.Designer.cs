namespace Vinoteka
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.artikliToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lagerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.artikliToolStripMenuItem,
            this.lagerToolStripMenuItem,
            this.lagerToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // artikliToolStripMenuItem
            // 
            this.artikliToolStripMenuItem.Name = "artikliToolStripMenuItem";
            this.artikliToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.artikliToolStripMenuItem.Text = "Artikli";
            this.artikliToolStripMenuItem.Click += new System.EventHandler(this.artikliToolStripMenuItem_Click);
            // 
            // lagerToolStripMenuItem
            // 
            this.lagerToolStripMenuItem.Name = "lagerToolStripMenuItem";
            this.lagerToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.lagerToolStripMenuItem.Text = "Musterija";
            this.lagerToolStripMenuItem.Click += new System.EventHandler(this.lagerToolStripMenuItem_Click);
            // 
            // lagerToolStripMenuItem1
            // 
            this.lagerToolStripMenuItem1.Name = "lagerToolStripMenuItem1";
            this.lagerToolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.lagerToolStripMenuItem1.Text = "Lager";
            this.lagerToolStripMenuItem1.Click += new System.EventHandler(this.lagerToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 213);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem artikliToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lagerToolStripMenuItem1;
    }
}

