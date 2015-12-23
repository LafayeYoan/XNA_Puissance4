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
    abstract class Menu
    {
        public abstract void Update(GameTime gametime);
        public abstract void Draw(GameTime gametime, SpriteBatch spriteBatch);
    }
}
