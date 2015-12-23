using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Puissance4
{
    /// <summary>
    /// Menu principal
    /// </summary>
    class MainMenu:Menu
    {
        //Couleur des textes
        private Color titleColor;
        private Color selectedColor;
        private Color itemColor;

        //Index selectioné
        private int selectedIndex;

        //Liste de choix
        private List<String> items;

        //Lock the keys
        private String direction;
        private bool lockKey;

        //Enter pressed
        private bool validated;

        public MainMenu()
            : base()
        {
            this.titleColor = new Color(255, 0, 0);
            this.itemColor = Color.Azure;
            this.selectedColor = Color.Blue;
            items = new List<string>();
            items.Add("1 Joueur");
            items.Add("2 Joueurs");
            items.Add("Quitter");

            selectedIndex = 0;
        }

        //Keybord handle
        public override void Update(GameTime gametime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Down) && !lockKey)
            {
                direction = "Down";
                if (selectedIndex < (items.Count - 1))
                {
                    selectedIndex++;
                }
                lockKey = true;
            }
            if (keyboard.IsKeyDown(Keys.Up) && !lockKey)
            {
                direction = "Up";
                if (selectedIndex > 0)
                {
                    selectedIndex--;
                }
                lockKey = true;
            }
            if (keyboard.IsKeyDown(Keys.Enter) && !lockKey)
            {
                direction = "Enter";
                validated = true;
                lockKey = true;
            }

            switch (direction)
            {
                case ("Down"):
                    if (keyboard.IsKeyUp(Keys.Down))
                        lockKey = false;
                    break;
                case ("Up"):
                    if (keyboard.IsKeyUp(Keys.Up))
                        lockKey = false;
                    break;
                case ("Enter"):
                    if (keyboard.IsKeyUp(Keys.Enter))
                        lockKey = false;
                    break;
            }
        }

        //Dessin du menu
        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Puissance4.titleFont, "Puissance 4",new Vector2(Puissance4.OFFSET_X, Puissance4.OFFSET_Y), titleColor);

            for (int i = 0; i < items.Count(); i++)
            {
                if (i == selectedIndex)
                {
                    spriteBatch.DrawString(Puissance4.textFont, items[i], new Vector2(Puissance4.OFFSET_X+200, Puissance4.OFFSET_Y + 200 + i * 20), selectedColor);
                }
                else
                {
                    spriteBatch.DrawString(Puissance4.textFont, items[i], new Vector2(Puissance4.OFFSET_X+200, Puissance4.OFFSET_Y + 200 + i * 20), itemColor);
                }
                
            }
        }

        //Obtenir la phase suivante
        public Phase getNextPhase()
        {
            if (!validated)
            {
                return null;
            }
            if (selectedIndex == 2)
            {
                Puissance4.game.Exit();
            }
            else if (selectedIndex == 0)
            {
                return new PhaseJeuVsIA();
            }
            else if (selectedIndex == 1)
            {
                return new PhaseJeuVsJoueur();
            }
            return null;
        }
    }
}
