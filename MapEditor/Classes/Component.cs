using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace MapEditor.Classes
{
    public delegate void SetCharacteristic(int x, int y, float width, float height, float rotation, string way, string name);
    class Component
    {
        #region Create
        private Texture2D texture;
        private Rectangle boundingBox;
        private Vector2 position;
        private Vector2 deviationPosition;
        private Color color;
        private ClassFrame.Frame frame;
        private int width;
        private int height;
        private int widthCell;
        private float widthObject;
        private float heightObject;
        private float differentWidth;
        private float differentHeight;
        private float rotation;
        private float alpha;
        private string way;
        private string name;
        private bool isVisible;
        private bool activate;
        private bool activateInfo;
        private bool mouseMove;
        private bool activateField = false;
        private bool activateFrame = false;
        public event SetCharacteristic setCharacteristic;
        public event SetCharacteristic setCharacteristicSave;
        #endregion Create

        #region Public fields
        public bool Activate { get { return activate; } }
        #endregion Public fields

        #region Methods

        private void UpdateActivateInfo(GameTime gameTime, MouseState mouse, Rectangle mouseBoundingBox, KeyboardState keyboard)
        {
            if (mouseBoundingBox.Intersects(boundingBox))
            {
                if (mouse.X <= width - 300)
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        activate = true;
                    }
                }
            }

            if (mouseBoundingBox.Intersects(boundingBox) == false)
            {
                if (mouse.X <= width - 300)
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseMove == false) { activate = false; }
                    }
                }
            }
        }

        private void UpdateActivate(GameTime gameTime, MouseState mouse, Rectangle mouseBoundingBox, KeyboardState keyboard)
        {
            if (mouseBoundingBox.Intersects(boundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    activate = true;
                }
            }

            if (mouseBoundingBox.Intersects(boundingBox) == false)
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (mouseMove == false) { activate = false; }
                }
            }
        }

        private void UpdateMove(KeyboardState keyboard, float speed)
        {
            if (keyboard.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }

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
        }

        private void MouseMove(MouseState mouse, Rectangle mouseBoundingBox)
        {
            if (mouseBoundingBox.Intersects(boundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (mouseMove == false)
                    {
                        mouseMove = true;
                        differentWidth = position.X - mouse.X;
                        differentHeight = position.Y - mouse.Y;
                    }
                }
            }

            if (mouseMove == true)
            {
                if (mouse.LeftButton == ButtonState.Released)
                {
                    mouseMove = false;
                }
                else
                {
                    position.X = mouse.X + differentWidth;
                    position.Y = mouse.Y + differentHeight;
                }
            }
        }

        private void IncreaseMove(KeyboardState keyboard, int speed, bool Increase)
        {
            if (keyboard.IsKeyDown(Keys.A))
            {
                if (Increase == true) { widthObject += speed; position.X -= speed; } else { widthObject += speed; }
                if (activateField == true) { widthObject += (widthObject % widthCell); heightObject += (heightObject % widthCell); }
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                if (Increase == true) { heightObject += speed; position.Y -= speed; } else { heightObject += speed; }
                if (activateField == true) { widthObject += (widthObject % widthCell); heightObject += (heightObject % widthCell); }
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                if (Increase == true) { heightObject += speed; } else { heightObject += speed; position.Y -= speed; }
                if (activateField == true) { widthObject += (widthObject % widthCell); heightObject += (heightObject % widthCell); }
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                if (Increase == true) { widthObject += speed; } else { widthObject += speed; position.X -= speed; }
                if (activateField == true) { widthObject += (widthObject % widthCell); heightObject += (heightObject % widthCell); }
            }
        }

        private void ActivateFrame(GameTime gameTime, MouseState mouse, Rectangle mouseBoundingBox)
        {
            for (int i = 0; i < frame.BandBoundingBox.Length; i++)
            {
                if (mouseBoundingBox.Intersects(frame.BandBoundingBox[i])) { activateFrame = true; } else { if (activateFrame == false) { activateFrame = false; } }
                if (mouseBoundingBox.Intersects(frame.BandBoundingBox2[i])) { activateFrame = true; } else { if (activateFrame == false) { activateFrame = false; } }
            }
            if (activateField == true) { activate = true; }
            Console.WriteLine(activateFrame);
            activateFrame = false;
            for (int i = 0; i < frame.VerticalBoundingBox.Length; i++)
            {
                if (mouseBoundingBox.Intersects(frame.VerticalBoundingBox[i])) { activateFrame = true; } else { if (activateFrame == false) { activateFrame = false; } }
                if (mouseBoundingBox.Intersects(frame.VerticalBoundingBox[i])) { activateFrame = true; } else { if (activateFrame == false) { activateFrame = false; } }
            }
            if (activateField == true) { activate = true; }
            Console.WriteLine(activateFrame);
            activateFrame = false;
        }

        public void GetFrame(Texture2D textureBand) { frame = new ClassFrame.Frame((int)heightObject, (int)widthObject, (int)position.X, (int)position.Y, 5, 5, textureBand); }

        public void GetCharacteristic(string filePath, string name, Texture2D texture)
        {
            way = filePath;
            this.name = name;
            this.texture = texture;
            widthObject = texture.Width;
            heightObject = texture.Height;
            color = Color.White;
            deviationPosition = new Vector2(0, 20);
            position = new Vector2(deviationPosition.X + 0, deviationPosition.Y + 0);
            isVisible = true;
        }

        public void GetNewCharacteristic(int x, int y, int width, int height, float rotation, string way, string name)
        {
            position = new Vector2(x + deviationPosition.X, y + deviationPosition.Y);
            widthObject = width;
            heightObject = height;
            this.way = way;
            this.name = name;
            this.rotation = rotation;
        }

        public void GetOpenedCharacteristic(GraphicsDevice GraphicsDevice,int x, int y, int widthObject, int heightObject, float rotation, string way, string name)
        {
            deviationPosition = new Vector2(0, 20);
            color = Color.White;
            position = new Vector2(x + deviationPosition.X, y + deviationPosition.Y);
            this.widthObject = widthObject;
            this.heightObject = heightObject;
            this.way = way;
            this.name = name;
            this.rotation = rotation;
            texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@way));
            isVisible = true;
        }

        public void unIsVisible()
        {
            isVisible = !isVisible;
        }
        
        public void DisActivate()
        {
            activate = false;
        }

        public void GetWH(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Update(GameTime gameTime, MouseState mouse, Rectangle mouseBoundingBox, KeyboardState keyboard)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, (int)widthObject, (int)heightObject);

            if (activateInfo == true){ UpdateActivateInfo(gameTime, mouse, mouseBoundingBox, keyboard); } else { UpdateActivate(gameTime, mouse, mouseBoundingBox, keyboard); }
            if (activate == true)
            {
                if (activateField == true)
                {
                    if (keyboard.IsKeyDown(Keys.LeftAlt) && keyboard.IsKeyDown(Keys.LeftAlt)) { UpdateMove(keyboard, widthCell); } else { if (keyboard.IsKeyDown(Keys.Q)) { IncreaseMove(keyboard, widthCell, true); } else { if (keyboard.IsKeyDown(Keys.E)) { IncreaseMove(keyboard, -widthCell, false); } else { UpdateMove(keyboard, 2 * widthCell); } } }
                    if (keyboard.IsKeyDown(Keys.Space)) { UpdateMove(keyboard, 3 * widthCell); }
                    if (position.X % widthCell != 0) { position.X -= position.X % widthCell; } if (position.Y % widthCell != 0) { position.Y -= position.Y % widthCell; }
                }
                else
                {
                    if (keyboard.IsKeyDown(Keys.LeftAlt) && keyboard.IsKeyDown(Keys.LeftAlt)) { UpdateMove(keyboard, 0.1f); } else { if (keyboard.IsKeyDown(Keys.Q)) { IncreaseMove(keyboard, 1, true); } else { if (keyboard.IsKeyDown(Keys.E)) { IncreaseMove(keyboard, -1, false); } else { UpdateMove(keyboard, 1); } } }
                    if (keyboard.IsKeyDown(Keys.Space)) { UpdateMove(keyboard, 2); }
                }
                if (keyboard.IsKeyDown(Keys.B)) { position = new Vector2(0 + deviationPosition.X, 0 + deviationPosition.Y); }
                if (keyboard.IsKeyDown(Keys.R)) { rotation += 0.1f; }
                if (keyboard.IsKeyDown(Keys.F)) { rotation -= 0.1f; }

                MouseMove(mouse, mouseBoundingBox);
                SetCharacteristic();
                frame.Update(gameTime, (int)position.X, (int)position.Y, (int)widthObject, (int)heightObject, rotation);
            }
        }

        public void SetCharacteristic()
        {
            setCharacteristic((int)position.X - (int)deviationPosition.X, (int)position.Y - (int)deviationPosition.Y, widthObject, heightObject, rotation, way, name);
        }

        public void SetCharacteristicSave()
        {
            setCharacteristicSave((int)position.X - (int)deviationPosition.X, (int)position.Y - (int)deviationPosition.Y, widthObject, heightObject, rotation, way, name);
        }

        public void ActivateField(int widthCell)
        {
            activateField = !activateField;
            this.widthCell = widthCell;
        }

        public void GetInfo(bool activateInfo)
        {
            this.activateInfo = activateInfo;
        }

        public void Darw(SpriteBatch spriteBatch)
        {
            alpha = MathHelper.ToRadians(rotation);
            if (isVisible == true)
            {
                try { spriteBatch.Draw(texture, boundingBox, null, Color.White, alpha, new Vector2(0, 0), SpriteEffects.None, 1f); } catch { }
                if (activate == true) { try { frame.Draw(spriteBatch); } catch { } }
            }
        }

        #endregion Methods
    }
}
