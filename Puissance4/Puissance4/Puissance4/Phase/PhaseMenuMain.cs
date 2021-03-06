﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Puissance4
{
    /// <summary>
    /// Phase de menu
    /// </summary>
    class PhaseMenuMain:Phase
    {
        MenuMain menu;
        public PhaseMenuMain()
            : base()
        {
            this.menu = new MenuMain();
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gametime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            menu.Draw(gametime,spriteBatch);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            menu.Update(gametime);
            Phase nextPhase = menu.getNextPhase();
            if (nextPhase != null)
            {
                //Appeler la phase suivane
                Phase.setPhase(nextPhase);
            }
        }
    }
}
