using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Microsoft.Xna.Framework;
using DeenGames.InfiniteArpg.Ecs;
using System.Collections.Generic;
using DeenGames.InfiniteArpg.Ecs.Components;
using Ninject;

namespace DeenGames.InfiniteArpg.Scenes
{
    public abstract class AbstractScene
    {

        internal static Texture2D WhiteTexture;

        protected Color clearColour { get; set; }
        protected GraphicsDevice graphicsDevice;
        protected List<AbstractSystem> systems = new List<AbstractSystem>();
        private DrawingSystem drawingSystem;

        protected int width { get { return Game1.ScreenWidth; } }
        protected int height { get { return Game1.ScreenHeight; } }

        private IList<Entity> entities = new List<Entity>();

        public AbstractScene(GraphicsDevice graphicsDevice)
        {
            this.AddDefaultSystems();
            
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

        internal void Draw(GameTime gameTime)
        {
            this.drawingSystem.Draw(gameTime, this.clearColour);
        }

        internal void Update(GameTime gameTime)
        {
            foreach (var system in this.systems)
            {
                system.Update(gameTime);
            }
        }

        private void AddDefaultSystems()
        {
            this.drawingSystem = Game1.Kernel.Get<DrawingSystem>();
            this.systems.Add(Game1.Kernel.Get<MoveToArrowKeysSystem>());
            this.systems.Add(Game1.Kernel.Get<AabbCollisionSystem>());            
        }
    }
}

