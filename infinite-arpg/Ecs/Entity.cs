using System;
using System.Collections.Generic;
using System.Linq;

using DeenGames.InfiniteArpg.Ecs.Components;

using Ninject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ninject.Parameters;
using DeenGames.InfiniteArpg.Scenes;

namespace DeenGames.InfiniteArpg.Ecs
{
    public class Entity
    {
        internal AbstractScene scene { get; set; }
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
            else
            {
                throw new InvalidOperationException("Can't move something that doesn't have a Drawable.");
            }

            return this;
        }

        public Entity move_to_arrow_keys(int velocityPerSecond)
        {
            if (!this.has<Drawable>())
            {
                this.color(Color.Red, 32, 32);
            }

            this.add(new MoveToArrowKeys(this, velocityPerSecond));
            return this;
        }

        #endregion

        #region raw Get/Has/etc. methods

        public T get<T>()
        {
            var type = typeof(T);
            if (!this.has<T>())
            {
                throw new ArgumentException(string.Format("Entity doesn't have an instance of {0}; it only has: {1}", type.FullName, string.Join(",", this.components.Keys.Select(s => s.FullName))));
            }
            return (T)this.components[type];
        }

        // Adds a component to this entity. If this entity already has a component of that
        // type, this overrides it with the new (parameter) component.
        public Entity add(Component component)
        {
            var type = component.GetType();
            this.components[type] = component;
            if (this.scene != null)
            {
                this.scene.EntityChanged(this);
            }
            return this;
        }

        public bool has<T>()
        {
            return this.components.ContainsKey(typeof(T));
        }

        public bool tagged(string tag)
        {
            return this.tags.Any(t => t == tag.ToUpperInvariant());
        }

        internal bool has(Type t)
        {
            return this.components.ContainsKey(t);
        }
        #endregion
    }
}

