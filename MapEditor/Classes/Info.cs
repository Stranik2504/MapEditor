using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using ClassMonogame;

namespace MapEditor.Classes
{
    class Info
    {
        #region Create
        private Texture2D open;
        private Texture2D close;
        private SpriteFont spriteFont;
        private KeyboardState keyboard;
        private KeyboardState prevKeyboard;
        private Button button = new Button();
        private Message message = new Message();
        private Message[] messages = new Message[8];
        private TextBoxNumber[] textboxs = new TextBoxNumber[5];
        private TextBox[] textBoxes = new TextBox[2];
        private Lable[] lables = new Lable[7];
        private int width;
        private int height;
        private string way;
        private string fullWay;
        private bool isVisible = false;
        private bool disActivate = false;
        private bool[] returnCharacteristic = new bool[6];
        public event MyAction Closed;
        public event MyAction Opened;
        public event SetCharacteristic setCharacteristic;
            #region Characteristic
        private int indent = 300;
        private int widthButton = 20;
        #endregion Characteristic
        #endregion Create

        #region Designer

        public Info()
        {
            for (int i = 0; i < messages.Length; i++)
            {
                messages[i] = new Message();
            }
        }

        #endregion Designer

        #region Methods

        private bool CheckKeyBoard(Keys key)
        {
            return (keyboard.IsKeyDown(key) && !prevKeyboard.IsKeyDown(key));
        }

        public void GetCharacteristic(int x, int y, float width, float height, float rotation, string way, string name)
        {
            if (textboxs[0].Activate == false) { textboxs[0].GetText(x.ToString()); }
            if (textboxs[1].Activate == false) { textboxs[1].GetText(y.ToString()); }
            if (textboxs[2].Activate == false) { textboxs[2].GetText(width.ToString()); }
            if (textboxs[3].Activate == false) { textboxs[3].GetText(height.ToString()); }
            if (textboxs[4].Activate == false) { textboxs[4].GetText(rotation.ToString()); }
            fullWay = way;
            if (fullWay.Length > 17)
            {
                this.way = fullWay;
                this.way = way.Remove(15);
                this.way += "..";
            }
            textBoxes[1].GetText(this.way);
            if (textBoxes[0].Activate == false) { textBoxes[0].GetText(name); }
        }

        public void SetCharacteristic()
        {
            float x;
            float y;
            float width;
            float height;
            float rotation;
            try { if (Convert.ToInt32(textboxs[0].Text) > -1000000 && Convert.ToInt32(textboxs[0].Text) < 1000000) { x = Convert.ToInt32(textboxs[0].Text); } else { x = 0; } } catch { x = 0; }
            try { if (Convert.ToInt32(textboxs[1].Text) > -1000000 && Convert.ToInt32(textboxs[1].Text) < 1000000) { y = Convert.ToInt32(textboxs[1].Text); } else { y = 0; } } catch { y = 0; }
            try { if (Convert.ToInt32(textboxs[2].Text) < 1) { width = 0; } else { width = Convert.ToInt32(textboxs[2].Text); } } catch { width = 1; }
            try { if (Convert.ToInt32(textboxs[3].Text) < 1) { height = 0; } else { height = Convert.ToInt32(textboxs[3].Text); } } catch { height = 1; }
            try { if (Convert.ToInt32(textboxs[4].Text) > 0) { if (Convert.ToInt32(textboxs[4].Text) < 360) { rotation = Convert.ToInt32(textboxs[4].Text); } else { rotation = 0; } } else { rotation = 0; } } catch { rotation = 0; }
            if (width > 10000000) { width = 1000000; }
            if (height > 10000000) { height = 1000000; }
            setCharacteristic((int)x, (int)y, width, height, rotation, fullWay, textBoxes[0].Text); 
        }

