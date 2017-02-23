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
    public class Player_Rob : Player
    {
        public Player_Rob(PlayerIndex index, Projectile projectile)
        {
            IsAlive = true;
            PlayerIndex = index;
            AttackStat = 12;
            SpecAtkStat = 1000;
            DefenseStat = 10;
            MaxSpeed = 8;
            Range = 14 * 50;
            ProjectileStrength = 6;
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
            PlayerProjectile.LeftTexture = Content.Load<Texture2D>("BanjoLeft");
            PlayerProjectile.RightTexture = Content.Load<Texture2D>("BanjoRight");

                    
            base.LoadContent(Content);
        }
    }
}
