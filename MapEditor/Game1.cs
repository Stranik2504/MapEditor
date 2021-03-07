#region Using Statements
using System;
using MonoGame;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Diagnostics;
using MapEditor.Classes;
#endregion

namespace MapEditor
{
    delegate void MyAction(int numberComponent, List<Component> listComponent);

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Создание
        Texture2D textureOpen;
        Texture2D textureClose;
        Texture2D Menu1;
        Texture2D textSave;
        Texture2D textOpen;
        Texture2D textNew;
        Texture2D textCreate;
        Texture2D SaveOnDesktopOn;
        Texture2D SaveOnDesktopOff;
        Texture2D Message;
        Texture2D MessageRatio;
        Rectangle SaveOnDesktopBoundingBox;
        Rectangle Menu1BoundingBox;
        Rectangle Open;
        Rectangle Close;
        Rectangle mouseBoudingBox;
        Rectangle textNewBoundingBox;
        Rectangle textCreateBoundingBox;
        Rectangle textSaveBoundingBox;
        Rectangle textOpenBoundingBox;
        Vector2 position;
        MouseState mouse;
        KeyboardState keyboard;
        KeyboardState prevKeyboard;
        int Width;
        int Height;
        int needWidth;
        int needHeight;
        int indent = 300;
        int Timer = 60;
        int[] delay = new int[4];
        int number = 0;
        int heightNumber = 50;
        int maxNumber;
        int numberComponent;
        int numberlistMenuComp = 0;
        int numberlistComp;
        private int ratio = 1;
        private int[] ratios = { 5, 10, 15, 20 };
        private int numberRatio = 0;
        int CellsBoundingBoxHeight = 20;
        int stopSec = 20;
        float numberSec;
        bool bloaked = false;
        bool activated = false;
        bool rewrite = false;
        bool end = false;
        bool SaveOnDeskteop = false;
        bool Save = false;
        bool open = false;
        bool editOn = false;
        bool activate = false;
        Edit edit = new Edit();
        EditCells editCells = new EditCells();
        Field field = new Field();
        SaveLoad saveLoad = new SaveLoad();
        FullScreen Screen = new FullScreen();
        List<MenuComponent> listMenuComponent = new List<MenuComponent>();
        List<Component> listComponent = new List<Component>();
        MyAction action = (numberComponent, listComponent) => { if (numberComponent == -1) { } else { if (listComponent.Count > 0) { listComponent.RemoveAt(numberComponent); } } };
        #endregion Создание

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            try
            {
                StreamReader File1 = new StreamReader(@"Link.txt");
                needWidth = Convert.ToInt32(File1.ReadLine());
                needHeight = Convert.ToInt32(File1.ReadLine());
                File1.Close();
            }
            catch { }

