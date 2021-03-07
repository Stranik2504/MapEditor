using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace MapEditor.Classes
{
    public delegate void GetCharacteristic(string filePath, string name, Texture2D texture);
    class MenuComponent
    {
        #region Create
        private Texture2D texture;
        private SpriteFont spriteFont;
        private Rectangle boundingBox;
        private Vector2 position;
        private KeyboardState keyboard;
        private KeyboardState prevKeyboard;
        private Keys key;
        private int widthObject;
        private int heightObject;
        private int delay = 0;
        private int timer = 30;
        private string way;
        private string name;
        private bool isVisible;
        private bool activate = false;
        private bool activateOnButton = false;
        public event GetCharacteristic Action;
        public event SetCharacteristic setCharacteristicSave;
        #endregion Create

        #region Public fields
        public bool IsVisible { get { return isVisible; } }
        public float Y { get { return position.X; } }
        #endregion Public fields

        #region Methods

        private bool CheckKeyBoard(Keys key)
        {
            return (keyboard.IsKeyDown(key) && !prevKeyboard.IsKeyDown(key));
        }

        public void GetCharacteristic(string filePath, GraphicsDevice GraphicsDevice)
        {
            way = filePath;
            name = way;
            name = name.Remove(name.IndexOf("."), name.Length - name.IndexOf("."));
            while (name.IndexOf(@"\") > -1)
            {
                name = name.Remove(0, name.IndexOf(@"\") + 1);
            }
            texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@way));
            isVisible = true;
        }

        public void GetOpenedCharacteristic(GraphicsDevice GraphicsDevice, int x, int y, float widthObject, float heightObject, float rotation, string way, string name)
        {
            texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@way));
            position.X = x;
            position.Y = y;
            this.widthObject = (int)widthObject;
            this.heightObject = (int)heightObject;
            this.way = way;
            this.name = name;
            isVisible = true;
        }

        public void Scroll(int coefficient)
        {
            position.Y += coefficient;
        }

        public void NewCharacteristic(int x, int y, int height, int width)
        {
            widthObject = width;
            heightObject = height;
            position.X = x;
            position.Y = y;
        }

        public void SetCharacteristicSave()
        {
            setCharacteristicSave((int)position.X, (int)position.Y, widthObject, heightObject, 0, way, name);
        }

        public void unIsVisible()
        {
            isVisible = !isVisible;
        }

        public void GetSpriteFont(SpriteFont spriteFont)
        {
            this.spriteFont = spriteFont;
        }

        public void ActivateButton(Keys key)
        {
            this.key = key;
            activateOnButton = true;
        }

        public void Update(GameTime gameTime, MouseState mouse, Rectangle mouseBoundingBox, KeyboardState keyboard, KeyboardState prevKeyboard)
        {
            this.keyboard = keyboard;
            if (isVisible == true)
            {
                boundingBox = new Rectangle((int)position.X, (int)position.Y, widthObject, heightObject);

                if (mouseBoundingBox.Intersects(boundingBox)) { if (mouse.LeftButton == ButtonState.Pressed) { if (activate == false) { if (delay >= timer) { activate = true; Action(way, name, texture); delay = 0; } } } else { activate = false; } } else { activate = false; }
                if(delay >= 0) { if(delay <= timer) { delay++; } }
            }

            if (activateOnButton == true) { if (CheckKeyBoard(key) == true) { if (activate == false) { activate = true; Action(way, name, texture); } } else { activate = false; } }
            this.prevKeyboard = this.keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                try { spriteBatch.Draw(texture, boundingBox, Color.White); } catch { }
                try { spriteBatch.DrawString(spriteFont, name, new Vector2(boundingBox.X + boundingBox.Width + 5, boundingBox.Y + ((boundingBox.Height - 20) / 2)), Color.LightGray); } catch { }
            }
        }

        #endregion Methods
    }
}
