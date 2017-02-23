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
    public class Player_Mike : Player
    {
        public Player_Mike(PlayerIndex index, Projectile projectile)
        {
            IsAlive = true;
            PlayerIndex = index;
            AttackStat = 10;
            SpecAtkStat = 1000;
            DefenseStat = 12;
            MaxSpeed = 8;
            Range = 6 * 50;
            ProjectileStrength = 12;
            ProjectileSpeed = 12;
            MaxHealth = 100;
            Health = MaxHealth;
            PlayerProjectile = projectile;
        }

        public override void LoadContent(ContentManager Content)
        {
            // Player Textures
            SpriteTexture = Content.Load<Texture2D>("Mike Brayshaw Spritesheet");

            // Character Specific Projectile Properties
            PlayerProjectile.LeftTexture = Content.Load<Texture2D>("FloppyDisk");
            PlayerProjectile.RightTexture = Content.Load<Texture2D>("FloppyDisk");
                    
            base.LoadContent(Content);
        }
    }
}
