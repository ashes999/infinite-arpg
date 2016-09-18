using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace DeenGames.InfiniteArpg.Scenes
{
    public abstract class AbstractScene
    {
        private GraphicsDevice graphicsDevice;

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
    }
}

