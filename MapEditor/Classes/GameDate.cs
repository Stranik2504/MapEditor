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
    public struct GameDate
    {
        private int listMenuComponentCount;
        private int maxNumber;
        private int listComponentCount;

        public int MaxNumber { get { return maxNumber; } set { maxNumber = value; } }
        public int ListMenuComponentCount { get { return listMenuComponentCount; } set { listMenuComponentCount = value; } }
        public int ListComponentCount { get { return listComponentCount; } set { listComponentCount = value; } }
    }
}
