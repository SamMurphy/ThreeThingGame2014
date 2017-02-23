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
    public class Player_Brian : Player
    {
        public Player_Brian(PlayerIndex index, Projectile projectile)
        {
            IsAlive = true;
            PlayerIndex = index;
            AttackStat = 10;
            SpecAtkStat = 1000;
            DefenseStat = 10;
            MaxSpeed = 10;
            Range = 10 * 50;
            ProjectileStrength = 10;
            ProjectileSpeed = 10;
            MaxHealth = 100;
            Health = MaxHealth;
            PlayerProjectile = projectile;
        }

        public override void LoadContent(ContentManager Content)
        {
            // Player Textures
            LeftTexture = Content.Load<Texture2D>("playerL2");
            RightTexture = Content.Load<Texture2D>("playerR2");
            baloonTexture = Content.Load<Texture2D>("playerR2");

            // Character Specific Projectile Properties
            PlayerProjectile.LeftTexture = Content.Load<Texture2D>("FloppyDisk");
            PlayerProjectile.RightTexture = Content.Load<Texture2D>("FloppyDisk");
                    
            base.LoadContent(Content);
        }
    }
}
