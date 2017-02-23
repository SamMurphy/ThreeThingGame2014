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
    public class Player_Matty : Player
    {
        public Player_Matty(PlayerIndex index, Projectile projectile)
        {
            IsAlive = true;
            PlayerIndex = index;
            AttackStat = 12;
            SpecAtkStat = 1000;
            DefenseStat = 6;
            MaxSpeed = 12;
            Range = 8 * 50;
            ProjectileStrength = 12;
            ProjectileSpeed = 10;
            MaxHealth = 100;
            Health = MaxHealth;
            PlayerProjectile = projectile;
        }

        public override void LoadContent(ContentManager Content)
        {
            // Player Textures
            SpriteTexture = Content.Load<Texture2D>("Mike Brayshaw Spritesheet");

            // Character Specific Projectile Properties
            PlayerProjectile.LeftTexture = Content.Load<Texture2D>("Pokeball");
            PlayerProjectile.RightTexture = Content.Load<Texture2D>("Pokeball");
                    
            base.LoadContent(Content);
        }
    }
}
