namespace MapEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.TBox1 = new System.Windows.Forms.TextBox();
            this.L1 = new System.Windows.Forms.Label();
            this.CBox1 = new System.Windows.Forms.CheckBox();
            this.B1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TBox1
            // 
            this.TBox1.Location = new System.Drawing.Point(116, 40);
            this.TBox1.Name = "TBox1";
            this.TBox1.Size = new System.Drawing.Size(100, 20);
            this.TBox1.TabIndex = 0;
            // 
            // L1
            // 
            this.L1.AutoSize = true;
            this.L1.Location = new System.Drawing.Point(22, 40);
            this.L1.Name = "L1";
            this.L1.Size = new System.Drawing.Size(58, 13);
            this.L1.TabIndex = 1;
            this.L1.Text = "Width Cell:";
            // 
            // CBox1
            // 
            this.CBox1.AutoSize = true;
            this.CBox1.Location = new System.Drawing.Point(25, 113);
            this.CBox1.Name = "CBox1";
            this.CBox1.Size = new System.Drawing.Size(69, 17);
            this.CBox1.TabIndex = 2;
            this.CBox1.Text = "Standard";
            this.CBox1.UseVisualStyleBackColor = true;
            this.CBox1.CheckedChanged += new System.EventHandler(this.CBox1_CheckedChanged);
            // 
            // B1
            // 
            this.B1.Location = new System.Drawing.Point(131, 113);
            this.B1.Name = "B1";
            this.B1.Size = new System.Drawing.Size(75, 23);
            this.B1.TabIndex = 3;
            this.B1.Text = "Create";
            this.B1.UseVisualStyleBackColor = true;
            this.B1.Click += new System.EventHandler(this.B1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 161);
            this.Controls.Add(this.B1);
            this.Controls.Add(this.CBox1);
            this.Controls.Add(this.L1);
            this.Controls.Add(this.TBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form2";
            this.Text = "Form2";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBox1;
        private System.Windows.Forms.Label L1;
        private System.Windows.Forms.CheckBox CBox1;
        private System.Windows.Forms.Button B1;
    }
}