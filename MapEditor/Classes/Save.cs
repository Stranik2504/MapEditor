using System;
using System.IO;
using System.Xml.Serialization; //XML

namespace MapEditor.Classes
{
    class Save
    {
        #region Create
        private GameDate gameDateComponents = new GameDate();
        private GameDate gameDateMenuComponent = new GameDate();
        private bool fullSave = false;
        #endregion Create

        #region Methods

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
        }

        public void SaveDataComponents()
        {
            if (Directory.Exists(@"Save") == false) { Directory.CreateDirectory(@"Save"); }
            if (Directory.Exists(@"Save")) {
                FileStream saveFile = File.Open(@"Save\Save.sav", FileMode.Create);

                string[] waysComponents = gameDateComponents.Way.ToArray();
                for (int i = 0; i < waysComponents.Length; i++)
                {
                    string name = waysComponents[i];
                    while (name.IndexOf(@"\") > -1)
                    {
                        name = name.Remove(0, name.IndexOf(@"\") + 1);
                    }
                    waysComponents[i] = @"Save\" + name;
                }
                gameDateComponents.Way.Clear();

                for (int i = 0; i < waysComponents.Length; i++)
                {
                    string way = waysComponents[i];
                    gameDateComponents.Way.Add(way);
                }

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameDate));
                xmlSerializer.Serialize(saveFile, gameDateComponents);
                saveFile.Close();
                if (File.Exists(@"Save\ExampleGameData.txt") == true) { File.Copy(@"ExampleGameData.txt", @"Save\ExampleGameData.txt", true); }
                else { File.Copy(@"ExampleGameData.txt", @"Save\ExampleGameData.txt", false); }
                if(Directory.Exists(@"Save\MapData") == false) { Directory.CreateDirectory(@"Save\MapData"); }
            }
        }

        public void SaveDataMenuComponents()
        {
            if (Directory.Exists(@"Save") == false) { Directory.CreateDirectory(@"Save"); }

            if (Directory.Exists(@"Save"))
            {
                foreach (string ways in gameDateMenuComponent.Way)
                {
                    string name = ways;
                    while (name.IndexOf(@"\") > -1)
                    {
                        name = name.Remove(0, name.IndexOf(@"\") + 1);
                    }
                    name = @"Save\" + name;
                    if (File.Exists(name) == true) { File.Copy(ways, name, true); } else { File.Copy(ways, name, false); }
                }
                if (Directory.Exists(@"Save\MapData") == false)
                {
                    Directory.CreateDirectory(@"Save\MapData");
                    FileStream saveFile = File.Open(@"Save\MapData\MenuComponents.sav", FileMode.Create);

                    string[] waysMenuComponents = gameDateMenuComponent.Way.ToArray();
                    for (int i = 0; i < waysMenuComponents.Length; i++)
                    {
                        string name = waysMenuComponents[i];
                        while (name.IndexOf(@"\") > -1)
                        {
                            name = name.Remove(0, name.IndexOf(@"\") + 1);
                        }
                        waysMenuComponents[i] = @"Save\" + name;
                    }
                    gameDateMenuComponent.Way.Clear();

                    for (int i = 0; i < waysMenuComponents.Length; i++)
                    {
                        string way = waysMenuComponents[i];
                        gameDateMenuComponent.Way.Add(way);
                    }

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameDate));
                    xmlSerializer.Serialize(saveFile, gameDateMenuComponent);
                    saveFile.Close();
                }
                else
                {
                    FileStream saveFile = File.Open(@"Save\MapData\MenuComponents.sav", FileMode.Create);

                    string[] waysMenuComponents = gameDateMenuComponent.Way.ToArray();
                    for (int i = 0; i < waysMenuComponents.Length; i++)
                    {
                        string name = waysMenuComponents[i];
                        while (name.IndexOf(@"\") > -1)
                        {
                            name = name.Remove(0, name.IndexOf(@"\") + 1);
                        }
                        waysMenuComponents[i] = @"Save\" + name;
                    }
                    gameDateMenuComponent.Way.Clear();

                    for (int i = 0; i < waysMenuComponents.Length; i++)
                    {
                        string way = waysMenuComponents[i];
                        gameDateMenuComponent.Way.Add(way);
                    }

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameDate));
                    xmlSerializer.Serialize(saveFile, gameDateMenuComponent);
                    saveFile.Close();
                }
            }

            if (fullSave == true)
            {
                fullSave = false;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\";

                CopyDir("Save", path + "Save");
                if (Directory.Exists(path + @"Save") == true) { } else { Directory.CreateDirectory(path + "Save"); }


                if (Directory.Exists(path + "Save"))
                {
                    FileStream saveFile = File.Open(path + @"Save\Save.sav", FileMode.Create);

                    string ways = path;
                    string[] waysComponents = gameDateComponents.Way.ToArray();
                    gameDateComponents.Way.Clear();

                    for (int i = 0; i < waysComponents.Length; i++)
                    {
                        string way = ways + waysComponents[i];
                        gameDateComponents.Way.Add(way);
                    }

                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameDate));
                    xmlSerializer.Serialize(saveFile, gameDateComponents);
                    saveFile.Close();

                    if (File.Exists(path + @"Save\ExampleGameData.txt") == true) { File.Copy(@"ExampleGameData.txt", path + @"Save\ExampleGameData.txt", true); }
                    else { File.Copy(@"ExampleGameData.txt", path + @"Save\ExampleGameData.txt", false); }
                    if (Directory.Exists(@"Save\MapData") == false) { Directory.CreateDirectory(@"Save\MapData"); }


                    if (Directory.Exists(path + @"Save") == false) { Directory.CreateDirectory(path + @"Save"); }
                    if (Directory.Exists(path + @"Save"))
                    {
                        if (Directory.Exists(path + @"Save\MapData") == false)
                        {
                            Directory.CreateDirectory(path + @"Save\MapData");

                            FileStream saveFile2 = File.Open(path + @"Save\MapData\MenuComponents.sav", FileMode.Create);

                            string[] waysMenuComponents = gameDateMenuComponent.Way.ToArray();
                            gameDateMenuComponent.Way.Clear();

                            for (int i = 0; i < waysMenuComponents.Length; i++)
                            {
                                string way = ways + waysMenuComponents[i];
                                gameDateMenuComponent.Way.Add(way);
                            }

                            XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(GameDate));
                            xmlSerializer2.Serialize(saveFile2, gameDateMenuComponent);
                            saveFile2.Close();
                        }
                        else
                        {
                            FileStream saveFile2 = File.Open(path + @"Save\MapData\MenuComponents.sav", FileMode.Create);

                            string[] waysMenuComponents = gameDateMenuComponent.Way.ToArray();
                            gameDateMenuComponent.Way.Clear();

                            for (int i = 0; i < waysMenuComponents.Length; i++)
                            {
                                string way = ways + waysMenuComponents[i];
                                gameDateMenuComponent.Way.Add(way);
                            }

                            XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(GameDate));
                            xmlSerializer2.Serialize(saveFile2, gameDateMenuComponent);
                            saveFile2.Close();
                        }
                    }
                }
                System.Windows.Forms.MessageBox.Show("Complete");
            } else { System.Windows.Forms.MessageBox.Show("Complete"); }
        }

        public void Clear()
        {
            gameDateComponents.X.Clear();
            gameDateComponents.Y.Clear();
            gameDateComponents.WidthObject.Clear();
            gameDateComponents.HeightObject.Clear();
            gameDateComponents.Rotation.Clear();
            gameDateComponents.Way.Clear();
            gameDateComponents.Name.Clear();
            gameDateComponents.NumberComponent = 0;
            gameDateMenuComponent.X.Clear();
            gameDateMenuComponent.Y.Clear();
            gameDateMenuComponent.WidthObject.Clear();
            gameDateMenuComponent.HeightObject.Clear();
            gameDateMenuComponent.Rotation.Clear();
            gameDateMenuComponent.Way.Clear();
            gameDateMenuComponent.Name.Clear();
            gameDateMenuComponent.NumberComponent = 0;
        }

        public void GetCharacteristicComponents(int x, int y, float width, float height, float rotation, string way, string name)
        {
            gameDateComponents.X.Add(x);
            gameDateComponents.Y.Add(y);
            gameDateComponents.WidthObject.Add(width);
            gameDateComponents.HeightObject.Add(height);
            gameDateComponents.Rotation.Add(rotation);
            gameDateComponents.Way.Add(way);
            gameDateComponents.Name.Add(name);
            gameDateComponents.NumberComponent++;
        }

        public void GetCharacteristicMenuComponents(int x, int y, float width, float height, float rotation, string way, string name)
        {
            gameDateMenuComponent.X.Add(x);
            gameDateMenuComponent.Y.Add(y);
            gameDateMenuComponent.WidthObject.Add(width);
            gameDateMenuComponent.HeightObject.Add(height);
            gameDateMenuComponent.Rotation.Add(rotation);
            gameDateMenuComponent.Way.Add(way);
            gameDateMenuComponent.Name.Add(name);
            gameDateMenuComponent.NumberComponent++;
        }

        public void ActivateFullSave()
        {
            fullSave = true;
        }

        #endregion Methods
    }
}
