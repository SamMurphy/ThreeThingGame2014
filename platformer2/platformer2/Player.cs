using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System;

namespace platformer2
{
    public class Player : Sprite
    {
        Rectangle sourceRect;
        bool isBReleased;
        bool isXReleased;
        bool isYReleased;
        bool inAir;
        Random rand;
        int yRespawn;
        int respawnXDrift;
        bool isFacingLeft;
        int colourCount;
        int driftCount;
        int animationCount;
        int punchTimer;
        int frameNumber;
        public PlayerIndex PlayerIndex;
        // Player Stats
        public int MaxSpeed;
        public int Range;
        public int AttackStat;
        public int SpecAtkStat;
        public int DefenseStat;
        public int ProjectileStrength;
        public int ProjectileSpeed;
        public float Health;
        public float MaxHealth;
        public int Lives;
        public int Points;
        public int MaxPoints;
        // Player Textures
        public Texture2D LeftTexture;
        public Texture2D RightTexture;
        public Texture2D baloonTexture;
        // The players projectile
        public Projectile PlayerProjectile;
        public enum PlayerState
        {
            Alive,
            Respawning,
            Dead
        }
        public enum SpriteState
        {
            Left,
            Right,
            StandingLeft,
            StandingRight,
            JumpLeft,
            JumpRight,
            PunchLeft,
            PunchRight,
            Baloon
        }
        PlayerState playerState;
        SpriteState spriteState;

        public override void LoadContent(ContentManager Content)
        {
            Lives = 3;
            Points = 0;
            MaxPoints = 100;
            SpriteRectangle = new Rectangle(
                Game1.GAME_WIDTH / 2,
                Game1.GAME_HEIGHT - 30,
                Game1.GAME_WIDTH / 19,
                Game1.GAME_HEIGHT / 13);
            sourceRect = new Rectangle(0, 0, 0, 0);
            PlayerProjectile.SpriteRectangle.Width = PlayerProjectile.LeftTexture.Width;
            PlayerProjectile.SpriteRectangle.Height = PlayerProjectile.LeftTexture.Height;
            PlayerProjectile.Range = Range;
            PlayerProjectile.Strength = ProjectileStrength;
            PlayerProjectile.Speed = ProjectileSpeed;
            colourCount = 0;
            rand = new Random();
            playerState = PlayerState.Alive;
            spriteState = SpriteState.StandingLeft;
            base.LoadContent(Content);
        }

        public override void Update(Game1 game)
        {
            switch (playerState)
            {
                case PlayerState.Alive:
                    MovePlayer(game);
                    Attack(game);
                    ProjectileCollisions(game);
                    ApplyGravity(game);
                    ChangeTexture();
                    Dead();
                    countDown();
                    break;
                case PlayerState.Respawning:
                    Respawning();
                    ChangeTexture();
                    break;
                case PlayerState.Dead:
                    break;
            }
            base.Update(game);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch(playerState)
            {
                case PlayerState.Alive:
                    //spriteBatch.Draw(SpriteTexture, SpriteRectangle, SpriteColour);
                    spriteBatch.Draw(SpriteTexture, SpriteRectangle, sourceRect, SpriteColour);
                    break;
                case PlayerState.Respawning:
                    //spriteBatch.Draw(SpriteTexture, SpriteRectangle, SpriteColour);
                    spriteBatch.Draw(SpriteTexture, SpriteRectangle, sourceRect, SpriteColour);
                    break;
                case PlayerState.Dead:
                    break;
            }
        }

        void MovePlayer(Game1 game)
        {
            // Control input
            KeyboardState keystate = Keyboard.GetState();
            GamePadState gamePad = GamePad.GetState(PlayerIndex);

            // Basic Movement
            if (keystate.IsKeyDown(Keys.A) 
                || gamePad.DPad.Left == ButtonState.Pressed 
                || gamePad.ThumbSticks.Left.X < 0)
            {
                // Left
                Velocity.X--;
                isFacingLeft = true;
            }
            else if (keystate.IsKeyDown(Keys.D) 
                || gamePad.DPad.Right == ButtonState.Pressed 
                || gamePad.ThumbSticks.Left.X > 0)
            {
                // Right
                Velocity.X++;
                isFacingLeft = false;
            }
            else
                friction();

            // Caps the maximum player speed.
            if (Velocity.X > MaxSpeed)
                Velocity.X = MaxSpeed;
            if (Velocity.X < -MaxSpeed)
                Velocity.X = -MaxSpeed;

            // Applies Movement
            SpriteRectangle.X = SpriteRectangle.X + (int)Velocity.X;

            // Jumping Mechanics
            if (keystate.IsKeyDown(Keys.Space) || gamePad.Buttons.A == ButtonState.Pressed)
            {
                if (Velocity.Y == 0 && !inAir)
                {
                    Velocity.Y = -15;
                    inAir = true;
                }
            }
            if (Velocity.Y >= 0)
            {
                inAir = false;
            }
            SpriteRectangle.Y += (int)Velocity.Y;
        }

