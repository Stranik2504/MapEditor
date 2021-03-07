using System;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor
{
    public partial class Form1 : Form
    {
        #region Create
        public int width;
        public int height;
        private bool active = false;
        public bool fullScreen = false;
        public bool end = false;
        #endregion Create

        #region Designer

        public Form1()
        {
            InitializeComponent();
            TBox1.Text = "1000";
            TBox2.Text = "1000";
            CBox1.CheckState = CheckState.Unchecked;
            CBox2.CheckState = CheckState.Unchecked;
            end = false;
            fullScreen = false;
            active = false;
            width = 0;
            height = 0;
            Size resolution = Screen.PrimaryScreen.Bounds.Size;
            Left = (resolution.Width - Width) / 2;
            Top = (resolution.Height - Height) / 2;
            CBox1.Height = 20;
            CBox1.Left = 20;
            CBox1.Top = 120;
            CBox2.Height = 20;
            CBox2.Left = 20;
            CBox2.Top = CBox1.Top - CBox2.Height - 5;
            B1.Left = 300 - 35 - B1.Width;
            L1.Left = ((300 - 15) - L1.Width - 10 - TBox1.Width) / 2;
            L2.Left = ((300 - 15) - L2.Width - 10 - TBox2.Width) / 2;
            L1.Top = 30;
            L2.Top = L1.Top + 10 + L1.Height;
            TBox1.Left = L1.Left + L1.Width + 10;
            TBox2.Left = L1.Left + L1.Width + 10;
            TBox1.Top = L1.Top;
            TBox2.Top = L2.Top;
        }

        #endregion Designer

        #region Methods

        private void CBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CBox1.CheckState.ToString() == "Unchecked")
            {
                fullScreen = false;
            }
            else
            {
                fullScreen = true;
            }
        }

        private void CBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CBox2.CheckState.ToString() == "Unchecked")
            {
                active = false;
                L1.Enabled = true;
                L2.Enabled = true;
                TBox1.Enabled = true;
                TBox2.Enabled = true;
            }
            else
            {
                active = true;
                L1.Enabled = false;
                L2.Enabled = false;
                TBox1.Enabled = false;
                TBox2.Enabled = false;
            }
        }

        private void B1_Click(object sender, EventArgs e)
        {
            try
            {
                if (active == true)
                {
                    Size resolution = Screen.PrimaryScreen.Bounds.Size;
                    width = resolution.Width;
                    height = resolution.Height;
                }
                else
                {
                    width = Convert.ToInt32(TBox1.Text);
                    height = Convert.ToInt32(TBox2.Text);
                }
                end = true;
                Close();
            }
            catch { MessageBox.Show("Error"); }
        }

        private void Doing(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (active == true)
                    {
                        Size resolution = Screen.PrimaryScreen.Bounds.Size;
                        width = resolution.Width;
                        height = resolution.Height;
                    }
                    else
                    {
                        width = Convert.ToInt32(TBox1.Text);
                        height = Convert.ToInt32(TBox2.Text);
                    }
                    end = true;
                    Close();
                }
                catch { MessageBox.Show("Error"); }
            }

            if (e.KeyCode == Keys.Q)
            {
                if (CBox2.CheckState.ToString() == "Unchecked")
                {
                    active = true;
                    L1.Enabled = false;
                    L2.Enabled = false;
                    TBox1.Enabled = false;
                    TBox2.Enabled = false;
                    CBox2.CheckState = CheckState.Checked;
                }
                else
                {
                    active = false;
                    L1.Enabled = true;
                    L2.Enabled = true;
                    TBox1.Enabled = true;
                    TBox2.Enabled = true;
                    CBox2.CheckState = CheckState.Unchecked;
                }
            }

            if (e.KeyCode == Keys.W)
            {
                if (CBox1.CheckState.ToString() == "Unchecked")
                {
                    CBox1.CheckState = CheckState.Checked;
                    fullScreen = true;
                }
                else
                {
                    CBox1.CheckState = CheckState.Unchecked;
                    fullScreen = false;
                }
            }

            if (e.KeyCode == Keys.Alt)
            {
                if (e.KeyCode == Keys.F4)
                {
                    Close();
                }
            }
        }

        #endregion Methods
    }
}
