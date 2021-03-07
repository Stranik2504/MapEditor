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
    public struct GameDate3
    {
        private string listName;
        private int listX;
        private int listY;
        private int listWidthObject;
        private int listHeightObject;
        private string listWay;

        public string ListName { get { return listName; } set { listName = value; } }
        public int ListX { get { return listX; } set { listX = value; } }
        public int ListY { get { return listY; } set { listY = value; } }
        public int ListWidthObject { get { return listWidthObject; } set { listWidthObject = value; } }
        public int ListHeightObject { get { return listHeightObject; } set { listHeightObject = value; } }
        public string ListWay { get { return listWay; } set { listWay = value; } }
    }
}
