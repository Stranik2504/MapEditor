using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Menu;
using ClassMonogame;
using System.IO;
using System.Collections.Generic;

namespace MapEditor.Classes
{
    class MenuUp
    {
        #region Create
        private Texture2D passive;
        private Texture2D active;
        private SpriteFont spriteFont;
        private Column[] columns;
        private Menu.Menu menu;
        private Message message;
        #endregion Create

        #region Public fields
        public Column[] Columns { get { return columns; } }
        #endregion Public fields

        #region Methods

        public MenuUp()
        {
            columns = new Column[3];
            menu = new Menu.Menu(3);
            message = new Message(0, 0);
        }

        public void GetWH(int width, int height)
        {
            message.NewSize(width, 20);
        }

        public void LoadContent(ContentManager content)
        {           
            active = content.Load<Texture2D>("selected2");
            passive = content.Load<Texture2D>("selected1");
            spriteFont = content.Load<SpriteFont>("font");
            message.NewTexture(passive);
            columns[0] = new Column(5, 0, 0, 160, 20, Color.LightGray, active, passive, spriteFont);
            columns[0].Components[0].Lable.Width = 50;
            columns[0].Components[0].Lable.GetText("File");
            columns[0].Components[1].Lable.GetText("Create Ctrl+Shift+N");
            columns[0].Components[2].Lable.GetText("Open Ctrl+O");
            columns[0].Components[3].Lable.GetText("Full Save Ctrl+Shift+S");
            columns[0].Components[4].Lable.GetText("Save on .exe Ctrl+S");
            columns[1] = new Column(4, (int)columns[0].Components[0].Lable.X + columns[0].Components[0].Lable.Width, 0, 220, 20, Color.LightGray, active, passive, spriteFont);
            columns[1].Components[0].Lable.Width = 50;
            columns[1].Components[0].Lable.GetText("Edit");
            columns[1].Components[1].Lable.GetText("New Menu Componetnt Ctrl+N");
            columns[1].Components[2].Lable.GetText("Field Ctrl+F");
            columns[1].Components[3].Lable.GetText("Clear Ctrl+C");
            columns[2] = new Column(3, (int)columns[1].Components[0].Lable.X + columns[1].Components[0].Lable.Width, 0, 180, 20, Color.LightGray, active, passive, spriteFont);
            columns[2].Components[0].Lable.Width = 50;
            columns[2].Components[0].Lable.GetText("View");
            columns[2].Components[1].Lable.GetText("Component Ctrl+Shift+C");
            columns[2].Components[2].Lable.GetText("Info Ctrl+I");
            menu.GetColumns(columns);
        }

        public void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
            message.UpdateBoundingBox(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            message.Draw(spriteBatch);
            menu.Draw(spriteBatch);
        }

        #endregion Methods
    }
}
