using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using ClassMonogame;
using System.Collections.Generic;

namespace MapEditor.Classes
{
    class Components
    {
        #region Create
        private Texture2D open;
        private Texture2D close;
        private Texture2D textureBand;
        private SpriteFont spriteFont;
        private GraphicsDevice GraphicsDevice;
        private KeyboardState keyboard;
        private KeyboardState prevKeyboard;
        private Button button = new Button();
        private Message message = new Message();
        private List<MenuComponent> listMenuComponents = new List<MenuComponent>();
        private List<Component> listComponets = new List<Component>();
        private int width;
        private int height;
        private int numberMenuComponent;
        private int numberUpdateComponent = 0;
        private int widthCell;
        private int scroll;
        private int prevScroll;
        private int indentY = 0;
        private bool isVisible;
        private bool disActivate;
        private bool activateComponent = true;
        private bool cheak = false;
        private bool activateInfo = false;
        private bool needUpdateComponents = false;
        private bool activateField = false;
        public event MyAction Closed;
        public event MyAction Opened;
        public event SetCharacteristic setCharacteristic;
        public event SetCharacteristic setCharacteristicSave;
        public event SetCharacteristic setCharacteristicSaveMenuComponetns;
        public event MyAction ClearInfo;
        public event MyAction end;
        public event MyAction endMenuComponents;
        
        #region Characteristic
        private int indent = 300;
        private int widthButton = 20;
        private int sizeMenuComponent = 40;
        #endregion Characteristic
        #endregion Create

        #region Methods

        private void UpdateComponents(GameTime gameTime, MouseState mouse, Rectangle mouseBoundingBox, KeyboardState keyboard)
        {
            if (needUpdateComponents == true)
            {
                needUpdateComponents = false;
                foreach (Component component in listComponets)
                {
                    component.Update(gameTime, mouse, mouseBoundingBox, keyboard);
                    if (activateField == true) { component.ActivateField(widthCell); }
                }
            }
        }

        private void CreateComponent(string filePath, string name, Texture2D texture)
        {
            Component component = new Component();
            component.GetCharacteristic(filePath, name, texture);
            component.setCharacteristic += SetCharacteristic;
            component.setCharacteristicSave += SetCharacteristicSave;
            component.GetWH(width, height);
            component.GetFrame(textureBand);
            listComponets.Add(component);
            needUpdateComponents = true;
        }

        private bool CheckKeyBoard(Keys key)
        {
            return (keyboard.IsKeyDown(key) && !prevKeyboard.IsKeyDown(key));
        }

        public void LoadContent(ContentManager content)
        {
            Texture2D messageTexture = content.Load<Texture2D>("selected1");
            open = content.Load<Texture2D>("selected3");
            close = content.Load<Texture2D>("selected4");
            spriteFont = content.Load<SpriteFont>("font");
            textureBand = content.Load<Texture2D>("cell2");
            button.NewPosition(width - button.Width, widthButton + button.Height);
            button.NewTexture(open);
            button.NewSize(widthButton, widthButton);
            message.NewTexture(messageTexture);
            message.NewSize(indent, height);
            message.NewPosition(width - message.Width, 0);
            message.Visible = false;
        }

