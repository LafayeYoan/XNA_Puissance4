using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Puissance4
{
    /// <summary>
    /// Une phase de jeu
    /// </summary>
    abstract class Phase
    {
        //Phase actuelle
        private static Phase actualPhase;
        protected Map map;

        public Phase()
        {
            this.map = new Map();
        }

        /// <summary>
        /// Set the actual phase
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        public static Phase setPhase(Phase phase)
        {
            actualPhase = phase;
            return getPhase();
        }

        /// <summary>
        /// Get the actual phase
        /// </summary>
        /// <returns></returns>
        public static Phase getPhase()
        {
            return actualPhase;
        }

        /// <summary>
        /// Retart the game
        /// </summary>
        /// <returns></returns>
        public static Phase clean()
        {
            actualPhase = new PhaseMenuMain();            
            return getPhase();
        }

        /* Vérifie si c'est la fin du jeu
            C'est la fin du jeu si : 
                - Le tableau est plein
                - L'IA a aligné 4 pions
                - Le joueur a aligné 4 pions
            Retourne vrai si c'est la fin du jeu. Faux sinon.
            */
        protected bool endOfTheGame()
        {
            if (thereIsAWinner() || mapIsFull())
            {
                return true;
            }
            return false;
        }

        /* Vérifie si un joueur a gagné en alignant 4 pions */
        private bool thereIsAWinner()
        {
            return Joueur.getWinner() != null;
        }


        /* Vérifie si le tableau de jeu est plein */
        private bool mapIsFull()
        {
            for (int i = 0; i < Map.NB_COLONNES; i++)
            {
                if (this.map.columnHaveFreeSpace(i))
                {
                    return false;
                }
            }
            return true;
        }

        public abstract void Update(GameTime gametime);
        public abstract void Draw(GameTime gametime, SpriteBatch spriteBatch);

    }
}
