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
    class EditCells
    {
        #region Создание
        private Texture2D cellsOn;
        private Texture2D cellsOff;
        private Texture2D message;
        private Texture2D b1;
        private Texture2D b2;
        private Texture2D errorMessage;
        private Texture2D okMessage;
        private Rectangle cellsBoundingBox;
        private Rectangle mouseBoundingBox;
        private Rectangle b1BoundingBox;
        private Rectangle b2BoundingBox;
        private Vector2 position;
        private Vector2 positionB1;
        private Vector2 positionB2;
        private Vector2 positionOkError;
        private MouseState mouse;
        private int X, Y;
        private int Timer = 120;
        private int delay = 60;
        private int delay2 = -1;
        private int cellsWidth = 0;
        private int width;
        private int height;
        private int stopSec = 20;
        private float numberSec;
        private bool active = false;
        private bool pressed = false;
        private bool error = true;
        private bool end;
        private bool activateCells = false;
        public event MyAction onEnd;
        #endregion Создание

        #region Публичные поля
        public int CellsWidth { get { return cellsWidth; } }
        public bool End { get { return end; } }
        public bool ActivateCells { get { return activateCells; } }
        #endregion Публичные поля

        #region Реализация

        public void XY(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void Active(bool active)
        {
            this.active = active;
        }

        public void WidthHeight(int Width, int Height)
        {
            width = Width;
            height = Height;
        }

        public void Ended(bool end)
        {
            this.end = end;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            message = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Message2.png"));
            b1 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\MessageB1.png"));
            b2 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\MessageB2.png"));
            cellsOn = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Cells2.png"));
            cellsOff = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Cells.png"));
            errorMessage = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\error.png"));
            okMessage = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\ok.png"));
            positionOkError.Y = 0 - errorMessage.Height;
            numberSec = (float)errorMessage.Height / (float)(Timer - stopSec);
        }

        public void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            cellsBoundingBox = new Rectangle(X, Y - 10 - cellsOn.Height, 20, 20);
            mouseBoundingBox = new Rectangle((int)mouse.Position.X, (int)mouse.Position.Y, 0, 0);
            b1BoundingBox = new Rectangle((int)positionB1.X, (int)positionB1.Y, b1.Width, b1.Height);
            b2BoundingBox = new Rectangle((int)positionB2.X, (int)positionB2.Y, b2.Width, b2.Height);
            position = new Vector2(width / 2 - message.Width / 2, height / 2 - message.Height / 2);
            positionB1 = new Vector2(position.X + 10, position.Y + message.Height - 10 - b1.Height);
            positionB2 = new Vector2(position.X + message.Width - 10 - b2.Width, position.Y + message.Height - 10 - b2.Height);
            positionOkError.X = (width / 2) - (errorMessage.Width / 2);

            if (delay <= Timer)
            {
                delay++;
            }

            if (delay2 >= 0)
            {
                if (delay2 <= Timer)
                {
                    delay2++;
                    if (delay2 < Timer - stopSec)
                    {
                        positionOkError.Y += numberSec;
                    }
                }
            }

            if (delay2 >= Timer)
            {
                delay2 = -1;
                positionOkError.Y = 0 - errorMessage.Height;
                if (activateCells == true)
                {
                    onEnd();
                }
            }

            if (pressed == true)
            {
                if (mouseBoundingBox.Intersects(b1BoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        try
                        {
                            StreamReader File2 = new StreamReader(@"Link.txt");
                            cellsWidth = Convert.ToInt32(File2.ReadLine());
                            File2.Close();
                            StreamWriter File3 = new StreamWriter(@"Link.txt");
                            File3.Close();
                            if (cellsWidth == 0)
                            {
                                error = true;
                                pressed = false;
                                activateCells = false;
                                end = false;
                                delay2 = 0;
                            }
                            else
                            {
                                delay2 = 0;
                                pressed = false;
                            }
                        }
                        catch
                        {
                            error = true;
                            pressed = false;
                            activateCells = false;
                            delay2 = 0;
                        }
                    }
                }

                if (mouseBoundingBox.Intersects(b2BoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        activateCells = false;
                        pressed = false;
                        delay2 = -1;
                    }
                }
            }
            else
            {
                if (delay2 == -1)
                {
                    if (mouseBoundingBox.Intersects(cellsBoundingBox))
                    {
                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            if (delay >= Timer)
                            {
                                delay = 0;
                                if (activateCells == false)
                                {
                                    activateCells = true;
                                    pressed = true;
                                    error = false;
                                }
                                else
                                {
                                    activateCells = false;
                                    end = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                if (pressed == true)
                {
                    if (delay2 == -1)
                    {
                        spriteBatch.Draw(message, position, Color.White);
                        spriteBatch.Draw(b1, b1BoundingBox, Color.White);
                        spriteBatch.Draw(b2, b2BoundingBox, Color.White);
                        spriteBatch.Draw(cellsOff, cellsBoundingBox, Color.White);
                    }
                }
                else
                {
                    if (activateCells == false)
                    {
                        spriteBatch.Draw(cellsOff, cellsBoundingBox, Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(cellsOn, cellsBoundingBox, Color.White);
                    }

                    if (delay2 >= 0)
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
            }
        }

        #endregion Реализация
    }
}
