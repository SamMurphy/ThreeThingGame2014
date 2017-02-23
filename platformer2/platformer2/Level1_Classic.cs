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
    public class Level1_Classic : Level
    {
        public Level1_Classic(Texture2D platform)
        {
            PlatformTexture = platform;
        }
        public override void LoadContent(ContentManager Content)
        {
            
            BackgroundTexture = Content.Load<Texture2D>("background");
            ForegroundTexture = Content.Load<Texture2D>("Level_Classic_Foreground");

            // Platforms
            Platform platform1 = new Platform(PlatformTexture, new Vector2((Game1.GAME_WIDTH/2) - 300, (Game1.GAME_HEIGHT/5)*2 + 100), 600);
            Platforms.Add(platform1);
            Platform platform2 = new Platform(PlatformTexture, new Vector2(100, (Game1.GAME_HEIGHT/5) * 4), Game1.GAME_WIDTH - 200);
            Platforms.Add(platform2);
            Platform platform3 = new Platform(PlatformTexture, new Vector2(50, (Game1.GAME_HEIGHT/5)*3), 400);
            Platforms.Add(platform3);
            Platform platform4 = new Platform(PlatformTexture, new Vector2(Game1.GAME_WIDTH-450, (Game1.GAME_HEIGHT/5)*3), 400);
            Platforms.Add(platform4);

            // Spawns
            P1Spawn = new Vector2(0, 0);
            P2Spawn = new Vector2(0, 0);
            P3Spawn = new Vector2(0, 0);
            P4Spawn = new Vector2(0, 0);

            base.LoadContent(Content);
        }
    }
}
