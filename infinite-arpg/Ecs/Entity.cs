using System;
using System.Collections.Generic;
using DeenGames.InfiniteArpg.Ecs.Components;
using Ninject;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeenGames.InfiniteArpg.Ecs
{
    public class Entity
    {
        // Map of type => component instance, eg. typeof(Drawable) => Drawable instance
        public IDictionary<Type, dynamic> components = new Dictionary<Type, dynamic>();

        #region fluent API for adding components

        public Entity Image(string fileName)
        {
            this.components[typeof(Drawable)] = Game1.Kernel.Get<Drawable>().Image(fileName);
            return this;
        }

        public Entity Colour(Color colour, int width, int height)
        {
            this.components[typeof(Drawable)] = Game1.Kernel.Get<Drawable>().Colour(colour, width, height);
            return this;
        }
            
        public Entity Move(int x, int y)
        {
            var drawable = this.Get<Drawable>();
            if (drawable != null)
            {
                drawable.X = x;
                drawable.Y = y;
            }

            return this;
        }

        public Entity MoveToArrowKeys(int velocityPerSecond)
        {
            this.components[typeof(MoveToArrowKeys)] = new MoveToArrowKeys(this, velocityPerSecond);
            return this;
        }

        #endregion

        #region raw Get/Has/etc. methods

        public T Get<T>()
        {
            var type = typeof(T);
            return (T)this.components[type];
        }

        public bool Has<T>()
        {
            return this.components[typeof(T)] != null;
        }

        internal void Update(GameTime gameTime)
        {
            foreach (var kvp in this.components)
            {
                kvp.Value.Update(gameTime);
            }
        }

        #endregion

        #region TODO: refactor

        // The exact same code, in Python, either returns a Tuple or True.
        // Instead of a Drawable instance. Pfft.
        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Has<Drawable>())
            {
                this.Get<Drawable>().Draw(spriteBatch);
            }
        }

        #endregion
    }
}

