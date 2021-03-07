using System;
using System.Windows.Forms;

namespace MapEditor
{
#if WINDOWS || LINUX
    public static class Program
    {
        #region Create
        static bool end = true;
        static bool Event = false;
        public static Game1 game;
        public static Form1 Form1;
        public static Form2 Form2;
        #endregion Create

        [STAThread]

        #region Methods

        static void Main()
        {
            while (end == true)
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                }
                catch { }
                Form1 = new Form1();
                Form2 = new Form2();
                try
                {
                    Application.Run(Form1);
                }
                catch { }

                if (Form1.end == true)
                {
                    game = new Game1(Form1.width, Form1.height, Form1.fullScreen);
                    if (Event == false) { Event = true; game.editForm2 += Start; Form2.Action += game.EditField; }
                    try
                    {
                        game.Run();
                    }
                    catch { }
                    end = game.end;
                }
                else
                {
                    end = false;
                }
            }
        }

        private static void Start()
        {
            System.Threading.Thread thread = new System.Threading.Thread(EditForm2);
            try { thread.Start(); } catch {  }
        }

        private static void EditForm2()
        {
            Form2 = new Form2();
            Form2.Action += game.EditField;
            try
            {
                Application.Run(Form2);

            }
            catch { MessageBox.Show("Error"); }
        }

        #endregion Methods
    }
#endif
}
