using System;
using System.Collections.Generic;
using DeenGames.InfiniteArpg.Ecs;
using Microsoft.Xna.Framework;

namespace DeenGames.InfiniteArpg
{
    public abstract class AbstractSystem
    {
        private Type[] requiredComponents;
        protected List<Entity> entities = new List<Entity>();

        // Create a new AbstractSystem, specifying what components this system is interested in.
        // Only entities with all matching components will be added to this system.
        public AbstractSystem(params Type[] requiredComponents)
        {
            this.requiredComponents = requiredComponents;
        }

        // Add an entity to this system. By necessity, this entity has all of the components
        // specified as requiredComponents in this system's constructor.
        public virtual void Add(Entity entity)
        {
            this.entities.Add(entity);
        }

        public abstract void Update(GameTime gameTime);
    }
}

