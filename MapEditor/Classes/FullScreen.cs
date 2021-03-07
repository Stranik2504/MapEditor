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
    class FullScreen
    {
        #region Создание
        private Texture2D message;
        private Vector2 position;
        private KeyboardState keyboard;
        private KeyboardState prevKeyBoard;
        private int Timer = 120;
        private int delay = 0;
        private int width;
        private float numberSec;
        private int stopSec = 20;
        private int needSec = 50;
        private bool activeFullScreen = true;
        public event MyAction NormalScreen;
        #endregion Создание

        #region Реализация

        public void W(int width)
        {
            this.width = width;
        }

        public void Strat(bool activeFullScreen)
        {
            this.activeFullScreen = activeFullScreen;
            if (activeFullScreen == true)
            {
                delay = 0;
            }
            else
            {
                delay = -1;
            }
        }

        public void LoadContnet(GraphicsDevice GraphicsDevice)
        {
            message = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Message7.png"));
            position.Y = 0 - message.Height;
            numberSec = (float)message.Height / (float)(Timer - needSec - stopSec);
        }

        public void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();
            position.X = (width / 2) - (message.Width / 2);
            if (activeFullScreen == true)
            {
                if (CheckKeyBoard(Keys.F11) == true)
                {
                    activeFullScreen = false;
                    NormalScreen();
                    delay = Timer;
                }
                else
                {
                    if (CheckKeyBoard(Keys.Escape) == true)
                    {
                        activeFullScreen = false;
                        NormalScreen();
                        delay = Timer;
                    }
                }
            }
            else
            {
                if (CheckKeyBoard(Keys.F11) == true)
                {
                    NormalScreen();
                    delay = 0;
                    activeFullScreen = true;
                }
            }

            if (delay >= 0)
            {
                if (delay <= Timer)
                {
                    delay++;

                    if (delay >= needSec)
                    {
                        if (delay < Timer - stopSec)
                        {
                            position.Y += numberSec;
                        }
                    }
                }
            }

            if (delay >= Timer)
            {
                delay = -1;
                position.Y = 0 - message.Height;
            }
            prevKeyBoard = keyboard;
        }

        private bool CheckKeyBoard(Keys key)
        {
            return (keyboard.IsKeyDown(key) && !prevKeyBoard.IsKeyDown(key));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if (delay >= 0)
                {
                    if (activeFullScreen == true)
                    {
                        spriteBatch.Draw(message, position, Color.White);
                    }
                }
            }
            catch { }
        }

        #endregion Реализация
    }
}
