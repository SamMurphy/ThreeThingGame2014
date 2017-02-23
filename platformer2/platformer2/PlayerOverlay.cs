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
    public class PlayerOverlay : Sprite
    {
        Texture2D OverlayTexture;
        Texture2D BarTexture;
        Player Player;
        Rectangle HealthRect;
        int ID;
        int MaxHealthLength;
        float length1;
        float length2;
        Rectangle PointsRect;
        int MaxPointsLength;
        public PlayerOverlay(Player player, Texture2D overlay, Vector2 position, int inID)
        {
            Player = player;
            OverlayTexture = overlay;
            Position = position;
            ID = inID;
        }

        public override void LoadContent(ContentManager Content)
        {
            BarTexture = Content.Load<Texture2D>("block");
            SpriteRectangle = new Rectangle((int)Position.X, (int)Position.Y, OverlayTexture.Width, OverlayTexture.Height);
            if (ID == 1 || ID == 3)
            {
                HealthRect = new Rectangle(SpriteRectangle.X + 170, SpriteRectangle.Y + 15, SpriteRectangle.X + 330,  30);
                MaxHealthLength = 330;
                PointsRect = new Rectangle(SpriteRectangle.X + 170, SpriteRectangle.Y + 55, SpriteRectangle.X + 290, 35);
                MaxPointsLength = 290;
            }
            if (ID == 2 || ID == 4)
            {
                HealthRect = new Rectangle(SpriteRectangle.Right - 170, SpriteRectangle.Y + 15, SpriteRectangle.Right - 330,  30);
                MaxHealthLength = 330;
                PointsRect = new Rectangle(SpriteRectangle.Right - 170, SpriteRectangle.Y + 55, SpriteRectangle.Right - 330,  30);
                MaxPointsLength =  290;
            }
        }

        public override void Update(Game1 game)
        {
            if (ID == 1 || ID == 3)
            {
                length1 = Player.Health / 100 * MaxHealthLength;
                HealthRect.Width = (int)length1;
                length2 = ((float)Player.Points / 100) * MaxPointsLength;
                PointsRect.Width = (int)length2;
            }
            if (ID == 2 || ID == 4)
            {
                length1 = Player.Health / 100 * MaxHealthLength;
                HealthRect.Width = (int)length1;
                HealthRect.X = SpriteRectangle.Right -170 -(int)length1;
                length2 = ((float)Player.Points / 100) * MaxPointsLength;
                PointsRect.Width = (int)length2;
                PointsRect.X = SpriteRectangle.Right - 170 - (int)length2;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(OverlayTexture, SpriteRectangle, Color.White);
            spriteBatch.Draw(BarTexture, HealthRect, Color.Green);
            spriteBatch.Draw(BarTexture, PointsRect, Color.Blue);
        }
    }
}