            try
            {
                if (needWidth > 100)
                {
                    if (needHeight > 100)
                    {
                        if (needWidth < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                        {
                            if (needHeight < GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
                            {
                                graphics.PreferredBackBufferWidth = needWidth;
                                graphics.PreferredBackBufferHeight = needHeight;
                                Screen.Strat(false);
                                Screen.NormalScreen += WHScreen;
                                activate = false;
                            }
                            else
                            {
                                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                                Screen.Strat(true);
                                graphics.IsFullScreen = true;
                                Screen.NormalScreen += NormalScreen;
                                activate = true;
                            }
                        }
                        else
                        {
                            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                            Screen.Strat(true);
                            graphics.IsFullScreen = true;
                            Screen.NormalScreen += NormalScreen;
                            activate = true;
                        }
                    }
                    else
                    {
                        graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                        graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                        Screen.Strat(true);
                        graphics.IsFullScreen = true;
                        Screen.NormalScreen += NormalScreen;
                        activate = true;
                    }
                }
                else
                {
                    graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                    Screen.Strat(true);
                    graphics.IsFullScreen = true;
                    Screen.NormalScreen += NormalScreen;
                    activate = true;
                }
            }
            catch { }
            try
            {
                StreamWriter File2 = new StreamWriter(@"Link.txt");
                File2.Close();
            }
            catch { }

            Width = graphics.PreferredBackBufferWidth;
            Height = graphics.PreferredBackBufferHeight;
            IsMouseVisible = true;
            edit.WidthHeight(Width, Height);
            editCells.WidthHeight(Width, Height);
            saveLoad.WidthHeight(Width, Height);
            Screen.W(Width);
            for (int i = 0; i < 4; i++)
            {
                delay[i] = 60;
            }
            delay[2] = 30;

            edit.onEnd += CreateMenuComponent;
            editCells.onEnd += CreateField;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            textureOpen = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Closed1.png"));
            textureClose = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Closed2.png"));
            Menu1 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Menu.png"));
            textSave = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Menu2.png"));
            textOpen = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Menu3.png"));
            textNew = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Menu1.png"));
            textCreate = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Menu4.png"));
            SaveOnDesktopOn = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Cells2.png"));
            SaveOnDesktopOff = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Cells.png"));
            Message = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Message3.png"));

            edit.LoadContent(Content, GraphicsDevice);
            editCells.LoadContent(GraphicsDevice);
            field.LoadContent(GraphicsDevice);
            saveLoad.LoadContent(GraphicsDevice);
            Screen.LoadContnet(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F4))
            {
                Exit();
            }

            SaveLoad2(gameTime);
            EditUpdate(gameTime);
            EditCellsUpdate(gameTime);
            Screen.Update(gameTime);
            Ratio(gameTime, GraphicsDevice);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            {
                Draw(spriteBatch);
                if (editOn == true)
                {
                    edit.Draw(spriteBatch);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #region Реализация

        public void SaveLoad2(GameTime gameTime)
        {
            saveLoad.Update(gameTime);
            saveLoad.Save(gameTime, listComponent, listMenuComponent, maxNumber);
            saveLoad.Load(gameTime, listComponent, listMenuComponent, maxNumber, GraphicsDevice, Menu1BoundingBox, textNewBoundingBox, textOpenBoundingBox, heightNumber);
            if (saveLoad.End == true)
            {
                saveLoad.End = false;
                rewrite = saveLoad.Rewrite;
                number = saveLoad.Number;
                numberlistComp = saveLoad.NumberlistComp;
                numberlistMenuComp = saveLoad.NumberlistMenuComp;
                listComponent = saveLoad.ListComponent;
                listMenuComponent = saveLoad.ListMenuComponent;
            }
        }

        public void EditUpdate(GameTime gameTime)
        {
            editCells.Update(gameTime);

            if (editOn == true)
            {
                edit.Update(gameTime);
                editOn = edit.End;
            }
            else
            {
                UpdateMenu(gameTime);
                BlockedUpdate(gameTime);
            }
        }

        public void EditCellsUpdate(GameTime gameTime)
        {
            if (editCells.ActivateCells == true)
            {
                field.Update(gameTime);
            }
            else
            {
                end = false;
            }
        }

        public void Ratio(GameTime gameTime, GraphicsDevice GraphicsDevice)
        {
            keyboard = Keyboard.GetState();

            if (CheckKeyBoard(Keys.F))
            {
                if (numberRatio >= 4)
                {
                    numberRatio = 0;
                }
                ratio = ratios[numberRatio];
                MessageRatio = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\ratio" + (numberRatio + 1) + ".png"));
                numberRatio++;
                delay[3] = 0;
            }

            if (delay[3] <= Timer)
            {
                delay[3]++;
            }

            prevKeyboard = keyboard;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Save == true)
            {
                spriteBatch.Draw(Message, position, Color.White);
            }

            if (delay[3] <= Timer)
            {
                spriteBatch.Draw(MessageRatio, new Vector2(0, 0), Color.White);
            }

            if (editCells.ActivateCells == true)
            {
                if (end == true)
                {
                    field.Draw(spriteBatch);
                }
            }
            foreach (Component comp in listComponent)
            {
                comp.Draw(spriteBatch);
            }

            if (open == true)
            {
                spriteBatch.Draw(textureClose, Close, Color.White);
                spriteBatch.Draw(Menu1, Menu1BoundingBox, Color.White);
                spriteBatch.Draw(textNew, textNewBoundingBox, Color.White);
                spriteBatch.Draw(textSave, textSaveBoundingBox, Color.White);
                spriteBatch.Draw(textOpen, textOpenBoundingBox, Color.White);
                spriteBatch.Draw(textCreate, textCreateBoundingBox, Color.White);
                if (SaveOnDeskteop == true)
                {
                    spriteBatch.Draw(SaveOnDesktopOn, SaveOnDesktopBoundingBox, Color.White);
                }
                else
                {
                    spriteBatch.Draw(SaveOnDesktopOff, SaveOnDesktopBoundingBox, Color.White);
                }
                editCells.Draw(spriteBatch);


                foreach (MenuComponent menu in listMenuComponent)
                {
                    menu.Draw(spriteBatch);
                }
            }
            Screen.Draw(spriteBatch);
            saveLoad.Draw(spriteBatch);

            if (open == false)
            {
                spriteBatch.Draw(textureOpen, Open, Color.White);
            }
        }

        private void UpdateMenu(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            mouseBoudingBox = new Rectangle((int)mouse.Position.X, (int)mouse.Position.Y, 0, 0);
            Menu1BoundingBox = new Rectangle((int)Close.X + Close.Width, (int)Close.Y, indent, Height);
            maxNumber = (Height - (textNewBoundingBox.Y + textNewBoundingBox.Height + 20 + (Height - textOpenBoundingBox.Y)) - CellsBoundingBoxHeight - 10) / heightNumber;
            keyboard = Keyboard.GetState();
            UpdateComponent(gameTime);
        }

        private void BlockedUpdate(GameTime gameTime)
        {
            if (bloaked == false)
            {
                switch (open)
                {
                    case true:
                        OpenUpdate(gameTime);
                        break;

                    case false:
                        CloseUpdate(gameTime);
                        break;
                }

                if (mouseBoudingBox.Intersects(textNewBoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (delay[0] >= Timer)
                        {
                            delay[0] = 0;
                            edit.End = true;
                            editOn = true;
                        }
                    }
                }

                if (delay[0] <= Timer)
                {
                    delay[0]++;
                }
            }
        }

        private void SaveLoad(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            if (mouseBoudingBox.Intersects(textSaveBoundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    saveLoad.Pressed(0);
                }
            }

            if (mouseBoudingBox.Intersects(textOpenBoundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    saveLoad.Pressed(1);
                }
            }
        }

        private void OpenUpdate(GameTime gameTime)
        {
            Close = new Rectangle(Width - 20 - indent, 0, 20, 20);
            textNewBoundingBox = new Rectangle(Width - Menu1BoundingBox.Width + ((Menu1BoundingBox.Width - textNew.Width) / 2), 10, 120, 20);
            textCreateBoundingBox = new Rectangle(Width - textCreateBoundingBox.Width - 10, textSaveBoundingBox.Y - 10 - textCreate.Height, 120, 20);
            textSaveBoundingBox = new Rectangle(Width - Menu1BoundingBox.Width + 10, Height - 10 - textSave.Height, 120, 20);
            textOpenBoundingBox = new Rectangle(Width - textOpen.Width - 10, Height - 10 - textOpen.Height, 120, 20);
            editCells.XY(textSaveBoundingBox.X, textSaveBoundingBox.Y);
            editCells.Active(true);
            SaveOnDesktopUpdate();

            foreach (MenuComponent menu in listMenuComponent)
            {
                menu.Update(gameTime, menu);
            }

            if (mouseBoudingBox.Intersects(Close))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    open = false;
                }
            }

            SaveLoad(gameTime);
            Clear();
        }

