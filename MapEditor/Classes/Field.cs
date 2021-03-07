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
    class Field
    {
        #region Создание
        private Texture2D texture;
        private Rectangle[,] cells;
        private int widthCell;
        private int heightCell;
        private int width;
        private int height;
        private int numberWideth;
        private int numberHeight;
        #endregion Создание

        #region Реализация

        public void LoadContent(GraphicsDevice GraphicsDevice)
        {
            texture = Texture2D.FromStream(GraphicsDevice, File.OpenRead(@"Content\cell.png"));
        }

        public void WidthHeight(int width, int height, int widthCell, int heightCell)
        {
            this.width = width;
            this.height = height;
            this.heightCell = heightCell;
            this.widthCell = widthCell;
            if (width % widthCell > 0)
            {
                if (height % heightCell > 0)
                {
                    cells = new Rectangle[(width / widthCell) + 1, (height / heightCell) + 1];
                    numberWideth = (width / widthCell) + 1;
                    numberHeight = (height / heightCell) + 1;
                }
                else
                {
                    cells = new Rectangle[(width / widthCell) + 1, height / heightCell];
                    numberWideth = (width / widthCell) + 1;
                    numberHeight = height / heightCell;
                }
            }
            else
            {
                if (height % heightCell > 0)
                {
                    cells = new Rectangle[width / widthCell, (height / heightCell) + 1];
                    numberWideth = width / widthCell;
                    numberHeight = (height / heightCell) + 1;
                }
                else
                {
                    cells = new Rectangle[width / widthCell, height / heightCell];
                    numberWideth = width / widthCell;
                    numberHeight = height / heightCell;
                }
            }

        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < numberWideth; i++)
            {
                for (int g = 0; g < numberHeight; g++)
                {
                    cells[i, g] = new Rectangle(widthCell * i, heightCell * g, widthCell, heightCell);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < numberWideth; i++)
            {
                for (int g = 0; g < numberHeight; g++)
                {
                    spriteBatch.Draw(texture, cells[i, g], Color.White);
                }
            }
        }

        #endregion Реализация
    }
}
