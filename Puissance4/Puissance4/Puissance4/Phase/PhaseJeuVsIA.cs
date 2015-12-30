using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puissance4
{
    //Phase de jeu 1 Joueur Vs IA
    class PhaseJeuVsIA:Phase
    {
        private Map map;

        public PhaseJeuVsIA()
            : base()
        {
            Joueur.cleanJoueurs();
            this.map = new Map();
            new JoueurHumain(Puissance4.PLAYER1_TOKEN, map, "Joueur");
            new JoueurIA(Puissance4.PLAYER2_TOKEN, map);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            Joueur.getActualPlayer().Update(gametime);

            if(endOfTheGame())
            {
                Joueur winner = Joueur.getWinner();
                Phase.setPhase(new PhaseMenuGain(winner, this));
            }
        }

        /* Vérifie si c'est la fin du jeu
            C'est la fin du jeu si : 
                - Le tableau est plein
                - L'IA a aligné 4 pions
                - Le joueur a aligné 4 pions
            Retourne vrai si c'est la fin du jeu. Faux sinon.
            */
        private bool endOfTheGame()
        {
            if(thereIsAWinner() || mapIsFull()) 
            {
                return true;
            }
            return false;
        }

        /* Vérifie si un joueur a gagné en alignant 4 pions */
        private bool thereIsAWinner()
        {
            for (int i = 0; i < Map.NB_COLONNES; i++)
            {
                for (int j = 0; j < Map.NB_LIGNES; j++)
                {
                    if (this.map.getGagnant(j, i) == -1)
                    {

                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
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

        public override void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Joueur.getActualPlayer().Draw(gametime, spriteBatch);
            map.Draw(gametime, spriteBatch);
        }
    }
}
