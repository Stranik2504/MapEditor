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
    public class GameDate22
    {
        private List<GameDate2> listGameDate = new List<GameDate2>();
        private GameDate gameDate = new GameDate();

        public List<GameDate2> ListGameDate { get { return listGameDate; } set { listGameDate = value; } }
        public GameDate GameDate { get { return gameDate; } set { gameDate = value; } }
    }
}
