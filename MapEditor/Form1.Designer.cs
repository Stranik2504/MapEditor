namespace MapEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CBox2 = new System.Windows.Forms.CheckBox();
            this.B1 = new System.Windows.Forms.Button();
            this.TBox1 = new System.Windows.Forms.TextBox();
            this.TBox2 = new System.Windows.Forms.TextBox();
            this.L1 = new System.Windows.Forms.Label();
            this.L2 = new System.Windows.Forms.Label();
            this.CBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CBox2
            // 
            this.CBox2.AutoSize = true;
            this.CBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.CBox2.Location = new System.Drawing.Point(34, 97);
            this.CBox2.Name = "CBox2";
            this.CBox2.Size = new System.Drawing.Size(69, 17);
            this.CBox2.TabIndex = 0;
            this.CBox2.Text = "Standard";
            this.CBox2.UseVisualStyleBackColor = true;
            this.CBox2.CheckedChanged += new System.EventHandler(this.CBox2_CheckedChanged);
            // 
            // B1
            // 
            this.B1.Location = new System.Drawing.Point(170, 120);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(76, 20);
            this.B1.TabIndex = 1;
            this.B1.Text = "Create";
            this.B1.UseVisualStyleBackColor = true;
            this.B1.Click += new System.EventHandler(this.B1_Click);
            // 
            // TBox1
            // 
            this.TBox1.Location = new System.Drawing.Point(95, 24);
            this.TBox1.Name = "TBox1";
            this.TBox1.Size = new System.Drawing.Size(120, 20);
            this.TBox1.TabIndex = 2;
            this.TBox1.Text = "100";
            // 
            // TBox2
            // 
            this.TBox2.Location = new System.Drawing.Point(95, 55);
            this.TBox2.Name = "TBox2";
            this.TBox2.Size = new System.Drawing.Size(120, 20);
            this.TBox2.TabIndex = 3;
            this.TBox2.Text = "100";
            // 
            // L1
            // 
            this.L1.AutoSize = true;
            this.L1.Location = new System.Drawing.Point(13, 31);
            this.L1.Name = "L1";
            this.L1.Size = new System.Drawing.Size(38, 13);
            this.L1.TabIndex = 4;
            this.L1.Text = "Width:";
            // 
            // L2
            // 
            this.L2.AutoSize = true;
            this.L2.Location = new System.Drawing.Point(13, 55);
            this.L2.Name = "L2";
            this.L2.Size = new System.Drawing.Size(41, 13);
            this.L2.TabIndex = 5;
            this.L2.Text = "Height:";
            // 
            // CBox1
            // 
            this.CBox1.AutoSize = true;
            this.CBox1.Location = new System.Drawing.Point(34, 123);
            this.CBox1.Name = "CBox1";
            this.CBox1.Size = new System.Drawing.Size(98, 17);
            this.CBox1.TabIndex = 6;
            this.CBox1.Text = "StartFullScreen";
            this.CBox1.UseVisualStyleBackColor = true;
            this.CBox1.CheckedChanged += new System.EventHandler(this.CBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.CBox1);
            this.Controls.Add(this.L2);
            this.Controls.Add(this.L1);
            this.Controls.Add(this.TBox2);
            this.Controls.Add(this.TBox1);
            this.Controls.Add(this.B1);
            this.Controls.Add(this.CBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "EditMap";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Doing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button B1;
        private System.Windows.Forms.TextBox TBox1;
        private System.Windows.Forms.TextBox TBox2;
        private System.Windows.Forms.Label L1;
        private System.Windows.Forms.Label L2;
        private System.Windows.Forms.CheckBox CBox1;
        private System.Windows.Forms.CheckBox CBox2;
    }
}