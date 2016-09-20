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

        #region fluent API for adding entities

        public Entity Image(string fileName)
        {
            this.components[typeof(Drawable)] = Game1.Kernel.Get<Drawable>().Image(fileName);
            return this;
        }

        public Entity Colour(Color colour, uint width, uint height)
        {
            this.components[typeof(Drawable)] = Game1.Kernel.Get<Drawable>().Colour(colour, width, height);
            return this;
        }
            

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Has<Drawable>())
            {
                this.Get<Drawable>().Draw(spriteBatch);
            }
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
            return this.Get<T>() != null;
        }

        #endregion
    }
}