        public void LoadContent(ContentManager content)
        {
            Texture2D messageTexture = content.Load<Texture2D>("selected1");
            Texture2D messageTexture2 = content.Load<Texture2D>("selected2");
            open = content.Load<Texture2D>("selected3");
            close = content.Load<Texture2D>("selected4");
            spriteFont = content.Load<SpriteFont>("font");
            button.NewPosition(width - button.Width, widthButton);
            button.NewTexture(open);
            button.NewSize(widthButton, widthButton);
            message.NewTexture(messageTexture);
            message.NewSize(indent, height);
            message.NewPosition(width - message.Width, 0);
            message.Visible = false;

            for (int i = 1; i < messages.Length; i++)
            {
                messages[i].NewTexture(messageTexture2);
                messages[i].NewSize(message.Width, 1);
                messages[i].NewPosition(message.X, 20 + (10 + widthButton) * i);
                messages[i].Visible = true;
            }

            messages[0].NewTexture(messageTexture2);
            messages[0].NewSize(1, messages[messages.Length - 1].Y);
            messages[0].NewPosition(message.X + ((message.Width - messages[0].Width) / 2), 0);

            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i] = new TextBox(spriteFont, (message.Width - 20) / 2, widthButton, messages[0].X + 10, 25 + (widthButton + 10) * i + (widthButton + 10) * textboxs.Length);
                textBoxes[i].GetTexture(messageTexture2);
                textBoxes[i].NullText();
                textBoxes[i].ColorString = Color.LightGray;
                textBoxes[i].Enabled = true;
            }

            textBoxes[0].Enabled = false;

            for (int i = 0; i < textboxs.Length; i++)
            {
                textboxs[i] = new TextBoxNumber(spriteFont, (message.Width - 20) / 2, widthButton, messages[0].X + 10, 25 + (widthButton + 10) * i);
                textboxs[i].GetTexture(messageTexture2);
                textboxs[i].GetText("0");
                textboxs[i].ColorString = Color.LightGray;
                textboxs[i].Enabled = false;
            }

            for (int i = 0; i < returnCharacteristic.Length; i++)
            {
                returnCharacteristic[i] = new bool();
                returnCharacteristic[i] = false;
            }

