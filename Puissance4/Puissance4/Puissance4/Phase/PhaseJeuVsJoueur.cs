﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puissance4
{
    //Phase de jeu un Joueur contre un Autre
    class PhaseJeuVsJoueur:Phase
    {
        private Map map;

        public PhaseJeuVsJoueur()
            : base()
        {
            Joueur.cleanJoueurs();
            this.map = new Map();
            new JoueurHumain(Puissance4.PLAYER1_TOKEN, map,"Premier Joueur");
            new JoueurHumain(Puissance4.PLAYER2_TOKEN, map,"Second Joueur");
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