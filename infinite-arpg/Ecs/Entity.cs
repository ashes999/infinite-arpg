using System;
using System.Collections.Generic;
using System.Linq;

using DeenGames.InfiniteArpg.Ecs.Components;

using Ninject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ninject.Parameters;

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
            this.add(Game1.Kernel.Get<Drawable>(
                new ConstructorArgument("parent", Game1.Kernel), 
                new ConstructorArgument("fileName", fileName)));

            return this;
        }

        public Entity color(Color color, int width, int height)
        {
            this.add(Game1.Kernel.Get<Drawable>(
                new ConstructorArgument("parent", this),
                new ConstructorArgument("color", color),
                new ConstructorArgument("width", width),
                new ConstructorArgument("height", height)));

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
            this.add(new MoveToArrowKeys(this, velocityPerSecond));
            return this;
        }

        #endregion

        #region raw Get/Has/etc. methods

        public T get<T>()
        {
            var type = typeof(T);
            return (T)this.components[type];
        }

        // Adds a component to this entity. If this entity already has a component of that
        // type, this overrides it with the new (parameter) component.
        public Entity add(Component component)
        {
            var type = component.GetType();
            this.components[type] = component;
            return this;
        }

        public bool has<T>()
        {
            return this.components[typeof(T)] != null;
        }

        public bool tagged(string tag)
        {
            return this.tags.Any(t => t == tag.ToUpperInvariant());
        }
        
        #endregion
    }
}

