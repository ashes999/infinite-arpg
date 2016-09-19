using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;

namespace DeenGames.InfiniteArpg.Scenes
{
    public abstract class AbstractScene
    {
        public Color ClearColour { get; protected set; }
        protected GraphicsDevice graphicsDevice;

        public AbstractScene(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public abstract void Draw(SpriteBatch spriteBatch);

        protected Texture2D LoadImage(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                return Texture2D.FromStream(this.graphicsDevice, stream);
            }
        }

        protected Texture2D Colour(Color fillColour)
        {
            var toReturn = new Texture2D(this.graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            toReturn.SetData<Color>(new Color[] { fillColour });
            return toReturn;
        }
    }
}

