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

        internal static Texture2D WhiteTexture;

        protected Color clearColour { get; set; }
        protected GraphicsDevice graphicsDevice;
        protected List<AbstractSystem> systems = new List<AbstractSystem>();

        private IList<Entity> entities = new List<Entity>();

        public AbstractScene(GraphicsDevice graphicsDevice)
        {
            this.systems.Add(new MoveToArrowKeysSystem());
            this.systems.Add(new AabbCollisionSystem());
            this.systems.Add(new DrawableSystem());
            
            this.graphicsDevice = graphicsDevice;

            if (WhiteTexture == null)
            {
                WhiteTexture = new Texture2D(this.graphicsDevice, 1, 1, false, SurfaceFormat.Color);
                WhiteTexture.SetData<Color>(new Color[] { Color.White });
            }
        }

        public Entity add(Entity e)
        {
            this.entities.Add(e);
            return e;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(this.clearColour);
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

        internal void Update(GameTime gameTime)
        {
            foreach (var entity in this.entities)
            {
                entity.Update(gameTime);
            }
        }

        protected int Width { get { return Game1.ScreenWidth; } }
        protected int Height { get { return Game1.ScreenHeight; } }
    }
}

