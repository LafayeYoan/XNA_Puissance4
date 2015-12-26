using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puissance4
{
    class PhaseMenuGain:Phase
    {
        private MenuGain menuGain;

        public PhaseMenuGain(Joueur winner, Phase caller)
            : base()
        {
            this.menuGain = new MenuGain(winner, caller);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            menuGain.Draw(gametime,spriteBatch);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            menuGain.Update(gametime);
            Phase nextPhase = menuGain.getNextPhase();
            if (nextPhase != null)
            {
                //Appeler la phase suivane
                Phase.setPhase(nextPhase);
            }
        }
    }
}
