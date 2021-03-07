using System;
using System.Windows.Forms;

namespace MapEditor
{
    public delegate void FieldWidth(int widthCell);

    public partial class Form2 : Form
    {
        #region Create
        private int widthCell;
        private bool active = false;
        public event FieldWidth Action;
        #endregion Create

        #region Designer

        public Form2()
        {
            InitializeComponent();
            B1.Width = Width - 45 - CBox1.Width;
            TBox1.Width = Width - L1.Width - 45;
            B1.Left = 20 + CBox1.Width;
            CBox1.Left = 10;
            L1.Left = 10;
            TBox1.Left = 20 + L1.Width;
            TBox1.Top = 50;
            L1.Top = TBox1.Top;
            CBox1.Top = 110;
            B1.Top = CBox1.Top;
            TBox1.Text = "20";
        }

        #endregion Designer

        #region Methods

        private void CBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CBox1.CheckState.ToString() == "Unchecked")
            {
                active = false;
                L1.Enabled = true;
                TBox1.Enabled = true;
            }
            else
            {
                active = true;
                L1.Enabled = false;
                TBox1.Enabled = false;
            }
        }

        private void B1_Click(object sender, EventArgs e)
        {
            try
            {
                if (active == true)
                {
                    widthCell = 20;
                }
                else
                {
                    widthCell = Convert.ToInt32(TBox1.Text);
                }
                Action(widthCell);
                Close();
            }
            catch { MessageBox.Show("Error"); }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (active == true)
                    {
                        widthCell = 20;
                    }
                    else
                    {
                        widthCell = Convert.ToInt32(TBox1.Text);
                    }
                    Action(widthCell);
                    Close();
                }
                catch { MessageBox.Show("Error"); }
            }

            if (e.KeyCode == Keys.Q)
            {
                if (CBox1.CheckState.ToString() == "Unchecked")
                {
                    active = false;
                    L1.Enabled = true;
                    TBox1.Enabled = true;
                }
                else
                {
                    active = true;
                    L1.Enabled = false;
                    TBox1.Enabled = false;
                }
            }
        }

        #endregion Methods
    }
}
