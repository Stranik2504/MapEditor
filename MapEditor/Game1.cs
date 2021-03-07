using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MapEditor.Classes;
using System.Collections.Generic;
using ClassMonogame;

namespace MapEditor
{
    public delegate void MyAction();
    public delegate void EditAction(int widthCell);

    public class Game1 : Game
    {
        #region Create
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle mouseBoundingBox;
        KeyboardState keyboard;
        KeyboardState prevKeyboard;
        MouseState mouse;
        int Height;
        int Width;
        int needWidth;
        int needHeight;
        public bool end = false;
        public bool fullScreen = false;
        MenuUp menu = new MenuUp();
        Editor editor = new Editor();
        Components components = new Components();
        Info info = new Info();
        Field field = new Field();
        Open open = new Open();
        Save save = new Save();
        public event EditAction editField;
        public event MyAction editForm2;
        #endregion Create

        public Game1(int width, int height, bool FullScreen)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.Title = "MapEditor";
            needWidth = width;
            needHeight = height;

            if (FullScreen == true)
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.IsFullScreen = true;
                fullScreen = true;
            }
            else
            {
                graphics.PreferredBackBufferWidth = width;
                graphics.PreferredBackBufferHeight = height;
                Window.Position = new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - graphics.PreferredBackBufferWidth) / 2, (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - graphics.PreferredBackBufferHeight) / 2);
            }
            IsMouseVisible = true;
            Width = graphics.PreferredBackBufferWidth;
            Height = graphics.PreferredBackBufferHeight;
            menu.GetWH(Width, Height);
            components.GetWH(Width, Height);
            editor.GetWH(Width, Height);
            info.GetWH(Width, Height);
            field.GetWH(Width, Height);

            #region подключение event
            editor.filePathGet += components.GetWay;
            info.Closed += components.Close;
            components.Closed += info.Close;
            info.Opened += components.Open;
            components.Opened += info.Open;
            components.setCharacteristic += info.GetCharacteristic;
            info.setCharacteristic += components.GetCharacteristic;
            components.ClearInfo += info.Clear;
            editField += field.CreateField;
            components.setCharacteristicSave += save.GetCharacteristicComponents;
            components.end += save.SaveDataComponents;
            components.setCharacteristicSaveMenuComponetns += save.GetCharacteristicMenuComponents;
            components.endMenuComponents += save.SaveDataMenuComponents;
            open.setComponentsCharacteristic += components.CreateOpenedComponents;
            open.setMenuComponentsCharacteristic += components.CreateOpenedMenuComponents;
            open.Clear += components.Clear;
            #endregion подключение event 
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            menu.LoadContent(Content);
            editor.LoadContent(Content);
            components.LoadContent(Content);
            info.LoadContent(Content);
            field.LoadContent(Content);
            ConnectMenuEvent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (System.Windows.Forms.MessageBox.Show("You definitely want to close the project?", "Close project", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Exit();
                }   
            }

            if (keyboard.IsKeyDown(Keys.LeftAlt))
            {
                if (CheckKeyBoard(Keys.F4))
                {
                    if (System.Windows.Forms.MessageBox.Show("You definitely want to close the project?", "Close project", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Exit();
                    }
                }
            }

            if (keyboard.IsKeyDown(Keys.RightAlt))
            {
                if (CheckKeyBoard(Keys.F4))
                {
                    if (System.Windows.Forms.MessageBox.Show("You definitely want to close the project?", "Close project", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Exit();
                    }
                }
            }

            menu.Update(gameTime);
            editor.Update(gameTime, GraphicsDevice);
            components.Update(gameTime, mouse, mouseBoundingBox, keyboard, prevKeyboard, GraphicsDevice);
            info.Update(gameTime, mouse, mouseBoundingBox, keyboard, prevKeyboard);
            UpdateMenu(gameTime);
            FullScreen();

            #region UpdateComponent
            mouse = Mouse.GetState();
            mouseBoundingBox = new Rectangle(mouse.X, mouse.Y, 0, 0);
            prevKeyboard = keyboard;
            #endregion UpdateComponent           

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            {
                field.Draw(spriteBatch);
                components.Draw(spriteBatch);
                info.Draw(spriteBatch);
                menu.Draw(spriteBatch);
                editor.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Реализация

        #region MenuUpdate

        public void CreateProject()
        {
            if (System.Windows.Forms.MessageBox.Show("You definitely want to create a new project?", "Create project", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                end = true;
                Exit();
            }
        }

        public void OpenProject()
        {
            if (System.Windows.Forms.MessageBox.Show("You definitely want to open the project?", "Open project", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                open.OpenSave();
            }  
        }

        public void FullSave()
        {
            save.Clear();
            save.ActivateFullSave();
            components.Save();
            components.SaveMenuComponents();
        }

        public void Save()
        {
            save.Clear();
            components.Save();
            components.SaveMenuComponents();
        }

        public void CreateMenuComponent()
        {
            editor.CreateComponent();
        }

        public void Field()
        {
            if (field.IsVisible == false) { editForm2(); } else { field.unIsVisible(); }
        }

        public void Clear()
        {
            if (System.Windows.Forms.MessageBox.Show("You definitely want to clean up the project?", "Clear project", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                components.Clear();
                info.Clear();
            }
        }

        public void MenuComponets()
        {
            components.unIsVisible();
        }

        public void Info()
        {
            info.unIsVisible();
        }

        public void Null()
        {
            
        }

        private void UpdateMenu(GameTime gameTime)
        {
            if (keyboard.IsKeyDown(Keys.RightControl) || keyboard.IsKeyDown(Keys.LeftControl))
            {
                if (keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift))
                {
                    if (CheckKeyBoard(Keys.N))
                    {
                        CreateProject();
                    }

                    if (CheckKeyBoard(Keys.S))
                    {
                        FullSave();
                    }

                    if (CheckKeyBoard(Keys.C))
                    {
                        MenuComponets();
                    }
                }
                else
                {
                    if (CheckKeyBoard(Keys.N))
                    {
                        CreateMenuComponent();
                    }

                    if (CheckKeyBoard(Keys.O))
                    {
                        OpenProject();
                    }

                    if (CheckKeyBoard(Keys.S))
                    {
                        Save();
                    }

                    if (CheckKeyBoard(Keys.F))
                    {
                        Field();
                    }

                    if (CheckKeyBoard(Keys.C))
                    {
                        Clear();
                    }

                    if (CheckKeyBoard(Keys.I))
                    {
                        Info();
                    }
                }
            }
        }

        private void ConnectMenuEvent()
        {
            menu.Columns[0].Components[0].Activated += Null;
            menu.Columns[0].Components[1].Activated += CreateProject;
            menu.Columns[0].Components[2].Activated += OpenProject;
            menu.Columns[0].Components[3].Activated += FullSave;
            menu.Columns[0].Components[4].Activated += Save;
            menu.Columns[1].Components[0].Activated += Null;
            menu.Columns[1].Components[1].Activated += CreateMenuComponent;
            menu.Columns[1].Components[2].Activated += Field;
            menu.Columns[1].Components[3].Activated += Clear;
            menu.Columns[2].Components[0].Activated += Null;
            menu.Columns[2].Components[1].Activated += MenuComponets;
            menu.Columns[2].Components[2].Activated += Info;
        }

        #endregion MenuUpdate

        private bool CheckKeyBoard(Keys key)
        {
            return (keyboard.IsKeyDown(key) && !prevKeyboard.IsKeyDown(key));
        }

        public void EditField(int widthCell)
        {
            editField(widthCell);
            field.unIsVisible();
        }

        private void FullScreen()
        {
            if(CheckKeyBoard(Keys.F11) || CheckKeyBoard(Keys.Escape))
            {
                if (fullScreen == true)
                {
                    graphics.PreferredBackBufferWidth = needWidth;
                    graphics.PreferredBackBufferHeight = needHeight;
                    graphics.ToggleFullScreen();
                    fullScreen = false;
                }
                else
                {
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    graphics.ToggleFullScreen();
                    fullScreen = true;
                }
                Width = graphics.PreferredBackBufferWidth;
                Height = graphics.PreferredBackBufferHeight;
                menu.GetWH(Width, Height);
                components.NewWH(Width, Height);
                editor.GetWH(Width, Height);
                info.GetWH(Width, Height);
                field.GetWH(Width, Height);
            }
        }

        #endregion Реализация
    }
}
