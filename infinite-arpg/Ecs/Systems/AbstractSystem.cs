using System;
using System.Collections.Generic;
using DeenGames.InfiniteArpg.Ecs;
using Microsoft.Xna.Framework;

namespace DeenGames.InfiniteArpg
{
    public abstract class AbstractSystem
    {
        internal Type[] requiredComponents { get; private set; }
        protected List<Entity> entities = new List<Entity>();

        // Create a new AbstractSystem, specifying what components this system is interested in.
        // Only entities with all matching components will be added to this system.
        public AbstractSystem(params Type[] requiredComponents)
        {
            this.requiredComponents = requiredComponents;
        }

        // Add an entity to this system. By necessity, this entity has all of the components
        // specified as requiredComponents in this system's constructor.
        public virtual void Add(Entity e)
        {
            this.entities.Add(e);
        }

        public virtual void Remove(Entity e)
        {
            this.entities.Remove(e);
        }

        internal bool Contains(Entity e)
        {
            return this.entities.Contains(e);
        }

        public abstract void Update(GameTime gameTime);
    }
}

