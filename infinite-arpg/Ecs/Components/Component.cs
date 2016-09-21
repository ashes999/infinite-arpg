using System;
using DeenGames.InfiniteArpg.Ecs;
using Microsoft.Xna.Framework;

namespace DeenGames.InfiniteArpg
{
    public abstract class Component
    {
        public Entity Parent { get; private set; }

        public Component(Entity parent)
        {
            this.Parent = parent;
        }

        public virtual void Update(GameTime gameTime) { }
    }
}