        public void GetWH(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void ActivateField(int widthCell)
        {
            activateField = !activateField;
            this.widthCell = widthCell;
            foreach (Component component in listComponets)
            {
                component.ActivateField(widthCell);
            }
        }

        public void Close()
        {
            activateInfo = true;
            if (isVisible == true)
            {
                button.NewPosition(width - button.Width, widthButton + button.Height);
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
            activateInfo = false;
            if (disActivate == true)
            {
                button.NewPosition(width - button.Width, widthButton + button.Height);
                button.NewSize(widthButton, widthButton);
                button.NewTexture(open);
                message.NewSize(indent, height);
                message.NewPosition(width - message.Width, 0);
                isVisible = true;
                message.Visible = false;
                disActivate = false;
            }
        }

        public void unIsVisible()
        {
            if (isVisible == true)
            {
                isVisible = !isVisible;
                button.NewPosition(width - button.Width, widthButton + button.Height);
                button.NewSize(widthButton, widthButton);
                message.NewSize(indent, height);
                message.NewPosition(width - message.Width, 0);
                button.Visible = false;
            }
            else
            {
                if (disActivate == false)
                {
                    isVisible = !isVisible;
                    button.NewPosition(width - button.Width, widthButton + button.Height);
                    button.NewSize(widthButton, widthButton);
                    message.NewSize(indent, height);
                    message.NewPosition(width - message.Width, 0);
                    button.Visible = false;
                }
            }
        }

        public void Update(GameTime gameTime, MouseState mouse, Rectangle mouseBoundingBox, KeyboardState keyboard, KeyboardState prevKeyboard, GraphicsDevice GraphicsDevice)
        {
            this.keyboard = keyboard;
            this.prevKeyboard = prevKeyboard;
            this.GraphicsDevice = GraphicsDevice;
            if (isVisible == true)
            {
                button.Update(gameTime);
                button.UpdateBoundingBox(gameTime);
                message.UpdateBoundingBox(gameTime);

                if (CheckKeyBoard(Keys.CapsLock))
                {
                    if (button.Visible == false)
                    {
                        button.NewPosition(width - button.Width - message.Width, 20 + button.Height);
                        button.NewTexture(close);
                        message.Visible = true;
                        Closed();
                    }
                    else
                    {
                        button.NewPosition(width - button.Width, 20 + button.Height);
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
                        button.NewPosition(width - button.Width - message.Width, 20 + button.Height);
                        button.NewTexture(close);
                        message.Visible = true;
                        Closed();
                    }
                    else
                    {
                        button.NewPosition(width - button.Width, 20 + button.Height);
                        button.NewTexture(open);
                        message.Visible = false;
                        Opened();
                    }
                    button.Visible = !button.Visible;
                }

                if (message.Visible == true) { foreach (MenuComponent menuComponent in listMenuComponents) { try { if (menuComponent.IsVisible == false) { menuComponent.unIsVisible(); } } catch { } } }
                else { foreach (MenuComponent menuComponent in listMenuComponents) { try { if (menuComponent.IsVisible == true) { menuComponent.unIsVisible(); } } catch { } } }
            }

            foreach (MenuComponent menuComponent in listMenuComponents)
            {
                try { menuComponent.Update(gameTime, mouse, mouseBoundingBox, keyboard, prevKeyboard); } catch { }
            }

            activateComponent = false;
            int number = 0;
            if (numberUpdateComponent == 0)
            {
                foreach (Component component in listComponets)
                {
                    number++;
                    component.Update(gameTime, mouse, mouseBoundingBox, keyboard);
                    try { if (component.Activate == true) { numberUpdateComponent = number; } } catch { }
                }
                number = 0;
                foreach (Component component in listComponets)
                {
                    number++;
                    if (number != numberUpdateComponent) { component.DisActivate(); }
                }
            }
            number = 0;
            foreach (Component component in listComponets)
            {
                number++;
                if (numberUpdateComponent != 0) { if (number == numberUpdateComponent) { try { component.Update(gameTime, mouse, mouseBoundingBox, keyboard); if (component.Activate == false) { numberUpdateComponent = 0; } } catch { } } }
                try { if (component.Activate == false) { if (activateComponent != true) { activateComponent = false; } } else { if (activateComponent == false) { activateComponent = true; cheak = false; } } } catch { }
                component.GetInfo(activateInfo);
            }

            if (activateComponent == false)
            {
                if (cheak == false)
                {
                    cheak = true;
                    ClearInfo();
                }
            }

            if (listComponets.Count > 0) { if (CheckKeyBoard(Keys.Delete) ||  CheckKeyBoard(Keys.Escape)) { if (numberMenuComponent > 0) { try { listComponets.RemoveAt(numberUpdateComponent - 1); numberUpdateComponent = 0; } catch { } } } }


            UpdateComponents(gameTime, mouse, mouseBoundingBox, keyboard);
            scroll = mouse.ScrollWheelValue;
            if (scroll != prevScroll)
            {
                if(scroll >= 0)
                {
                    if (scroll > prevScroll)
                    {
                        foreach (MenuComponent menuComponent in listMenuComponents)
                        {
                            menuComponent.Scroll((scroll / 120) * -5);
                        }
                        indentY += (scroll / 120) * -5;
                    }
                    else
                    {
                        foreach (MenuComponent menuComponent in listMenuComponents)
                        {
                            menuComponent.Scroll((scroll / 120) * 5);
                        }
                        indentY -= (scroll / 120) * 5;
                    }
                }
                else
                {
                    if (scroll < prevScroll)
                    {
                        foreach (MenuComponent menuComponent in listMenuComponents)
                        {
                            menuComponent.Scroll((scroll / 120) * -5);
                        }
                        indentY += (scroll / 120) * -5;
                    }
                    else
                    {
                        foreach (MenuComponent menuComponent in listMenuComponents)
                        {
                            menuComponent.Scroll((scroll / 120) * 5);
                        }
                        indentY -= (scroll / 120) * 5;
                    }
                }
                prevScroll = scroll;
            }

            if (keyboard.IsKeyDown(Keys.Z)) { int numberMenu = 0; foreach (MenuComponent menuComponent in listMenuComponents) {
                if (numberMenu == 0) { menuComponent.NewCharacteristic(message.X + 5, 20, sizeMenuComponent, sizeMenuComponent); } else { menuComponent.NewCharacteristic(message.X + 5, numberMenu * sizeMenuComponent + 30, sizeMenuComponent, sizeMenuComponent); }  numberMenu++; }
                indentY = 0;
            }
        }

        public void SetCharacteristic(int x, int y, float width, float height, float rotation, string way, string name)
        {
            setCharacteristic(x, y, width, height, rotation, way, name);
        }

        public void SetCharacteristicSave(int x, int y, float width, float height, float rotation, string way, string name)
        {
            setCharacteristicSave(x, y, width, height, rotation, way, name);
        }

        public void SetCharacteristicSaveMenuComponents(int x, int y, float width, float height, float rotation, string way, string name)
        {
            setCharacteristicSaveMenuComponetns(x, y, width, height, rotation, way, name);
        }

        public void GetCharacteristic(int x, int y, float width, float height, float rotation, string way, string name)
        {
            foreach (Component component in listComponets)
            {
                if (component.Activate == true)
                {
                    component.GetNewCharacteristic(x, y, (int)width, (int)height, rotation, way, name);
                }
            }
        }

        public void Clear()
        {
            listMenuComponents.Clear();
            listComponets.Clear();
            numberMenuComponent = 0;
        }

        public void GetWay(string filePath, GraphicsDevice GraphicsDevice)
        {
            int numberMenu = 0;
            foreach (MenuComponent menuComponents in listMenuComponents)
            {
                if (numberMenu == 0) { menuComponents.NewCharacteristic(message.X + 5, 20, sizeMenuComponent, sizeMenuComponent); } else { menuComponents.NewCharacteristic(message.X + 5, numberMenu * sizeMenuComponent + 30, sizeMenuComponent, sizeMenuComponent); }
                numberMenu++;
            }
            indentY = 0;
            MenuComponent menuComponent = new MenuComponent();
            menuComponent.GetCharacteristic(filePath, GraphicsDevice);
            menuComponent.Action += CreateComponent;
            menuComponent.setCharacteristicSave += SetCharacteristicSaveMenuComponents;
            if (numberMenuComponent == 0) { menuComponent.NewCharacteristic(message.X + 5, numberMenuComponent * sizeMenuComponent + 20 + indentY, sizeMenuComponent, sizeMenuComponent); }
            else { menuComponent.NewCharacteristic(message.X + 5, numberMenuComponent * sizeMenuComponent + 30 + indentY, sizeMenuComponent, sizeMenuComponent); }
            menuComponent.GetSpriteFont(spriteFont);
            if (numberMenuComponent < 10)
            {
                switch (numberMenuComponent)
                {
                    case 0:
                    menuComponent.ActivateButton(Keys.D1);
                        break;
                    case 1:
                        menuComponent.ActivateButton(Keys.D2);
                        break;
                    case 2:
                        menuComponent.ActivateButton(Keys.D3);
                        break;
                    case 3:
                        menuComponent.ActivateButton(Keys.D4);
                        break;
                    case 4:
                        menuComponent.ActivateButton(Keys.D5);
                        break;
                    case 5:
                        menuComponent.ActivateButton(Keys.D6);
                        break;
                    case 6:
                        menuComponent.ActivateButton(Keys.D7);
                        break;
                    case 7:
                        menuComponent.ActivateButton(Keys.D8);
                        break;
                    case 8:
                        menuComponent.ActivateButton(Keys.D9);
                        break;
                    case 9:
                        menuComponent.ActivateButton(Keys.D0);
                        break;
                }
            }
            numberMenuComponent++;
            listMenuComponents.Add(menuComponent);
        }

        public void NewWH(int width, int height)
        {
            GetWH(width, height);
            foreach (Component component in listComponets)
            {
                component.GetWH(width, height);
            }
        }

        public void CreateOpenedComponents(int x, int y, float widthObject2, float heightObject2, float rotation, string way, string name)
        {
            Component component = new Component();
            component.GetOpenedCharacteristic(GraphicsDevice, x, y, (int)widthObject2, (int)heightObject2, rotation, way, name);
            component.setCharacteristic += SetCharacteristic;
            component.setCharacteristicSave += SetCharacteristicSave;
            component.GetWH(width, height);
            component.GetFrame(textureBand);
            listComponets.Add(component);
            needUpdateComponents = true;
        }

        public void CreateOpenedMenuComponents(int x, int y, float widthObject2, float heightObject2, float rotation, string way, string name)
        {
            MenuComponent menuComponent = new MenuComponent();
            menuComponent.GetOpenedCharacteristic(GraphicsDevice, x, y, widthObject2, heightObject2, rotation, way, name);
            menuComponent.Action += CreateComponent;
            menuComponent.setCharacteristicSave += SetCharacteristicSaveMenuComponents;
            if (numberMenuComponent == 0) { menuComponent.NewCharacteristic(message.X + 5, numberMenuComponent * sizeMenuComponent + 20, sizeMenuComponent, sizeMenuComponent); }
            else { menuComponent.NewCharacteristic(message.X + 5, numberMenuComponent * sizeMenuComponent + 30, sizeMenuComponent, sizeMenuComponent); }
            menuComponent.GetSpriteFont(spriteFont);
            if (numberMenuComponent < 10)
            {
                switch (numberMenuComponent)
                {
                    case 0:
                        menuComponent.ActivateButton(Keys.D1);
                        break;
                    case 1:
                        menuComponent.ActivateButton(Keys.D2);
                        break;
                    case 2:
                        menuComponent.ActivateButton(Keys.D3);
                        break;
                    case 3:
                        menuComponent.ActivateButton(Keys.D4);
                        break;
                    case 4:
                        menuComponent.ActivateButton(Keys.D5);
                        break;
                    case 5:
                        menuComponent.ActivateButton(Keys.D6);
                        break;
                    case 6:
                        menuComponent.ActivateButton(Keys.D7);
                        break;
                    case 7:
                        menuComponent.ActivateButton(Keys.D8);
                        break;
                    case 8:
                        menuComponent.ActivateButton(Keys.D9);
                        break;
                    case 9:
                        menuComponent.ActivateButton(Keys.D0);
                        break;
                }
            }
            numberMenuComponent++;
            listMenuComponents.Add(menuComponent);
        }

        public void Save()
        {
            foreach (Component component in listComponets)
            {
                component.SetCharacteristicSave();
            }
            end();
        }

        public void SaveMenuComponents()
        {
            foreach (MenuComponent menuComponent in listMenuComponents)
            {
                menuComponent.SetCharacteristicSave();
            }
            endMenuComponents();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in listComponets) { component.Darw(spriteBatch); }

            if (isVisible == true)
            {
                message.Draw(spriteBatch);
                if (button.Visible == false)
                {
                    spriteBatch.Draw(button.Texture, button.BoundingBox, Color.White);
                }
                else
                {
                    button.Draw(spriteBatch);
                }

                if (message.Visible == true)
                {
                    foreach (MenuComponent menuComponent in listMenuComponents)
                    {
                        if(menuComponent.Y > 20 - sizeMenuComponent) { if (menuComponent.Y < width) { menuComponent.Draw(spriteBatch); } }
                    }
                }
            }
        }

        #endregion Methods
    }
}
