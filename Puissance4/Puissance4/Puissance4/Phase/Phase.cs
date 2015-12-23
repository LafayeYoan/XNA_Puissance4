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

        public Phase()
        {
            
        }

        /// <summary>
        /// Set teh actual pahse
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
            actualPhase = new PhaseMenu();            
            return getPhase();
        }

        public abstract void Update(GameTime gametime);
        public abstract void Draw(GameTime gametime, SpriteBatch spriteBatch);

    }
}
