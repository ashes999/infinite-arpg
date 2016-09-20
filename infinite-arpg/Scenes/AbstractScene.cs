using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;
using DeenGames.InfiniteArpg.Ecs;
using System.Collections.Generic;
using DeenGames.InfiniteArpg.Ecs.Components;

namespace DeenGames.InfiniteArpg.Scenes
{
    public abstract class AbstractScene
    {
        public Color ClearColour { get; protected set; }
        protected GraphicsDevice graphicsDevice;
        private IList<Entity> entities = new List<Entity>();

        public AbstractScene(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public void Add(Entity e)
        {
            this.entities.Add(e);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(this.ClearColour);
            spriteBatch.Begin();

            foreach (var entity in this.entities)
            {
                var drawable = entity.Get<Drawable>();
                if (drawable != null)
                {
                    drawable.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
        }

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

