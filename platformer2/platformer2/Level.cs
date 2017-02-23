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
    public class Level
    {
        public Texture2D PlatformTexture;
        public Texture2D BackgroundTexture;
        public Texture2D ForegroundTexture;
        public List<Platform> Platforms = new List<Platform>();
        public Vector2 P1Spawn, P2Spawn, P3Spawn, P4Spawn;

        
        public virtual void LoadContent(ContentManager Content)
        {
            
            foreach (Platform p in Platforms)
            {
                p.LoadContent(Content);
            }
        }

        public virtual void Update(Game1 game)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, Game1.GAME_WIDTH, Game1.GAME_HEIGHT), Color.White);
            foreach (Platform p in Platforms)
            {
                p.Draw(spriteBatch);
            }
            spriteBatch.Draw(ForegroundTexture, new Rectangle(0, 0, Game1.GAME_WIDTH, Game1.GAME_HEIGHT), Color.White);
        }
    }
}
