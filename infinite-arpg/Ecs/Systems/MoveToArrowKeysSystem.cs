using System;
using System.Collections.Generic;
using DeenGames.InfiniteArpg.Ecs;
using DeenGames.InfiniteArpg.Ecs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DeenGames.InfiniteArpg
{
    public class MoveToArrowKeysSystem : AbstractSystem
    {
        public MoveToArrowKeysSystem() : base(typeof(MoveToArrowKeys))
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in entities)
            {
                // Guaranteed to exist; we add one if missing in Add(Entity)
                var drawable = entity.get<Drawable>();
                
                var mover = entity.get<MoveToArrowKeys>();
                this.SetVelocityIfKeysPressed(mover);
                drawable.X += (mover.Velocity.X * gameTime.ElapsedGameTime.TotalSeconds);
                drawable.Y += (mover.Velocity.Y * gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        private void SetVelocityIfKeysPressed(MoveToArrowKeys mover)
        {
            var state = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
            {
                mover.Velocity.X = -mover.moveSpeed;
            }
            else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                mover.Velocity.X = mover.moveSpeed;
            }
            else
            {
                mover.Velocity.X = 0;
            }

            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
            {
                mover.Velocity.Y = -mover.moveSpeed;
            }
            else if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                mover.Velocity.Y = mover.moveSpeed;
            }
            else
            {
                mover.Velocity.Y = 0;
            }
        }
    }
}

