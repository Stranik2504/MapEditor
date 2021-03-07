using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization; //XML

namespace MapEditor.Classes
{
    class Open
    {
        #region Create
        private GameDate gameDateComponents = new GameDate();
        private GameDate gameDateMenuComponent = new GameDate();
        private string filePathComponent;
        private string filePathMenuCompoent;
        public event SetCharacteristic setComponentsCharacteristic;
        public event SetCharacteristic setMenuComponentsCharacteristic;
        public event MyAction Clear;
        #endregion Create

        #region Methods

        public void OpenSave()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "sav (*.sav)|*.sav";
                openFileDialog.FilterIndex = 4;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        filePathComponent = openFileDialog.FileName;
                        if (Directory.Exists(@"Save") == false) { Directory.CreateDirectory(@"Save"); }
                        string name = filePathComponent;
                        while (name.IndexOf(@"\") > -1)
                        {
                            name = name.Remove(0, name.IndexOf(@"\") + 1);
                        }
                        int numberLendth = name.Length;
                        name = filePathComponent;
                        filePathMenuCompoent = filePathComponent.Remove(filePathComponent.Length - numberLendth) + @"MapData\MenuComponents.sav";
                        filePathComponent = name;

                        FileStream openFile = File.Open(filePathComponent, FileMode.Open);
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameDate));
                        gameDateComponents = (GameDate)xmlSerializer.Deserialize(openFile);
                        openFile.Close();

                        FileStream openFile2 = File.Open(filePathMenuCompoent, FileMode.Open);
                        XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(GameDate));
                        gameDateMenuComponent = (GameDate)xmlSerializer2.Deserialize(openFile2);
                        openFile2.Close();
                    }
                    catch { MessageBox.Show("Error"); }

                    try
                    {
                        if (Directory.Exists(@"Textures") == false) { Directory.CreateDirectory(@"Textures"); }
                        foreach (string ways in gameDateMenuComponent.Way)
                        {
                            string name = ways;
                            while (name.IndexOf(@"\") > -1)
                            {
                                name = name.Remove(0, name.IndexOf(@"\") + 1);
                            }
                            name = @"Textures\" + name;
                            if (File.Exists(name) == true) { File.Copy(ways, name, true); } else { File.Copy(ways, name, false); }
                        }
                    }
                    catch { MessageBox.Show("Error"); }
                    Clear();
                    try
                    {
                        string[] waysComponents = gameDateComponents.Way.ToArray();
                        string[] nameComponents = gameDateComponents.Name.ToArray();
                        int[] xComponents = gameDateComponents.X.ToArray();
                        int[] yComponents = gameDateComponents.Y.ToArray();
                        float[] widthObjectComponents = gameDateComponents.WidthObject.ToArray();
                        float[] heightObjectComponents = gameDateComponents.HeightObject.ToArray();
                        float[] rotationComponents = gameDateComponents.Rotation.ToArray();
                        for (int i = 0; i < gameDateComponents.NumberComponent; i++)
                        {
                            string name = waysComponents[i];
                            while (name.IndexOf(@"\") > -1)
                            {
                                name = name.Remove(0, name.IndexOf(@"\") + 1);
                            }
                            name = @"Textures\" + name;
                            setComponentsCharacteristic(xComponents[i], yComponents[i], widthObjectComponents[i], heightObjectComponents[i], rotationComponents[i], name, nameComponents[i]);
                        }

                        string[] waysMenuComponents = gameDateMenuComponent.Way.ToArray();
                        string[] nameMenuComponents = gameDateMenuComponent.Name.ToArray();
                        int[] xMenuComponents = gameDateMenuComponent.X.ToArray();
                        int[] yMenuComponents = gameDateMenuComponent.Y.ToArray();
                        float[] widthObjectMenuComponents = gameDateMenuComponent.WidthObject.ToArray();
                        float[] heightObjectMenuComponents = gameDateMenuComponent.HeightObject.ToArray();
                        float[] rotationMenuComponents = gameDateMenuComponent.Rotation.ToArray();

                        for (int i = 0; i < gameDateMenuComponent.NumberComponent; i++)
                        {
                            string name = waysMenuComponents[i];
                            while (name.IndexOf(@"\") > -1)
                            {
                                name = name.Remove(0, name.IndexOf(@"\") + 1);
                            }
                            name = @"Textures\" + name;
                            setMenuComponentsCharacteristic(xMenuComponents[i], yMenuComponents[i], widthObjectMenuComponents[i], heightObjectMenuComponents[i], rotationMenuComponents[i], name, nameMenuComponents[i]);
                        }

                        MessageBox.Show("Complete");
                    }
                    catch { MessageBox.Show("Error"); }
                }
                else {  }
            }
            catch {  }
        }

        #endregion Methods
    }
}
