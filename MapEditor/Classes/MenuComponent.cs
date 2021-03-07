using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization; //XML

namespace MapEditor.Classes
{
    delegate void Go(GameTime gameTime, MenuComponent menu);

    class MenuComponent
    {
        #region Создание
        private Texture2D texture;
        private Rectangle boundingBox;
        private Rectangle textureBoundingBox;
        private Rectangle mouseBoundingBox;
        private MouseState mouse;
        private int heightNumber;
        private int number;
        private int width;
        private int height;
        private int indent;
        private int indentHeight;
        private int widthTexture;   //Ширин полоски
        private string way;
        private bool go;    // Когда нажали на элемент в menu
        private int timer = 60;
        private int delay = 60;
        public event Go onGo;
        #endregion Создание

        #region Публичные поля
        public string Way { get { return way; } }
        public bool Go { get { return go; } set { go = value; } }
        #endregion Публичные поля

        #region Реализация

        public MenuComponent(string way, int number, int widthTexture, int indentHeight, int width, int height, int indent, int hightNumber)
        {
            this.way = way;
            this.heightNumber = hightNumber;
            this.number = number;
            this.indentHeight = indentHeight;
            this.width = width;
            this.height = height;
            this.indent = indent;
            this.widthTexture = widthTexture;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            try
            {
                texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@way));
            }
            catch { }
        }

        public void Update(GameTime gameTime, MenuComponent menu)
        {
            mouse = Mouse.GetState();
            mouseBoundingBox = new Rectangle((int)mouse.Position.X, (int)mouse.Position.Y, 0, 0);
            boundingBox = new Rectangle(width - widthTexture, indentHeight + heightNumber * number, widthTexture, heightNumber);
            textureBoundingBox = new Rectangle(boundingBox.X, boundingBox.Y, heightNumber, heightNumber);

            if (mouseBoundingBox.Intersects(boundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (delay >= timer)
                    {
                        delay = 0;
                        onGo(gameTime, menu);
                    }
                }
            }

            if (delay <= timer)
            {
                delay++;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.Draw(texture, textureBoundingBox, Color.White);
            }
            catch { }
        }

        #endregion Реализация
    }
}
