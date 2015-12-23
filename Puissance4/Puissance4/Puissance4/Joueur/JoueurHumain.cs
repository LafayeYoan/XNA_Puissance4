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
        private bool hasPressDownToStart;

        private String nomJoueur;

        public JoueurHumain(int playerIndex, Map map, String nomJoueur) : base (playerIndex, map)
        {
            this.nomJoueur = nomJoueur;
            this.cursorPosition = 3;
            this.lockKey = false;
            hasPressDownToStart = true;
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keyboard = Keyboard.GetState();
            if (!hasPressDownToStart)
            {
                if (keyboard.IsKeyDown(Keys.Enter) && !lockKey)
                {
                    direction = "Enter";
                    hasPressDownToStart = true;
                    lockKey = true;
                }
                unlockKey();
                return;
            }
            
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
                unlockKey();         
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            if (!hasPressDownToStart)
            {
                int xpostxt, ypostxt;
                xpostxt = Puissance4.OFFSET_X - Puissance4.TAILLE_BLOCK;
                ypostxt = Puissance4.OFFSET_Y;
                Vector2 postxt = new Vector2(ypostxt, xpostxt);
                spriteBatch.DrawString(Puissance4.textFont, "Au tour de "+ nomJoueur + ", Appuyer sur Entree pour continuer.", postxt, Color.White);
                return;
            }
            //Draw cursor
            int xpos, ypos;
            xpos = Puissance4.OFFSET_X - Puissance4.TAILLE_BLOCK;
            ypos = Puissance4.OFFSET_Y + cursorPosition * Puissance4.TAILLE_BLOCK;
            Vector2 pos = new Vector2(ypos, xpos);
            spriteBatch.Draw(Puissance4.cursor.Texture, pos, Color.White);
        }

        public void hasToValidate(bool valid = true)
        {
            if (valid == true)
            {
                this.hasPressDownToStart = false;
            }
            else
            {
                this.hasPressDownToStart = true;
            }
        }

        private void unlockKey()
        {
            KeyboardState keyboard = Keyboard.GetState();
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
                case ("Enter"):
                    if (keyboard.IsKeyUp(Keys.Enter))
                        lockKey = false;
                    break;
            }
        }
    }
}
