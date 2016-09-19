using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DeenGames.InfiniteArpg.Ecs.Components
{
    public class Drawable
    {
        private bool isColour = false;
        private Texture2D texture2D;
        private int width = 0;
        private int height = 0;

        public Drawable Image(Texture2D imageTexture)
        {
            this.isColour = false;
            this.texture2D = imageTexture;
            return this;
        }


        public Drawable Colour(Texture2D colourTexture, int width, int height)
        {
            this.isColour = true;
            this.texture2D = colourTexture;
            this.width = width;
            this.height = height;
            return this;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.isColour == true)
            {
                spriteBatch.Draw(this.texture2D, null, new Rectangle(0, 0, this.width, this.height));
            }
            else
            {
                spriteBatch.Draw(this.texture2D, Vector2.Zero);

            }
        }
    }
}

