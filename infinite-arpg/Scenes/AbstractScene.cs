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

        protected int width { get { return Game1.ScreenWidth; } }
        protected int height { get { return Game1.ScreenHeight; } }

        private IList<Entity> entities = new List<Entity>();
        private DrawingSystem drawingSystem;

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
            e.scene = this;

            foreach (var system in this.systems)
            {
                if (this.HasRequirementsFor(e, system))
                {
                    system.Add(e);
                }
            }

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

        internal void EntityChanged(Entity e)
        {
            foreach (var system in this.systems)
            {
                if (this.HasRequirementsFor(e, system) && !system.Contains(e))
                {
                    system.Add(e);
                }
                else if (!this.HasRequirementsFor(e, system) && system.Contains(e))
                {
                    system.Remove(e);
                }
            }
        }

        private bool HasRequirementsFor(Entity e, AbstractSystem system)
        {
            var requirements = system.requiredComponents;
            foreach (var type in requirements)
            {
                if (!e.has(type))
                {
                    return false;
                }
            }

            return true;
        }

        private void AddDefaultSystems()
        {
            this.drawingSystem = Game1.Kernel.Get<DrawingSystem>();
            this.systems.Add(this.drawingSystem);
            this.systems.Add(Game1.Kernel.Get<MoveToArrowKeysSystem>());
            this.systems.Add(Game1.Kernel.Get<AabbCollisionSystem>());            
        }
    }
}

