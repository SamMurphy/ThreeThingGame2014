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
    public class Button : Sprite
    {
        Color spriteHoverColor;
        Color spriteColor;
        public bool IsClicked;

        // Rectangle to follow the mouse
        static Rectangle mouseRectangle;

        /// <summary>
        /// Button contstructor
        /// </summary>
        /// <param name="texture">Button Texture</param>
        /// <param name="position">Position of bullet</param>
        public Button(Texture2D texture, Vector2 position)
        {
            spriteHoverColor = Color.LimeGreen;
            spriteColor = Color.White;
            SpriteTexture = texture;
            Position = position;
            IsClicked = false;
            IsAlive = true;
        }

        /// <summary>
        /// Creates the sprite rectangle
        /// </summary>
        /// <param name="Content">The content manager</param>
        public override void LoadContent(ContentManager Content)
        {
            // Creates the sprite rectangle
            SpriteRectangle = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                SpriteTexture.Width,
                SpriteTexture.Height);

            base.LoadContent(Content);
        }

        /// <summary>
        /// Detects when the mouse is over the button
        /// Changes the buttons colour and detects when it
        /// is clicked upon
        /// </summary>
        /// <param name="game"></param>
        public override void Update(Game1 game)
        {
            // Only returns for visible buttons
            if (IsAlive)
            {
                MouseState mouse = Mouse.GetState();
                //Sets the mouseRectangle to the position of the mouse
                mouseRectangle.X = mouse.X;
                mouseRectangle.Y = mouse.Y;

                // Is the mouse over the button?
                if (mouseRectangle.Intersects(this.SpriteRectangle))
                {
                    // Changes the colour of the button
                    spriteColor = spriteHoverColor;
                    IsClicked = false;
                    // Checks if the button is clicked
                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        IsClicked = true;
                    }
                }
                else
                {
                    // Resets the button
                    this.spriteColor = Color.White;
                    this.IsClicked = false;
                }
            }
            else
                IsClicked = false;
        }

        /// <summary>
        /// Draws the button if visible
        /// </summary>
        /// <param name="spriteBatch">The sprite batch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsAlive)
                spriteBatch.Draw(SpriteTexture, SpriteRectangle, spriteColor);
        }
    }
}
