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
    class Component
    {
        #region Создание
        private Texture2D texture;
        private Rectangle boundingBox;
        private Rectangle mouseBoundingBox;
        private Vector2 position;
        private MouseState mouse;
        private KeyboardState keyboard;
        private float differenceWidth;
        private float differenceHeight;
        private float widthObject;
        private float heightObject;
        private int widht;
        private int height;
        private int widthCell;
        private int x;
        private int y;
        private int ratio = 1;
        private string way;
        private string name;
        private bool go;
        private bool activate;
        private bool activate2;
        private bool activeField;
        private bool click = false;
        #endregion Создание

        #region Публичные поля
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int WidthObject { get { return (int)widthObject; } }
        public int HeightObject { get { return (int)heightObject; } }
        public int Ration { set { ratio = value; } }
        public string Name { get { return name; } }
        public string Ways { get { return way; } }
        public bool Go { get { return go; } }
        public bool Activate { get { return activate; } }
        #endregion Публичные поля

        #region Реализация

        public Component(Vector2 position, string way, int widht, int height)
        {
            this.way = way;
            this.position = position;
            this.widht = widht;
            this.height = height;
        }

        public void Way(string way)
        {
            this.way = way;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@way));
            widthObject = texture.Width;
            heightObject = texture.Height;
        }

        private void UpdateMoveMouse(GameTime gameTime, bool af)
        {
            mouse = Mouse.GetState();
            boundingBox = new Rectangle((int)position.X, (int)position.Y, (int)widthObject, (int)heightObject);
            mouseBoundingBox = new Rectangle((int)mouse.Position.X, (int)mouse.Position.Y, 0, 0);
            if (mouseBoundingBox.Intersects(boundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    go = true;
                }
            }

            if (mouse.LeftButton == ButtonState.Released)
            {
                click = false;
                go = false;
            }

            if (af == false)
            {
                if (go == true)
                {
                    if (click == false)
                    {
                        differenceWidth = mouse.Position.X - position.X;
                        differenceHeight = mouse.Position.Y - position.Y;
                        click = true;
                    }
                    position.X = mouse.Position.X - differenceWidth;
                    position.Y = mouse.Position.Y - differenceHeight;
                }
            }

            if (af == true)
            {
                if (go == true)
                {
                    if (click == false)
                    {
                        differenceWidth = mouse.Position.X - position.X;
                        differenceHeight = mouse.Position.Y - position.Y;
                        click = true;
                    }
                    position.X = mouse.Position.X - differenceWidth;
                    position.Y = mouse.Position.Y - differenceHeight;
                    position.X -= (position.X % widthCell);
                    position.Y -= (position.Y % widthCell);
                }
            }
        }

        public void Cheak(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            boundingBox = new Rectangle((int)position.X, (int)position.Y, (int)widthObject, (int)heightObject);
            mouseBoundingBox = new Rectangle((int)mouse.Position.X, (int)mouse.Position.Y, 0, 0);

            if (mouseBoundingBox.Intersects(boundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    activate = true;
                }
            }

            if (activate == true)
            {
                if (mouse.LeftButton == ButtonState.Released)
                {
                    activate2 = true;
                }

                if (activate2 == true)
                {
                    if (mouseBoundingBox.Intersects(boundingBox) == false)
                    {
                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            activate2 = false;
                            activate = false;
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (activeField == true)
            {
                UpdateMoveKeyboard(gameTime, true);
                UpdateWidthHeight1(gameTime, true);
                UpdateMoveMouse(gameTime, true);
            }
            else
            {
                UpdateMoveMouse(gameTime, false);
                UpdateMoveKeyboard(gameTime, false);
                UpdateWidthHeight1(gameTime, false);
            }
        }

        private void UpdateMoveKeyboard(GameTime gameTime, bool af)
        {
            keyboard = Keyboard.GetState();

            if (af == false)
            {
                if (keyboard.IsKeyDown(Keys.LeftShift))
                {
                    if (keyboard.IsKeyDown(Keys.Q))
                    {

                    }
                    else
                    {
                        if (keyboard.IsKeyDown(Keys.E))
                        {

                        }
                        else
                        {
                            UpdateMoveKeyboard2(gameTime, 1);
                        }
                    }
                }
                else
                {
                    if (keyboard.IsKeyDown(Keys.LeftControl))
                    {
                        if (keyboard.IsKeyDown(Keys.Q))
                        {

                        }
                        else
                        {
                            if (keyboard.IsKeyDown(Keys.E))
                            {

                            }
                            else
                            {
                                UpdateMoveKeyboard2(gameTime, 20);
                            }
                        }
                    }
                    else
                    {
                        if (keyboard.IsKeyDown(Keys.Q))
                        {

                        }
                        else
                        {
                            if (keyboard.IsKeyDown(Keys.E))
                            {

                            }
                            else
                            {
                                UpdateMoveKeyboard2(gameTime, 10);
                            }
                        }
                    }
                }
            }
            else
            {
                if (keyboard.IsKeyDown(Keys.LeftShift))
                {
                    if (keyboard.IsKeyDown(Keys.Q))
                    {

                    }
                    else
                    {
                        if (keyboard.IsKeyDown(Keys.E))
                        {

                        }
                        else
                        {
                            UpdateMoveKeyboard2(gameTime, widthCell);
                        }
                    }
                }
                else
                {
                    if (keyboard.IsKeyDown(Keys.LeftControl))
                    {
                        if (keyboard.IsKeyDown(Keys.Q))
                        {

                        }
                        else
                        {
                            if (keyboard.IsKeyDown(Keys.E))
                            {

                            }
                            else
                            {
                                UpdateMoveKeyboard2(gameTime, 4 * widthCell);
                            }
                        }
                    }
                    else
                    {
                        if (keyboard.IsKeyDown(Keys.Q))
                        {

                        }
                        else
                        {
                            if (keyboard.IsKeyDown(Keys.E))
                            {

                            }
                            else
                            {
                                UpdateMoveKeyboard2(gameTime, 2 * widthCell);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateMoveKeyboard2(GameTime gameTime, int speed)
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                position.X += speed;
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }



            if (position.X < 1 - boundingBox.Width)
            {
                position.X = 1 - boundingBox.Width;
            }

            if (position.Y < 1 - boundingBox.Height)
            {
                position.Y = 1 - boundingBox.Height;
            }

            if (position.X + boundingBox.Width > widht + boundingBox.Width - 1)
            {
                position.X = widht - 1;
            }

            if (position.Y + boundingBox.Height > height + boundingBox.Height - 1)
            {
                position.Y = height - 1;
            }
        }

        private void UpdateWidthHeight1(GameTime gameTime, bool af)
        {
            keyboard = Keyboard.GetState();

            if (af == false)
            {
                if (keyboard.IsKeyDown(Keys.Q))
                {
                    UpdateWidthHeight12(gameTime, 0.1f * ratio);
                }

                if (keyboard.IsKeyDown(Keys.E))
                {
                    UpdateWidthHeight13(gameTime, 0.1f * ratio);
                }
            }
            else
            {
                if (keyboard.IsKeyDown(Keys.Q))
                {
                    UpdateWidthHeight12(gameTime, widthCell * ratio);
                }

                if (keyboard.IsKeyDown(Keys.E))
                {
                    UpdateWidthHeight13(gameTime, widthCell * ratio);
                }
            }

        }

        private void UpdateWidthHeight12(GameTime gameTime, float speed)
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.A))
            {
                position.X -= speed;
                widthObject += speed;
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                widthObject += speed;
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
                heightObject += speed;
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                heightObject += speed;
            }
            boundingBox.Width = (int)widthObject;
            boundingBox.Height = (int)heightObject;
        }

        private void UpdateWidthHeight13(GameTime gameTime, float speed)
        {
            keyboard = Keyboard.GetState();

            if (widthObject > 0)
            {
                if (keyboard.IsKeyDown(Keys.A))
                {
                    widthObject -= speed;
                }

                if (keyboard.IsKeyDown(Keys.D))
                {
                    position.X += speed;
                    widthObject -= speed;
                }
            }

            if (heightObject > 0)
            {
                if (keyboard.IsKeyDown(Keys.W))
                {
                    heightObject -= speed;
                }

                if (keyboard.IsKeyDown(Keys.S))
                {
                    position.Y += speed;
                    heightObject -= speed;
                }
            }
            boundingBox.Width = (int)widthObject;
            boundingBox.Height = (int)heightObject;
        }

        public void ActiveField(bool activeField, int widthCell)
        {
            this.activeField = activeField;
            this.widthCell = widthCell;
            if (activeField == true)
            {
                if (widthCell <= 0)
                {

                }
                else
                {
                    position.X -= (position.X % widthCell);
                    position.Y -= (position.Y % widthCell);
                    widthObject -= (widthObject % widthCell);
                    heightObject -= (heightObject % widthCell);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.Draw(texture, boundingBox, Color.White);
            }
            catch { }
        }

        public void Save(StreamWriter File1, bool iff, string path)
        {
            name = way;
            name = name.Remove(name.IndexOf("."), name.Length - name.IndexOf("."));
            while (name.IndexOf(@"\") > -1)
            {
                name = name.Remove(0, name.IndexOf(@"\") + 1);
            }
            File1.WriteLine(name);
            File1.WriteLine(position.X);
            File1.WriteLine(position.Y);
            File1.WriteLine(widthObject);
            File1.WriteLine(heightObject);
            if (iff == true)
            {
                File1.WriteLine(path + way);
            }
            else
            {
                File1.WriteLine(way);
            }
        }

        public void Save2()
        {
            name = way;
            name = name.Remove(name.IndexOf("."), name.Length - name.IndexOf("."));
            while (name.IndexOf(@"\") > -1)
            {
                name = name.Remove(0, name.IndexOf(@"\") + 1);
            }
            x = (int)position.X;
            y = (int)position.Y;
        }

        public void Load(string name, int x, int y, int widthObject, int heightObject, string way)
        {
            this.name = name;
            position.X = x;
            position.Y = y;
            this.widthObject = widthObject;
            this.heightObject = heightObject;
            this.way = way;
        }

        #endregion Реализация
    }
}