        private void CloseUpdate(GameTime gameTime)
        {
            Open = new Rectangle(Width - 20, 0, 20, 20);

            if (mouseBoudingBox.Intersects(Open))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    open = true;
                }
            }

            editCells.Active(false);
        }

        private void Clear()
        {
            mouse = Mouse.GetState();
            if (mouseBoudingBox.Intersects(textCreateBoundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    listComponent.Clear();
                    listMenuComponent.Clear();
                    rewrite = false;
                    number = 0;
                    numberlistMenuComp = 0;
                }
            }
        }

        private void UpdateComponent(GameTime gameTime)
        {
            foreach (Component comp in listComponent)
            {
                if (numberComponent == -1)
                {
                    if (comp.Activate == true)
                    {
                        numberComponent = listComponent.IndexOf(comp);
                    }
                    else
                    {
                        comp.Cheak(gameTime);
                        comp.ActiveField(editCells.ActivateCells, editCells.CellsWidth);
                        numberComponent = -1;
                        activated = false;
                    }
                }

                if (numberComponent == listComponent.IndexOf(comp))
                {
                    comp.Ration = ratio;
                    activated = true;
                    comp.Update(gameTime);
                    comp.Cheak(gameTime);
                    if (comp.Go == true)
                    {
                        bloaked = true;
                    }
                    else
                    {
                        bloaked = false;
                    }

                    if (comp.Activate == true)
                    {
                        numberComponent = listComponent.IndexOf(comp);
                    }
                    else
                    {
                        numberComponent = -1;
                        activated = false;
                    }
                }
            }

            try
            {
                if (activated == true)
                {
                    if (keyboard.IsKeyDown(Keys.Delete))
                    {
                        action(numberComponent, listComponent);
                    }

                    if (keyboard.IsKeyDown(Keys.Back))
                    {
                        action(numberComponent, listComponent);
                    }
                }
            }
            catch { }
        }

        private void SaveOnDesktopUpdate()
        {
            SaveOnDesktopBoundingBox = new Rectangle(textSaveBoundingBox.X + 30, textSaveBoundingBox.Y - 10 - SaveOnDesktopOn.Height, 20, 20);
            if (mouseBoudingBox.Intersects(SaveOnDesktopBoundingBox))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (delay[2] >= Timer / 2)
                    {
                        delay[2] = 0;
                        if (SaveOnDeskteop == true)
                        {
                            SaveOnDeskteop = false;
                            Save = false;
                        }
                        else
                        {
                            SaveOnDeskteop = true;
                            numberSec = (float)Message.Height / (float)(Timer - stopSec);
                            position.X = Width / 2 - Message.Width / 2;
                            position.Y = 0 - Message.Height;
                            Save = true;
                            Debug.WriteLine(numberSec);
                            Debug.WriteLine(position.Y);
                        }
                    }
                }
            }

            if (delay[2] <= Timer)
            {
                delay[2]++;
                if (delay[2] < Timer - stopSec)
                {
                    position.Y += numberSec;
                }
            }

            if (delay[2] >= Timer)
            {
                Save = false;
            }

            saveLoad.SaveOnDesktop(SaveOnDeskteop);
        }

        private void CreateMenuComponent()
        {
            if (number == maxNumber) { if (maxNumber == 0) { } else { number = 0; rewrite = true; } }

            if (rewrite == true) { listMenuComponent.RemoveAt(number); }

            MenuComponent menuComponent = new MenuComponent(edit.Way, number, Menu1BoundingBox.Width, textNewBoundingBox.Height + 10, Width, Height, textNewBoundingBox.Y + textNewBoundingBox.Height + 20 + (Height - textOpenBoundingBox.Y) - CellsBoundingBoxHeight - 10, heightNumber);
            menuComponent.onGo += CreateComponent;
            listMenuComponent.Add(menuComponent);
            number++;

            foreach (MenuComponent menu in listMenuComponent) { menu.LoadContent(GraphicsDevice); }
        }

        private void CreateComponent(GameTime gameTime, MenuComponent menu)
        {
            Component component = new Component(new Vector2(0, 0), menu.Way, Width, Height);
            listComponent.Add(component);

            foreach (Component comp in listComponent)
            {
                comp.LoadContent(GraphicsDevice);
                comp.Update(gameTime);
            }
        }

        private void CreateField()
        {
            field.WidthHeight(Width, Height, editCells.CellsWidth, editCells.CellsWidth);
            end = true;
        }

        private void NormalScreen()
        {
            if (activate == false)
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.ToggleFullScreen();
                Width = graphics.PreferredBackBufferWidth;
                Height = graphics.PreferredBackBufferHeight;
                edit.WidthHeight(Width, Height);
                editCells.WidthHeight(Width, Height);
                if (editCells.ActivateCells == true)
                {
                    if (end == true)
                    {
                        CreateField();
                    }
                }
                saveLoad.WidthHeight(Width, Height);
                activate = true;
            }
            else
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 70;
                graphics.ToggleFullScreen();
                Width = graphics.PreferredBackBufferWidth;
                Height = graphics.PreferredBackBufferHeight;
                edit.WidthHeight(Width, Height);
                editCells.WidthHeight(Width, Height);
                saveLoad.WidthHeight(Width, Height);
                if (editCells.ActivateCells == true)
                {
                    if (end == true)
                    {
                        CreateField();
                    }
                }
                activate = false;
            }
        }

        private void WHScreen()
        {
            if (activate == false)
            {
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                graphics.ToggleFullScreen();
                Width = graphics.PreferredBackBufferWidth;
                Height = graphics.PreferredBackBufferHeight;
                edit.WidthHeight(Width, Height);
                editCells.WidthHeight(Width, Height);
                saveLoad.WidthHeight(Width, Height);
                if (editCells.ActivateCells == true)
                {
                    if (end == true)
                    {
                        CreateField();
                    }
                }
                activate = true;
            }
            else
            {
                graphics.PreferredBackBufferWidth = needWidth;
                graphics.PreferredBackBufferHeight = needHeight;
                graphics.ToggleFullScreen();
                Width = graphics.PreferredBackBufferWidth;
                Height = graphics.PreferredBackBufferHeight;
                edit.WidthHeight(Width, Height);
                editCells.WidthHeight(Width, Height);
                saveLoad.WidthHeight(Width, Height);
                if (editCells.ActivateCells == true)
                {
                    if (end == true)
                    {
                        CreateField();
                    }
                }
                activate = false;
            }
        }

        private bool CheckKeyBoard(Keys key)
        {
            return (keyboard.IsKeyDown(key) && !prevKeyboard.IsKeyDown(key));
        }

        #endregion Реализация

    }
}
