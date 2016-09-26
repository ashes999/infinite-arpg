using System;
using DeenGames.InfiniteArpg.Ecs;
using Microsoft.Xna.Framework;

namespace DeenGames.InfiniteArpg
{
    public class MoveToArrowKeys : Component
    {
        internal int moveSpeed = 0;
        internal Vector2 Velocity = new Vector2();

        public MoveToArrowKeys(Entity parent, int moveSpeedInPixelsPerSecond) : base(parent)
        {
            this.moveSpeed = moveSpeedInPixelsPerSecond;
        }
    }
}

