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
    delegate void MyAction();

    class Edit
    {
        #region Создание
        private Texture2D message;
        private Texture2D b1;
        private Texture2D b2;
        private Texture2D errorMessage;
        private Texture2D okMessage;
        private Rectangle b1BundingBox;
        private Rectangle b2BundingBox;
        private Rectangle mouseBoundingBox;
        private Vector2 position;
        private Vector2 positionB1;
        private Vector2 positionB2;
        private Vector2 positionOkError;
        private MouseState mouse;
        private int width;
        private int height;
        private int timer = 60;
        private int delay = -1;
        private int stopSec = 20;
        private float numberSec;
        private string way;
        private bool end = true;
        private bool error = true;
        public event MyAction onEnd;
        #endregion Создние

        #region Публичные поля
        public string Way { get { return way; } }
        public bool End { get { return end; } set { end = value; } }
        #endregion Публичные поля

        #region Реализация

        public void WidthHeight(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void LoadContent(ContentManager content, GraphicsDevice GraphicsDevice)
        {
            message = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Message.png"));
            b1 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\MessageB1.png"));
            b2 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\MessageB2.png"));
            errorMessage = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\error.png"));
            okMessage = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\ok.png"));
            positionOkError.Y = 0 - errorMessage.Height;
            numberSec = (float)errorMessage.Height / (float)(timer - stopSec);
        }

        public void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            mouseBoundingBox = new Rectangle((int)mouse.Position.X, (int)mouse.Position.Y, 0, 0);
            b1BundingBox = new Rectangle((int)positionB1.X, (int)positionB1.Y, b1.Width, b1.Height);
            b2BundingBox = new Rectangle((int)positionB2.X, (int)positionB2.Y, b2.Width, b2.Height);
            position = new Vector2(width / 2 - message.Width / 2, height / 2 - message.Height / 2);
            positionB1 = new Vector2(position.X + 10, position.Y + message.Height - 10 - b1.Height);
            positionB2 = new Vector2(position.X + message.Width - 10 - b2.Width, position.Y + message.Height - 10 - b2.Height);
            positionOkError.X = (width / 2) - (errorMessage.Width / 2);

            if (delay == -1)
            {
                if (mouseBoundingBox.Intersects(b1BundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        try
                        {
                            if (File.Exists("Save")) { } else { Directory.CreateDirectory(@"Save"); }
                            StreamReader File2 = new StreamReader(@"Link.txt");
                            way = File2.ReadLine();
                            File2.Close();
                            if (File.Exists(way) == true)
                            {
                                string name = way;
                                while (name.IndexOf(@"\") > -1)
                                {
                                    name = name.Remove(0, name.IndexOf(@"\") + 1);
                                }
                                name = @"Save\" + name;
                                if (File.Exists(name)) { } else { File.Copy(way, name); }
                                StreamWriter File1 = new StreamWriter(@"Link.txt");
                                way = name;
                                File1.Close();
                                if (way == "")
                                {
                                    error = true;
                                }
                                else
                                {
                                    if (way == " ")
                                    {
                                        error = true;
                                    }
                                    else
                                    {
                                        if (way == null)
                                        {
                                            error = true;
                                        }
                                        else
                                        {
                                            error = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                error = true;
                            }
                        }
                        catch
                        {
                            error = true;
                        }
                        delay++;
                    }
                }

                if (mouseBoundingBox.Intersects(b2BundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        end = false;
                    }
                }
            }

            if (delay >= timer)
            {
                delay = -1;
                end = false;
                positionOkError.Y = 0 - errorMessage.Height;
                if (error == false)
                {
                    onEnd();
                }
            }

            if (delay >= 0)
            {
                if (delay <= timer)
                {
                    delay++;
                    if (delay < timer - stopSec)
                    {
                        positionOkError.Y += numberSec;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (delay == -1)
            {
                spriteBatch.Draw(message, position, Color.White);
                spriteBatch.Draw(b1, b1BundingBox, Color.White);
                spriteBatch.Draw(b2, b2BundingBox, Color.White);
            }

            if (delay >= 0)
            {
                if (error == true)
                {
                    spriteBatch.Draw(errorMessage, positionOkError, Color.White);
                }
                else
                {
                    spriteBatch.Draw(okMessage, positionOkError, Color.White);
                }
            }
        }

        #endregion Реализация
    }
}
