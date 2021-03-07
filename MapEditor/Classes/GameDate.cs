using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MapEditor.Classes
{
    public class GameDate
    {
        #region Create
        private int numberComponent = new int();
        private List<int> x = new List<int>();
        private List<int> y = new List<int>();
        private List<float> widthObject = new List<float>();
        private List<float> heightObject = new List<float>();
        private List<float> rotation = new List<float>();
        private List<string> name = new List<string>();
        private List<string> way = new List<string>();
        #endregion Create

        #region Public fields
        public int NumberComponent { get { return numberComponent; } set { numberComponent = value; } }
        public List<int> X { get { return x; } set { x = value; } }
        public List<int> Y { get { return y; } set { y = value; } }
        public List<float> WidthObject { get { return widthObject; } set { widthObject = value; } }
        public List<float> HeightObject { get { return heightObject; } set { heightObject = value; } }
        public List<float> Rotation { get { return rotation; } set { rotation = value; } }
        public List<string> Name { get { return name; } set { name = value; } }
        public List<string> Way { get { return way; } set { way = value; } }
        #endregion Public fields
    }
}
