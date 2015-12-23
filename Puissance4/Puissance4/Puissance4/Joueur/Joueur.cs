using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Puissance4
{
    abstract class Joueur
    {
        protected int playerToken;
        protected Map map;

        public const int N_MAX_PLAYERS = 2;
        private static int actualIndex =0;
        private static List<Joueur> players = new List<Joueur>();
        public static int gagnant;

        public Joueur(int playerToken, Map map)
        {
            this.map = map;
            this.playerToken = playerToken;
            players.Add(this);
        }

        public abstract void Update(GameTime gametime);
        public abstract void Draw(GameTime gametime, SpriteBatch spriteBatch);

        public static Joueur nextPlayer()
        {
            Joueur joueurActuel = getPlayer();
            if (actualIndex + 1 < players.Count)
            {
                actualIndex++;
            }
            else
            {
                actualIndex = 0;
            }
            if (players[actualIndex] is JoueurHumain && !(joueurActuel is JoueurIA))
            {
                JoueurHumain jh = getPlayer() as JoueurHumain;
                jh.hasToValidate();
            }
            return getPlayer();

        }
        public static Joueur getPlayer(){

            return players[actualIndex];
        }
        public static void cleanJoueurs(){
            players.Clear();
        }
    }
}
