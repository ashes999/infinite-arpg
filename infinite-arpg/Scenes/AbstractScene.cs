using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace infinitearpg
{
    public abstract class AbstractScene
    {
        private GraphicsDevice graphicsDevice;

        internal AbstractScene(GraphicsDevice graphicsDevice)
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
    }
}

