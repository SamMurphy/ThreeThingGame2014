using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace platformer2
{
    public class Sprite
    {
        public Texture2D SpriteTexture;
        public Rectangle SpriteRectangle;
        public Vector2 Velocity;
        public Vector2 Position;
        public bool IsAlive;
        public Color SpriteColour;

        public virtual void LoadContent(ContentManager Content)
        {
            SpriteColour = Color.White;
        }

        public virtual void Update(Game1 game)
        {
                EdgeOfScreen();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
                spriteBatch.Draw(SpriteTexture, SpriteRectangle, SpriteColour);
        }

        public virtual void Reset(Game1 game)
        {
        }

        void EdgeOfScreen()
        {
            if (SpriteRectangle.Right < 0)
                SpriteRectangle.X = Game1.GAME_WIDTH;
            if (SpriteRectangle.Left > Game1.GAME_WIDTH)
                SpriteRectangle.X = 0 - SpriteRectangle.Width;
        }
    }
}
