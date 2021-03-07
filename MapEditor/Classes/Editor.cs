using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;
using System.IO;

namespace MapEditor.Classes
{
    public delegate void FilePathGet(string filePath, GraphicsDevice GraphicsDevice);
    class Editor
    {
        #region Create
        private ClassMonogame.Lable message;
        private int delay = -1;
        private int width;
        private int height;
        private string filePath;
        private bool complete;
        public event FilePathGet filePathGet;
        #endregion Create

        #region Methods

        public void LoadContent(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("selected1");
            SpriteFont spriteFont = content.Load<SpriteFont>("font");
            message = new ClassMonogame.Lable(spriteFont, 100, 20, (width - 100) / 2, 0);
            message.GetTexture(texture);
            message.Visible = true;
            message.GetColor(Color.White);
        }

        public void CreateComponent()
        {
            message.NewPosition((width - 100) / 2, 0 - message.Height);
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpg;*.jpeg;*.jpe;*.jfif)|*.jpg;*.jpeg;*.jpe;*.jfif|GIF (*.gif)|*.gif| All files (*.png;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif)|*.png;*.jpg;*.jpeg;*.jpe;*.jfif;*.gif";
                openFileDialog.FilterIndex = 4;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        filePath = openFileDialog.FileName;
                        if (Directory.Exists(@"Textures") == false) { Directory.CreateDirectory(@"Textures"); }
                        if (Directory.Exists(@"Save") == false) { Directory.CreateDirectory(@"Save"); }
                        string name = filePath;
                        while (name.IndexOf(@"\") > -1)
                        {
                            name = name.Remove(0, name.IndexOf(@"\") + 1);
                        }
                        name = @"Textures\" + name;
                        if (File.Exists(name) == true) { File.Copy(filePath, name, true); } else { File.Copy(filePath, name, false); }
                        filePath = name;
                        complete = true;
                        message.GetText("Complete");
                    }
                    catch { message.GetText("  Error "); }
                }
                else { message.GetText("  Error "); }
            }
            catch { message.GetText("  Error "); }
            delay++;
        }

        public void GetWH(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Update(GameTime gameTime, GraphicsDevice GraphicsDevice)
        {
            message.UpdateBoundingBox(gameTime);
            if (delay >= 0)
            {
                if (delay <= 150)
                {
                    delay++;
                    if (delay <= 110)
                    {
                        if (delay >= 20)
                        {
                            message.GetPosition(message.X, message.Y + (float)message.Height / (float)(90));
                        }
                    }
                }
            }

            if (delay >= 150)
            {
                delay = -1;
            }

            if (complete == true)
            {
                complete = false;
                filePathGet(filePath, GraphicsDevice);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (delay >= 0)
            {
                message.Draw(spriteBatch);
            }
        }

        #endregion Methods
    }
}
