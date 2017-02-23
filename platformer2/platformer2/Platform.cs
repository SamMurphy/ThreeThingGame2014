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
    public class Platform : Sprite
    {
        public int Length;
        public Platform(Texture2D texture, Vector2 position, int length)
        {
            SpriteTexture = texture;
            Position = position;
            IsAlive = true;
            Length = length;
        }

        public override void LoadContent(ContentManager Content)
        {
            // Create sprite rectangle
            SpriteRectangle = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Length,
                5);

            base.LoadContent(Content);
        }
    }
}
