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
    delegate void MyPressed(int number);

    class SaveLoad
    {
        #region Создание
        private Texture2D message1;
        private Texture2D b11;
        private Texture2D b12;
        private Texture2D message2;
        private Texture2D b21;
        private Texture2D b22;
        private Texture2D returned;
        private Rectangle b11BoundingBox;
        private Rectangle b12BoundingBox;
        private Rectangle b21BoundingBox;
        private Rectangle b22BoundingBox;
        private Rectangle returnedBoundingBox;
        private Rectangle mouseBoundingBox;
        private Vector2 position;
        private Vector2 positionB1;
        private Vector2 positionB2;
        private MouseState mouse;
        private int width;
        private int height;
        private int number;
        private int numberlistMenuComp;
        private int numberlistComp;
        private bool action;
        private bool saveOnDesktop;
        private bool[] pressed = new bool[2];
        private bool end;
        private bool rewrite;
        private GameDate gameDate = new GameDate();
        private List<MenuComponent> listMenuComponent = new List<MenuComponent>();
        private List<Component> listComponent = new List<Component>();
        private List<GameDate2> listGameDate2 = new List<GameDate2>();
        private List<GameDate3> listGameDate3 = new List<GameDate3>();
        public event MyPressed Presse;
        #endregion Создание

        #region Публичные поля
        public int Number { get { return number; } }
        public int NumberlistMenuComp { get { return numberlistMenuComp; } }
        public int NumberlistComp { get { return numberlistComp; } }
        public bool End { get { return end; } set { end = value; } }
        public bool Rewrite { get { return rewrite; } }
        public List<MenuComponent> ListMenuComponent { get { return listMenuComponent; } }
        public List<Component> ListComponent { get { return listComponent; } }
        #endregion Публичные поля

        #region Реализация

        public void WidthHeight(int Width, int Height)
        {
            width = Width;
            height = Height;
        }

        public void SaveOnDesktop(bool saveOnDesktop)
        {
            this.saveOnDesktop = saveOnDesktop;
        }

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            message1 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Message4.png"));
            b11 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\MessageB1.png"));
            b12 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\MessageB3.png"));
            message2 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Message5.png"));
            b21 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\MessageB4.png"));
            b22 = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\MessageB5.png"));
            returned = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\Closed2.png"));
            Presse = number => { pressed[number] = true; };
        }

        public void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            mouseBoundingBox = new Rectangle(mouse.X, mouse.Y, 0, 0);
            b11BoundingBox = new Rectangle((int)positionB1.X, (int)positionB1.Y, b11.Width, b11.Height);
            b12BoundingBox = new Rectangle((int)positionB2.X, (int)positionB2.Y, b12.Width, b12.Height);
            b21BoundingBox = new Rectangle((int)positionB1.X, (int)positionB1.Y, b21.Width, b21.Height);
            b22BoundingBox = new Rectangle((int)positionB2.X, (int)positionB2.Y, b22.Width, b22.Height);
            returnedBoundingBox = new Rectangle((int)position.X + message1.Width - 20, (int)position.Y, 20, 20);
            position = new Vector2(width / 2 - message1.Width / 2, height / 2 - message1.Height / 2);
            positionB1 = new Vector2(position.X + 10, position.Y + message1.Height - 10 - b11.Height);
            positionB2 = new Vector2(position.X + message1.Width - 10 - b12.Width, position.Y + message1.Height - 10 - b12.Height);
        }

        private void CopyDir(string FromDir, string ToDir)
        {
            Directory.CreateDirectory(ToDir);
            foreach (string s1 in Directory.GetFiles(FromDir))
            {
                string s2 = ToDir + "\\" + Path.GetFileName(s1);
                if (File.Exists(s2)) { } else { File.Copy(s1, s2); }
            }
            foreach (string s in Directory.GetDirectories(FromDir))
            {
                CopyDir(s, ToDir + "\\" + Path.GetFileName(s));
            }
            action = true;
        }

        public void Save(GameTime gameTime, List<Component> listComponent, List<MenuComponent> listMenuComponent, int maxNumber)
        {
            if (pressed[0] == true)
            {
                if (mouseBoundingBox.Intersects(b11BoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Save2(gameTime, listComponent, listMenuComponent, maxNumber);
                        pressed[0] = false;
                    }
                }

                if (mouseBoundingBox.Intersects(b12BoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        Save1(gameTime, listComponent, listMenuComponent, maxNumber);
                        pressed[0] = false;
                    }
                }

                if (mouseBoundingBox.Intersects(returnedBoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        pressed[0] = false;
                    }
                }
            }
        }

        public void Load(GameTime gameTime, List<Component> listComponent, List<MenuComponent> listMenuComponent, int maxNumber, GraphicsDevice GraphicsDevice, Rectangle Menu1BoundingBox, Rectangle textNewBoundingBox, Rectangle textOpenBoundingBox, int heightNumber)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (pressed[1] == true)
            {
                if (mouseBoundingBox.Intersects(b21BoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (Directory.Exists(path + @"\Save"))
                        {
                            try
                            {
                                CheakLoad(gameTime, listComponent, listMenuComponent, maxNumber, GraphicsDevice, Menu1BoundingBox, textNewBoundingBox, textOpenBoundingBox, heightNumber, path + @"\Save");
                            }
                            catch { }
                        }
                        pressed[1] = false;
                        end = true;
                    }
                }

                if (mouseBoundingBox.Intersects(b22BoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        if (Directory.Exists(@"Save"))
                        {
                            try
                            {
                                CheakLoad(gameTime, listComponent, listMenuComponent, maxNumber, GraphicsDevice, Menu1BoundingBox, textNewBoundingBox, textOpenBoundingBox, heightNumber, @"Save");
                            }
                            catch { }
                        }
                        pressed[1] = false;
                        end = true;
                    }
                }

                if (mouseBoundingBox.Intersects(returnedBoundingBox))
                {
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        pressed[1] = false;
                    }
                }
            }
        }

        private void Save1(GameTime gameTime, List<Component> listComponent, List<MenuComponent> listMenuComponent, int maxNumber)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            try
            {
                StreamWriter File1 = new StreamWriter(@"Save\Ways.txt");
                File1.WriteLine();
                File1.Close();
                StreamWriter File2 = new StreamWriter(@"Save\SaveMap.txt");
                File2.WriteLine();
                File2.Close();
                if (Directory.Exists(@"Save")) { } else { Directory.CreateDirectory(@"Save"); }
                StreamWriter File3 = new StreamWriter(@"Save\Ways.txt");
                File3.WriteLine(listMenuComponent.Count);
                File3.WriteLine(maxNumber);
                foreach (MenuComponent menuComponent in listMenuComponent)
                {
                    File3.WriteLine(menuComponent.Way);
                }
                File3.Close();
            }
            catch { }
            try
            {
                StreamWriter File1 = new StreamWriter(@"Save\SaveMap.txt");
                File1.WriteLine(listComponent.Count);
                foreach (Component comp in listComponent)
                {
                    comp.Save(File1, false, "");
                }
                File1.Close();
            }
            catch { }


            if (saveOnDesktop == true)
            {
                try
                {

                    CopyDir("Save", path + @"\Save");
                }
                catch { }

                StreamWriter File4 = new StreamWriter(path + @"\Save\Ways.txt");
                File4.WriteLine();
                File4.Close();
                StreamWriter File6 = new StreamWriter(path + @"\Save\SaveMap.txt");
                File6.WriteLine();
                File6.Close();
                try
                {
                    if (File.Exists(path + @"\Save")) { } else { Directory.CreateDirectory(path + @"\Save"); }
                    StreamWriter File3 = new StreamWriter(path + @"\Save\Ways.txt");
                    File3.WriteLine(listMenuComponent.Count);
                    File3.WriteLine(maxNumber);
                    foreach (MenuComponent menuComponent in listMenuComponent)
                    {
                        File3.WriteLine(path + @"\" + menuComponent.Way);
                    }
                    File3.Close();
                }
                catch { }
                try
                {
                    StreamWriter File1 = new StreamWriter(path + @"\Save\SaveMap.txt");
                    File1.WriteLine(listComponent.Count);
                    foreach (Component comp in listComponent)
                    {
                        comp.Save(File1, true, path + @"\");
                    }
                    File1.Close();
                }
                catch { }
            }
        }

        private void Save2(GameTime gameTime, List<Component> listComponent, List<MenuComponent> listMenuComponent, int maxNumber)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            try
            {
                StreamWriter File1 = new StreamWriter(@"Save\Ways.txt");
                File1.WriteLine();
                File1.Close();
                StreamWriter File3 = new StreamWriter(@"Save\SaveMap.txt");
                File3.WriteLine();
                File3.Close();
                listGameDate2.Clear();
                listGameDate3.Clear();
                action = false;
                if (Directory.Exists(@"Save")) { } else { Directory.CreateDirectory(@"Save"); }
                gameDate.ListMenuComponentCount = listMenuComponent.Count;
                gameDate.MaxNumber = maxNumber;
                gameDate.ListComponentCount = listComponent.Count;
                foreach (MenuComponent menuComponent in listMenuComponent)
                {
                    GameDate2 gameDate2 = new GameDate2();
                    gameDate2.ListMenuComponentWay = menuComponent.Way;
                    listGameDate2.Add(gameDate2);
                }
                if (Directory.Exists(@"Save"))
                {
                    GameDate22 gamedate22 = new GameDate22();
                    gamedate22.ListGameDate = listGameDate2;
                    gamedate22.GameDate = gameDate;
                    FileStream saveFile = File.Open(@"Save\Ways.txt", FileMode.Create);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameDate22));
                    xmlSerializer.Serialize(saveFile, gamedate22);
                    saveFile.Close();
                }

                foreach (Component component in listComponent)
                {
                    GameDate3 gameDate3 = new GameDate3();
                    component.Save2();
                    gameDate3.ListName = component.Name;
                    gameDate3.ListX = component.X;
                    gameDate3.ListY = component.Y;
                    gameDate3.ListWidthObject = component.WidthObject;
                    gameDate3.ListHeightObject = component.HeightObject;
                    gameDate3.ListWay = component.Ways;
                    listGameDate3.Add(gameDate3);
                }

                if (Directory.Exists(@"Save"))
                {
                    GameDate33 gameDate33 = new GameDate33();
                    gameDate33.ListGameDate3 = listGameDate3;
                    FileStream saveFile2 = File.Open(@"Save\SaveMap.txt", FileMode.Create);
                    XmlSerializer xmlSerializer3 = new XmlSerializer(typeof(GameDate33));
                    xmlSerializer3.Serialize(saveFile2, gameDate33);
                    saveFile2.Close();
                }
            }
            catch { }


            if (saveOnDesktop == true)
            {
                try
                {

                    CopyDir("Save", path + @"\Save");
                }
                catch { }

                try
                {
                    listGameDate2.Clear();
                    listGameDate3.Clear();
                    if (action == true)
                    {
                        StreamWriter File4 = new StreamWriter(path + @"\Save\Ways.txt");
                        File4.WriteLine();
                        File4.Close();
                        StreamWriter File6 = new StreamWriter(path + @"\Save\SaveMap.txt");
                        File6.WriteLine();
                        File6.Close();
                        if (File.Exists(path + @"\Save")) { Directory.Delete(path + @"\Save"); Directory.CreateDirectory(path + @"\Save"); } else { Directory.CreateDirectory(path + @"\Save"); }
                        gameDate.ListMenuComponentCount = listMenuComponent.Count;
                        gameDate.MaxNumber = maxNumber;
                        gameDate.ListComponentCount = listComponent.Count;
                        foreach (MenuComponent menuComponent in listMenuComponent)
                        {
                            GameDate2 gameDate2 = new GameDate2();
                            gameDate2.ListMenuComponentWay = path + @"\" + menuComponent.Way;
                            listGameDate2.Add(gameDate2);
                        }
                        if (Directory.Exists(path + @"\Save"))
                        {
                            GameDate22 gamedate22 = new GameDate22();
                            gamedate22.ListGameDate = listGameDate2;
                            gamedate22.GameDate = gameDate;
                            FileStream saveFile = File.Open(path + @"\Save\Ways.txt", FileMode.Create);
                            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameDate22));
                            xmlSerializer.Serialize(saveFile, gamedate22);
                            saveFile.Close();
                        }

                        foreach (Component component in listComponent)
                        {
                            GameDate3 gameDate3 = new GameDate3();
                            component.Save2();
                            gameDate3.ListName = component.Name;
                            gameDate3.ListX = component.X;
                            gameDate3.ListY = component.Y;
                            gameDate3.ListWidthObject = component.WidthObject;
                            gameDate3.ListHeightObject = component.HeightObject;
                            gameDate3.ListWay = path + @"\" + component.Ways;
                            listGameDate3.Add(gameDate3);
                        }

                        if (Directory.Exists(path + @"\Save"))
                        {
                            GameDate33 gameDate33 = new GameDate33();
                            gameDate33.ListGameDate3 = listGameDate3;
                            FileStream saveFile2 = File.Open(path + @"\Save\SaveMap.txt", FileMode.Create);
                            XmlSerializer xmlSerializer3 = new XmlSerializer(typeof(GameDate33));
                            xmlSerializer3.Serialize(saveFile2, gameDate33);
                            saveFile2.Close();
                        }
                        action = false;
                    }
                }
                catch { }
            }

        }

        private void Load1(GameTime gameTime, List<Component> listComponent1, List<MenuComponent> listMenuComponent1, int maxNumber, GraphicsDevice GraphicsDevice, Rectangle Menu1BoundingBox, Rectangle textNewBoundingBox, Rectangle textOpenBoundingBox, int heightNumber, string path)
        {
            this.listMenuComponent = listMenuComponent1;
            this.listComponent = listComponent1;
            listComponent.Clear();
            listMenuComponent.Clear();
            rewrite = false;
            number = 0;
            numberlistMenuComp = 0;
            try
            {
                StreamReader File3 = new StreamReader(path + @"\Ways.txt");
                numberlistMenuComp = Convert.ToInt32(File3.ReadLine());
                maxNumber = Convert.ToInt32(File3.ReadLine());
                for (int i = 0; i < numberlistMenuComp; i++)
                {
                    if (number == maxNumber)
                    {
                        number = 0;
                        rewrite = true;
                    }
                    if (rewrite == true)
                    {
                        listMenuComponent.RemoveAt(number);
                    }
                    MenuComponent menuComponent = new MenuComponent(File3.ReadLine(), number, Menu1BoundingBox.Width, textNewBoundingBox.Height + 10, width, height, textNewBoundingBox.Y + textNewBoundingBox.Height + 20 + (height - textOpenBoundingBox.Y), heightNumber);
                    listMenuComponent.Add(menuComponent);
                    number++;

                    foreach (MenuComponent menu in listMenuComponent)
                    {
                        menu.LoadContent(GraphicsDevice);
                    }
                }
                File3.Close();
            }
            catch { }
            try
            {
                StreamReader File2 = new StreamReader(path + @"\SaveMap.txt");
                numberlistComp = Convert.ToInt32(File2.ReadLine());
                for (int i = 0; i < numberlistComp; i++)
                {
                    Component component = new Component(new Vector2(0, 0), "", width, height);
                    listComponent.Add(component);
                }
                foreach (Component comp in listComponent)
                {
                    string name = File2.ReadLine();
                    int X = Convert.ToInt32(File2.ReadLine());
                    int Y = Convert.ToInt32(File2.ReadLine());
                    int widthObject = Convert.ToInt32(File2.ReadLine());
                    int heightObject = Convert.ToInt32(File2.ReadLine());
                    string way = File2.ReadLine();
                    comp.Load(name, X, Y, widthObject, heightObject, way);
                    comp.LoadContent(GraphicsDevice);
                }
                File2.Close();
            }
            catch { }
        }

        private void Load2(GameTime gameTime, List<Component> listComponent1, List<MenuComponent> listMenuComponent1, int maxNumber, GraphicsDevice GraphicsDevice, Rectangle Menu1BoundingBox, Rectangle textNewBoundingBox, Rectangle textOpenBoundingBox, int heightNumber, string path)
        {

            this.listMenuComponent = listMenuComponent1;
            this.listComponent = listComponent1;
            listComponent.Clear();
            listMenuComponent.Clear();
            rewrite = false;
            number = 0;
            numberlistMenuComp = 0;
            if (Directory.Exists(path))
            {
                listGameDate2.Clear();
                listGameDate3.Clear();
                FileStream openFile = File.Open(path + @"\Ways.txt", FileMode.Open);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameDate22));
                GameDate22 gameDate22 = new GameDate22();
                gameDate22 = (GameDate22)xmlSerializer.Deserialize(openFile);
                openFile.Close();
                gameDate = gameDate22.GameDate;

                numberlistMenuComp = gameDate.ListMenuComponentCount;
                int listComponentCount = gameDate.ListComponentCount;

                for (int i = 0; i < numberlistMenuComp + 1; i++)
                {
                    GameDate2 gameDate2 = new GameDate2();
                    listGameDate2.Add(gameDate2);
                }

                listGameDate2 = gameDate22.ListGameDate;

                try
                {
                    foreach (GameDate2 gameDate2 in listGameDate2)
                    {
                        if (number == maxNumber)
                        {
                            number = 0;
                            rewrite = true;
                        }
                        if (rewrite == true)
                        {
                            listMenuComponent.RemoveAt(number);
                        }
                        MenuComponent menuComponent = new MenuComponent(gameDate2.ListMenuComponentWay, number, Menu1BoundingBox.Width, textNewBoundingBox.Height + 10, width, height, textNewBoundingBox.Y + textNewBoundingBox.Height + 20 + (height - textOpenBoundingBox.Y), heightNumber);
                        listMenuComponent.Add(menuComponent);
                        number++;

                        foreach (MenuComponent menu in listMenuComponent)
                        {
                            menu.LoadContent(GraphicsDevice);
                        }
                    }
                }
                catch { }

                for (int i = 0; i < listComponentCount; i++)
                {
                    GameDate3 gameDate3 = new GameDate3();
                    listGameDate3.Add(gameDate3);
                }

                FileStream openFile2 = File.Open(path + @"\SaveMap.txt", FileMode.Open);
                XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(GameDate33));
                GameDate33 gameDate33 = new GameDate33();
                gameDate33 = (GameDate33)xmlSerializer2.Deserialize(openFile2);
                openFile2.Close();
                listGameDate3 = gameDate33.ListGameDate3;

                try
                {
                    numberlistComp = gameDate.ListComponentCount;
                    string[] name = new string[numberlistComp];
                    int[] X = new int[numberlistComp];
                    int[] Y = new int[numberlistComp];
                    int[] widthObject = new int[numberlistComp];
                    int[] heightObject = new int[numberlistComp];
                    string[] way = new string[numberlistComp];
                    int numb = 0;
                    foreach (GameDate3 gameDate3 in listGameDate3)
                    {
                        name[numb] = gameDate3.ListName;
                        X[numb] = gameDate3.ListX;
                        Y[numb] = gameDate3.ListY;
                        widthObject[numb] = gameDate3.ListWidthObject;
                        heightObject[numb] = gameDate3.ListHeightObject;
                        way[numb] = gameDate3.ListWay;
                        numb++;
                    }
                    numb = 0;
                    for (int i = 0; i < numberlistComp; i++)
                    {
                        Component component = new Component(new Vector2(0, 0), "", width, height);
                        listComponent.Add(component);
                    }
                    foreach (Component comp in listComponent)
                    {
                        comp.Load(name[numb], X[numb], Y[numb], widthObject[numb], heightObject[numb], way[numb]);
                        comp.LoadContent(GraphicsDevice);
                        numb++;
                    }
                }
                catch { }
            }
        }

        private void CheakLoad(GameTime gameTime, List<Component> listComponent1, List<MenuComponent> listMenuComponent1, int maxNumber, GraphicsDevice GraphicsDevice, Rectangle Menu1BoundingBox, Rectangle textNewBoundingBox, Rectangle textOpenBoundingBox, int heightNumber, string path)
        {
            StreamReader cheak = new StreamReader(path + @"\Ways.txt");
            string cheakstr = cheak.ReadLine();
            bool isLoad1 = false;
            cheak.Close();

            int a = cheakstr.IndexOf(">");
            if (a > 0)
            {
                isLoad1 = false;
            }
            else
            {
                isLoad1 = true;
            }

            if (isLoad1 == true)
            {
                Load1(gameTime, listComponent, listMenuComponent, maxNumber, GraphicsDevice, Menu1BoundingBox, textNewBoundingBox, textOpenBoundingBox, heightNumber, path);
            }
            else
            {
                Load2(gameTime, listComponent, listMenuComponent, maxNumber, GraphicsDevice, Menu1BoundingBox, textNewBoundingBox, textOpenBoundingBox, heightNumber, path);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (pressed[0] == true)
            {
                spriteBatch.Draw(message1, position, Color.White);
                spriteBatch.Draw(b11, b11BoundingBox, Color.White);
                spriteBatch.Draw(b12, b12BoundingBox, Color.White);
                spriteBatch.Draw(returned, returnedBoundingBox, Color.White);
            }

            if (pressed[1] == true)
            {
                spriteBatch.Draw(message2, position, Color.White);
                spriteBatch.Draw(b21, b11BoundingBox, Color.White);
                spriteBatch.Draw(b22, b12BoundingBox, Color.White);
                spriteBatch.Draw(returned, returnedBoundingBox, Color.White);
            }
        }

        public void Pressed(int number)
        {
            Presse(number);
        }

        #endregion Реализация
    }
}
