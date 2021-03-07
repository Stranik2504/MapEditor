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
    public struct GameDate2
    {
        private string listMenuComponentWay;

        public string ListMenuComponentWay { get { return listMenuComponentWay; } set { listMenuComponentWay = value; } }
    }
}
