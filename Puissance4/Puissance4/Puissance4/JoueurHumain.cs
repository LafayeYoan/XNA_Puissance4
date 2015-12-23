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
    class JoueurHumain : Joueur
    {
        private int cursorPosition;
        private String direction;
        private bool lockKey;

        public JoueurHumain(int playerIndex, Map map) : base (playerIndex, map)
        {
            this.cursorPosition = 3;
            this.lockKey = false;
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Right) && !lockKey)
            {
                direction = "Right";
                if (cursorPosition < (Map.NB_COLONNES - 1))
                {
                    cursorPosition++;
                }
                lockKey = true;
            }
            if (keyboard.IsKeyDown(Keys.Left) && !lockKey)
            {
                direction = "Left";
                if (cursorPosition > 0)
                {
                    cursorPosition--;
                }
                lockKey = true;
            }
            if (keyboard.IsKeyDown(Keys.Down) && !lockKey)
            {
                direction = "Down";
                if (map.columnHaveFreeSpace(cursorPosition))
                {
                    gagnant = map.addToken(cursorPosition, this.playerToken);
                    Joueur.nextPlayer();
                }
                //TODO : Transparence du curseur
                lockKey = true;
            }

            switch (direction)
            {
                case ("Right"):
                    if (keyboard.IsKeyUp(Keys.Right))
                        lockKey = false;
                    break;
                case ("Left"):
                    if (keyboard.IsKeyUp(Keys.Left))
                        lockKey = false;
                    break;
                case ("Down"):
                    if (keyboard.IsKeyUp(Keys.Down))
                        lockKey = false;
                    break;
            }
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            //Draw cursor
            int xpos, ypos;
            xpos = Puissance4.OFFSET_X - Puissance4.TAILLE_BLOCK;
            ypos = Puissance4.OFFSET_Y + cursorPosition * Puissance4.TAILLE_BLOCK;
            Vector2 pos = new Vector2(ypos, xpos);
            spriteBatch.Draw(Puissance4.cursor.Texture, pos, Color.White);
        }
    }
}