        void friction()
        {
            if (Velocity.X > 0)
                Velocity.X--;
            if (Velocity.X < 0)
                Velocity.X++;
        }

        void PlatformCollision(Game1 game)
        {
            foreach (Platform t in game.ReturnPlatformList())
            {
                if (t.SpriteRectangle.Intersects(SpriteRectangle) && Velocity.Y > 0)
                {
                    Velocity.Y = 0;
                    SpriteRectangle.Y = t.SpriteRectangle.Y - SpriteRectangle.Height - 1;
                }
            }
        }

        void ApplyGravity(Game1 game)
        {
                Velocity.Y++;
                if (Velocity.Y >= 10)
                {
                    Velocity.Y = 10;
                }
                SpriteRectangle.Y += (int)Velocity.Y;
                if (SpriteRectangle.Y + SpriteRectangle.Height >= Game1.GAME_HEIGHT)
                {
                    Velocity.Y = 0;
                    SpriteRectangle.Y = Game1.GAME_HEIGHT - SpriteRectangle.Height;
                }
                PlatformCollision(game);
        }

        bool punching()
        {
            if (punchTimer <= 0)
            {
                return false;
            }
            if (spriteState == SpriteState.PunchRight)
	        {
                return true;
	        }
            if (spriteState == SpriteState.PunchLeft)
            {
                return true;
            }
            if (spriteState == SpriteState.Baloon)
            {
                return true;
            }
            return false;
        }
        void ChangeTexture()
        {
            
                if (spriteState == SpriteState.Baloon)
	            {
		            
	            }
                else if (isFacingLeft)
                {
                    
                    if (spriteState == SpriteState.PunchLeft)
                    {

                    }
                    else if (Velocity.X < 0)
                        spriteState = SpriteState.Left;
                    else if (Velocity.Y < 0)
                        spriteState = SpriteState.JumpLeft;
                    else
                        spriteState = SpriteState.StandingLeft;
                }
                else
                {
                    if (spriteState == SpriteState.PunchRight)
                    {
                        
                    }
                    else if (Velocity.X > 0)
                        spriteState = SpriteState.Right;
                    else if (Velocity.Y < 0)
                        spriteState = SpriteState.JumpRight;
                    else
                        spriteState = SpriteState.StandingRight;
                }
                

            switch (spriteState)
            {
                case SpriteState.Left:
                    Animation(400, 50, 47, 5, 5);
                    break;
                case SpriteState.Right:
                    Animation(0, 50, 47, 5, 5);
                    break;
                case SpriteState.StandingLeft:
                    sourceRect = new Rectangle(600, 0, 50, 47);
                    break;
                case SpriteState.StandingRight:
                    sourceRect = new Rectangle(210, 0, 50, 47);
                    break;
                case SpriteState.JumpLeft:
                    sourceRect = new Rectangle(500, 0, 50, 47);
                    break;
                case SpriteState.JumpRight:
                    sourceRect = new Rectangle(100, 0, 50, 47);
                    break;
                case SpriteState.PunchLeft:
                    sourceRect = new Rectangle(700, 0, 50, 47);
                    break;
                case SpriteState.PunchRight:
                    sourceRect = new Rectangle(310, 0, 50, 47);
                    break;
                case SpriteState.Baloon:
                    sourceRect = new Rectangle(800, 0, 50, 80);
                    break;
            }
        }

        void Animation(int x, int frameWidth, int frameHeight, int frameQuantity, int frameDelay)
        {
            animationCount++;
            if (animationCount > frameDelay)
	        {
		        frameNumber++;
                if (frameNumber > frameQuantity - 1)
	            {
                    frameNumber = 0;
	            }
                animationCount = 0;
	        }
            sourceRect.Width = frameWidth;
            sourceRect.Height = frameHeight;
            sourceRect.X = x;
            sourceRect.Y = frameHeight * frameNumber;
        }

