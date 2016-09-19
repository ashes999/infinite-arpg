using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DeenGames.InfiniteArpg.Ecs.Components
{
    public class Drawable
    {
        public int X { get; set; }
        public int Y { get;set; }

        private bool isColour = false;
        private Texture2D texture2D;
        private int width = 0;
        private int height = 0;
        private Vector2 position = Vector2.Zero;

        public Drawable()
        {
            this.X = 0;
            this.Y = 0;
        }

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

        public Drawable Move(int x, int y)
        {
            this.X = x;
            this.Y = y;
            return this;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.isColour == true)
            {
                spriteBatch.Draw(this.texture2D, null, new Rectangle(this.X, this.Y, this.width, this.height));
            }
            else
            {
                // TODO: cache or something the vector
                spriteBatch.Draw(this.texture2D, new Vector2(this.X, this.Y));

            }
        }

    }
}

