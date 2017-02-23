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
    public class Player_Adam : Player
    {
        public Player_Adam(PlayerIndex index, Projectile projectile)
        {
            IsAlive = true;
            PlayerIndex = index;
            AttackStat = 8;
            SpecAtkStat = 1000;
            DefenseStat = 14;
            MaxSpeed = 8;
            Range = 12 * 50;
            ProjectileStrength = 8;
            ProjectileSpeed = 10;
            MaxHealth = 100;
            Health = MaxHealth;
            PlayerProjectile = projectile;
        }

        public override void LoadContent(ContentManager Content)
        {
            // Player Textures
            SpriteTexture = Content.Load<Texture2D>("Rob_Spritesheet");

            // Character Specific Projectile Properties
            PlayerProjectile.LeftTexture = Content.Load<Texture2D>("polo");
            PlayerProjectile.RightTexture = Content.Load<Texture2D>("polo");
                    
            base.LoadContent(Content);
        }
    }
}