        void Attack(Game1 game)
        {
            List<Player> playerList = game.ReturnPlayerList();
            
            // Punching - B
            GamePadState gamePad = GamePad.GetState(PlayerIndex);
            if (gamePad.Buttons.B == ButtonState.Released)
            {
                isBReleased = true;
            }
            if (gamePad.Buttons.B == ButtonState.Pressed && isBReleased)
            {
                isBReleased = false;
                if (isFacingLeft)
                    spriteState = SpriteState.PunchLeft;
                else
                    spriteState = SpriteState.PunchRight;
                foreach (Player a in playerList)
                    if (a != this)
                        AttackHit(a, AttackStat);
            }

            // Special - Y
            if (gamePad.Buttons.Y == ButtonState.Released)
            {
                isYReleased = true;
                punchTimer--;
            }  
            if (gamePad.Buttons.Y == ButtonState.Pressed && isYReleased && Points >= MaxPoints)
            {
                isYReleased = false;
                punchTimer = 400;
                if (isFacingLeft)
                    spriteState = SpriteState.PunchLeft;
                else
                    spriteState = SpriteState.PunchRight;
                Points -= 100;
                foreach (Player a in playerList)
                    if (a != this)
                        AttackHit(a, SpecAtkStat);
            }

            // Thowing Projectile - X
            if (gamePad.Buttons.X == ButtonState.Released)
            {
                isXReleased = true;
                punchTimer--;
            }   
            if (gamePad.Buttons.X == ButtonState.Pressed && !PlayerProjectile.IsAlive && isXReleased)
            {
                isXReleased = false;
                punchTimer = 400;
                if (isFacingLeft)
                    spriteState = SpriteState.PunchLeft;
                else
                    spriteState = SpriteState.PunchRight;
                PlayerProjectile.Reset(new Vector2(SpriteRectangle.X, SpriteRectangle.Y), isFacingLeft);
            }

            if (Points < 0)
                Points = 0;
            if (Points >  100)
                Points = 100;
        }

        void ProjectileCollisions(Game1 game)
        {
            foreach (Projectile p in game.ReturnProjectileList())
            {
                if (p != PlayerProjectile && p.IsAlive)
                {
                    if (p.SpriteRectangle.Intersects(SpriteRectangle))
                    {
                        Health -= (10 * (float)p.Strength) / DefenseStat;
                        Points -= 5;
                        foreach (Player a in game.ReturnPlayerList())
                        {
                            if (p == a.PlayerProjectile)
                            {
                                a.Points += 100;
                            }
                        }
                        p.IsAlive = false;
                        SpriteColour = Color.Red;
                        colourCount = 15;
                    }
                }
            }
        }

        void AttackHit(Player Enemy, int strength)
        {
            Rectangle hitZone;
            if (isFacingLeft)
            {
                hitZone = new Rectangle(SpriteRectangle.Left - 50, SpriteRectangle.Y, 50 + SpriteRectangle.Width, 40);
            }
            else
            {
                hitZone = new Rectangle(SpriteRectangle.Left, SpriteRectangle.Y, 50 + SpriteRectangle.Width, 40);
            }

            if (hitZone.Intersects(Enemy.SpriteRectangle))
            {
                Enemy.Health -= (10 * (float)strength) / Enemy.DefenseStat;
                Enemy.SpriteColour = Color.Red;
                Enemy.colourCount = 15;
                Points += 10;
                Enemy.Points -= 5;
                if (isFacingLeft)
                {
                    // Sets Enemy recoil.
                    Enemy.Velocity.X = -15;
                    Enemy.Velocity.Y = -10;
                }
                else
                {
                    // Sets Enemy recoil.
                    Enemy.Velocity.X = 15;
                    Enemy.Velocity.Y = -10;
                }
            }
        }

        void countDown()
        {
            if (colourCount != 0)
            {
                colourCount--;
                if (colourCount <= 0)
                {
                    SpriteColour = Color.White;
                    colourCount = 0;
                } 
            }
            
        }

        void Dead()
        {
            if (Health <= 0)
            {
                IsAlive = false;
                Lives--;
                if (Lives <= 0)
                    playerState = PlayerState.Dead;
                else
                {
                    // Setting up for respawn
                    SpriteRectangle.X = newRand(0, Game1.GAME_WIDTH - SpriteRectangle.Width);
                    SpriteRectangle.Y = Game1.GAME_HEIGHT + SpriteRectangle.Height + 20;
                    yRespawn = newRand(Game1.GAME_HEIGHT / 3, Game1.GAME_HEIGHT / 3 * 2);
                    spriteState = SpriteState.Baloon;
                    playerState = PlayerState.Respawning;
                }
            }
        }

        int newRand(int min, int max)
        {
            return rand.Next(min, max);
        }

        void Respawning()
        {
            if (driftCount <= 0)
            {
                respawnXDrift = newRand(-1, 2);
                driftCount = 15;
            }
            if (driftCount >= 0)
            {
                SpriteRectangle.X += respawnXDrift * 5;
                driftCount--;
            }

            SpriteRectangle.Y -= 5;
            // When Y reaches a certian point change to playing again.
            if (SpriteRectangle.Y <= yRespawn)
            {
                Health = MaxHealth;
                playerState = PlayerState.Alive;
                IsAlive = true;
                spriteState = SpriteState.StandingLeft;
            }
        }
    }
}
