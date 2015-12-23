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
            Joueur.getPlayer().Update(gametime);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Joueur.getPlayer().Draw(gametime, spriteBatch);
            map.Draw(gametime, spriteBatch);
        }
    }
}
