using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MapEditor.Classes
{
    class Field
    {
        #region Create
        private Texture2D texture;
        private Texture2D texture2;
        private Rectangle[,] field;
        private Rectangle[,] field2;
        private int width;
        private int height;
        private int widthCell;
        private int row;
        private int col;
        private bool isVisible = false;
        #endregion Create

        #region Public fields
        public bool IsVisible { get { return isVisible; } }
        #endregion Public fields

        #region Methods

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("cell");
            texture2 = content.Load<Texture2D>("cell2");
        }

        public void GetWH(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void CreateField(int widthCell)
        {
            this.widthCell = widthCell;
            if (height % this.widthCell != 0) { row = height / this.widthCell + 1; } else { row = height / this.widthCell; }
            if (width % this.widthCell != 0) { col = width / this.widthCell + 1; } else { col = width / this.widthCell; }
            field = new Rectangle[row, col];
            field2 = new Rectangle[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int q = 0; q < col; q++)
                {
                    field[i, q] = new Rectangle(this.widthCell * q, this.widthCell * i, this.widthCell, this.widthCell);
                    field2[i, q] = new Rectangle(this.widthCell * q + 1, this.widthCell * i + 1, this.widthCell - 1, this.widthCell - 1);
                }
            }
        }

        public void unIsVisible()
        {
            isVisible = !isVisible;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible == true)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int q = 0; q < col; q++)
                    {
                        spriteBatch.Draw(texture, field[i, q], Color.White);
                        spriteBatch.Draw(texture2, field2[i, q], Color.White);
                    }
                }
            }
        }

        # endregion Methods
    }
}
