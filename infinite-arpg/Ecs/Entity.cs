﻿using System;
using System.Collections.Generic;
using System.Linq;

using DeenGames.InfiniteArpg.Ecs.Components;

using Ninject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeenGames.InfiniteArpg.Ecs
{
    public class Entity
    {
        // Map of type => component instance, eg. typeof(Drawable) => Drawable instance
        protected IDictionary<Type, dynamic> components = new Dictionary<Type, dynamic>();
        private string[] tags = new string[0];

        public Entity(string tags = "")
        {
            if (!string.IsNullOrEmpty(tags))
            {
                this.tags = tags.Split(',').Select(s => s.ToUpperInvariant()).ToArray();
            }
        }

        #region fluent API for adding components

        public Entity image(string fileName)
        {
            this.components[typeof(Drawable)] = Game1.Kernel.Get<Drawable>().Image(fileName);
            return this;
        }

        public Entity color(Color colour, int width, int height)
        {
            this.components[typeof(Drawable)] = Game1.Kernel.Get<Drawable>().Colour(colour, width, height);
            return this;
        }
            
        public Entity move(int x, int y)
        {
            var drawable = this.get<Drawable>();
            if (drawable != null)
            {
                drawable.X = x;
                drawable.Y = y;
            }
            // TODO: what if Drawable is null?

            return this;
        }

        public Entity move_to_arrow_keys(int velocityPerSecond)
        {
            this.components[typeof(MoveToArrowKeys)] = new MoveToArrowKeys(this, velocityPerSecond);
            return this;
        }

        #endregion

        #region raw Get/Has/etc. methods

        public T get<T>()
        {
            var type = typeof(T);
            return (T)this.components[type];
        }

        public bool has<T>()
        {
            return this.components[typeof(T)] != null;
        }

        public bool tagged(string tag)
        {
            return this.tags.Any(t => t == tag.ToUpperInvariant());
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
        public void draw(SpriteBatch spriteBatch)
        {
            if (this.has<Drawable>())
            {
                this.get<Drawable>().Draw(spriteBatch);
            }
        }

        #endregion
    }
}