            for (int i = 0; i < lables.Length; i++)
            {
                lables[i] = new Lable(spriteFont, (message.Width - 20) / 2, widthButton, message.X + 10, 25 + (widthButton + 10) * i);
                lables[i].GetColor(Color.LightGray);
            }
            lables[0].GetText("X");
            lables[1].GetText("Y");
            lables[2].GetText("Width");
            lables[3].GetText("Height");
            lables[4].GetText("Rotation");
            lables[5].GetText("Name");
            lables[6].GetText("Way");
        }

        public void Clear()
        {
            for (int i = 0; i < textboxs.Length; i++)
            {
                textboxs[i].GetText("0");
            }

            for (int i = 0; i < textBoxes.Length; i++)
            {
                textBoxes[i].NullText();
            }
        }

        public void Update(GameTime gameTime, MouseState mouse, Rectangle mouseBoundingBox, KeyboardState keyboard, KeyboardState prevKeyboard)
        {
            this.keyboard = keyboard;
            this.prevKeyboard = prevKeyboard;
            if (isVisible == true)
            {
                button.Update(gameTime);
                button.UpdateBoundingBox(gameTime);
                message.UpdateBoundingBox(gameTime);
                if (message.Visible == true)
                {
                    for (int i = 0; i < messages.Length; i++)
                    {
                        messages[i].UpdateBoundingBox(gameTime);
                    }

                    for (int i = 0; i < textboxs.Length; i++)
                    {
                        textboxs[i].Update(gameTime);
                        textboxs[i].UpdateBoundingBox(gameTime);
                        if (textboxs[i].Activate == true)
                        {
                            returnCharacteristic[i] = true;
                        }
                        else
                        {
                            if (returnCharacteristic[i] == true)
                            {
                                SetCharacteristic();
                                returnCharacteristic[i] = false;
                            }
                        }
                    }

                    for (int i = 0; i < lables.Length; i++)
                    {
                        lables[i].UpdateBoundingBox(gameTime);
                    }

                    for (int i = 0; i < textBoxes.Length; i++)
                    {
                        textBoxes[i].Update(gameTime);
                        textBoxes[i].UpdateBoundingBox(gameTime);
                        if (textBoxes[i].Activate == true)
                        {
                            returnCharacteristic[i] = true;
                        }
                        else
                        {
                            if (returnCharacteristic[i] == true)
                            {
                                SetCharacteristic();
                                returnCharacteristic[i] = false;
                            }
                        }
                    }

                    
                }

                if (CheckKeyBoard(Keys.Tab))
                {
                    if (button.Visible == false)
                    {
                        button.NewPosition(width - button.Width - message.Width, widthButton);
                        button.NewTexture(close);
                        message.Visible = true;
                        Closed();
                    }
                    else
                    {
                        button.NewPosition(width - button.Width, widthButton);
                        button.NewTexture(open);
                        message.Visible = false;
                        Opened();
                    }
                    button.Visible = !button.Visible;
                }

                if (button.Activate == true)
                {
                    if (button.Visible == false)
                    {
                        button.NewPosition(width - button.Width - message.Width, widthButton);
                        button.NewTexture(close);
                        message.Visible = true;
                        Closed();
                    }
                    else
                    {
                        button.NewPosition(width - button.Width, widthButton);
                        button.NewTexture(open);
                        message.Visible = false;
                        Opened();
                    }
                    button.Visible = !button.Visible;
                }
            }
        }

        public void GetWH(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void unIsVisible()
        {
            if (isVisible == true)
            {
                isVisible = !isVisible;
                button.NewPosition(width - button.Width, widthButton);
                button.NewSize(widthButton, widthButton);
                button.NewTexture(open);
                message.NewSize(indent, height);
                message.NewPosition(width - message.Width, 0);
                button.Visible = false;
                message.Visible = false;
            }
            else
            {
                if (disActivate == false)
                {
                    isVisible = !isVisible;
                    button.NewPosition(width - button.Width, widthButton);
                    button.NewSize(widthButton, widthButton);
                    button.NewTexture(open);
                    message.NewSize(indent, height);
                    message.NewPosition(width - message.Width, 0);
                    button.Visible = false;
                    message.Visible = false;
                }
            }
        }

        public void Close()
        {
            if (isVisible == true)
            {
                button.NewPosition(width - button.Width, widthButton);
                button.NewSize(widthButton, widthButton);
                button.NewTexture(open);
                message.NewSize(indent, height);
                message.NewPosition(width - message.Width, 0);
                isVisible = false;
                message.Visible = false;
                disActivate = true;
            }
        }

        public void Open()
        {
            if (disActivate ==  true)
            {
                button.NewPosition(width - button.Width, widthButton);
                button.NewSize(widthButton, widthButton);
                button.NewTexture(open);
                message.NewSize(indent, height);
                message.NewPosition(width - message.Width, 0);
                isVisible = true;
                message.Visible = false;
                disActivate = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (isVisible == true)
            {
                message.Draw(spriteBatch);
                if (message.Visible == true)
                {
                    for (int i = 0; i < messages.Length; i++)
                    {
                        messages[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < textboxs.Length; i++)
                    {
                        textboxs[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < textBoxes.Length; i++)
                    {
                        textBoxes[i].Draw(spriteBatch);
                    }

                    for (int i = 0; i < lables.Length; i++)
                    {
                        lables[i].Draw(spriteBatch);
                    }
                }
                if (button.Visible == false)
                {
                    spriteBatch.Draw(button.Texture, button.BoundingBox, Color.White);
                }
                else
                {
                    button.Draw(spriteBatch);
                }
            }
        }

        #endregion Methods
    }
}
