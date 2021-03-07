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
    public class GameDate33
    {
        private List<GameDate3> listGameDate3 = new List<GameDate3>();

        public List<GameDate3> ListGameDate3 { get { return listGameDate3; } set { listGameDate3 = value; } }
    }
}
