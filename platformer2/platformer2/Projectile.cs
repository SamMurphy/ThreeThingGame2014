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
    public class Projectile : Sprite
    {
        public int Range;
        public int Strength;
        public int Speed;
        public Vector2 Origin;
        public Texture2D LeftTexture;
        public Texture2D RightTexture;

        public Projectile()
        {
        }

        public void Reset(Vector2 position, bool isGoingLeft)
        {
            IsAlive = true;
            Origin = position;
            SpriteRectangle.X = (int)position.X;
            SpriteRectangle.Y = (int)position.Y;
            if (isGoingLeft)
            {
                Velocity.X = -Speed;
                SpriteTexture = LeftTexture;
            }
            else
            {
                Velocity.X = Speed;
                SpriteTexture = RightTexture;
            }
        }

        public override void Update(Game1 game)
        {
            if (IsAlive)
            {
                if (SpriteRectangle.X >= Origin.X + Range || SpriteRectangle.X <= Origin.X - Range)
                {
                    IsAlive = false;
                }
                SpriteRectangle.X += (int)Velocity.X;
                ProjectileCollisions(game);
            }
            base.Update(game);
        }

        void ProjectileCollisions(Game1 game)
        {
            foreach (Projectile p in game.ReturnProjectileList())
            {
                if (p != this && p.IsAlive)
                {
                    if (p.SpriteRectangle.Intersects(SpriteRectangle))
                    {
                        p.IsAlive = false;
                        IsAlive = false;
                    }
                }
            }
        }
    }
}
