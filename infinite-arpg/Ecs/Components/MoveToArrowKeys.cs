using System;
using DeenGames.InfiniteArpg.Ecs;
using DeenGames.InfiniteArpg.Ecs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DeenGames.InfiniteArpg
{
    public class MoveToArrowKeys : Component
    {
        private int moveSpeed = 0;

        // If we don't have a drawable, add one
        public MoveToArrowKeys(Entity parent) : base(parent)
        {
            if (!parent.Has<Drawable>())
            {
                parent.Colour(Color.White, 32, 32);
            }
        }

        public MoveToArrowKeys(Entity parent, int moveSpeedInPixelsPerSecond) : base(parent)
        {
            this.moveSpeed = moveSpeedInPixelsPerSecond;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            var drawable = this.Parent.Get<Drawable>();
            if (drawable != null)
            {
                var state = Microsoft.Xna.Framework.Input.Keyboard.GetState();
                if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
                {
                    drawable.Y -= gameTime.ElapsedGameTime.TotalSeconds * this.moveSpeed;
                }
                else if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
                {
                    drawable.Y += gameTime.ElapsedGameTime.TotalSeconds * this.moveSpeed;
                }
                if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
                {
                    drawable.X -= gameTime.ElapsedGameTime.TotalSeconds * this.moveSpeed;
                }
                else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
                {
                    drawable.X += gameTime.ElapsedGameTime.TotalSeconds * this.moveSpeed;
                }
            }
            base.Update(gameTime);
        }
    }
}

