using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puissance4
{
    //Phase de jeu 1 Joueur Vs IA
    class PhaseJeuVsIA:Phase
    {

        public PhaseJeuVsIA()
            : base()
        {
            Joueur.cleanJoueurs();
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

        public override void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Joueur.getActualPlayer().Draw(gametime, spriteBatch);
            map.Draw(gametime, spriteBatch);
        }
    }
}
