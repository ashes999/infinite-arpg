using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Ninject;

namespace DeenGames.InfiniteArpg.Ecs.Components
{
    public class Drawable : Component
    {
        [Inject]
        public GraphicsDevice GraphicsDevice { private get; set; }

        public int X { get; set; }
        public int Y { get;set; }

        private bool isColour = false;
        private Texture2D texture2D;
        private int width = 0;
        private int height = 0;

        public Drawable()
        {
            this.X = 0;
            this.Y = 0;
        }

        public Drawable Image(string fileName)
        {
            this.isColour = false;

            using (var stream = File.Open(fileName, FileMode.Open))
            {
                this.texture2D = Texture2D.FromStream(this.GraphicsDevice, stream);
            }

            return this;
        }


        public Drawable Colour(Color colour, int width, int height)
        {
            this.isColour = true;

            this.texture2D = new Texture2D(this.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            this.texture2D.SetData<Color>(new Color[] { colour });
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
                // TODO: cache (or something) the vector
                spriteBatch.Draw(this.texture2D, new Vector2(this.X, this.Y));
            }
        }
    }
}

