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
        private Vector2 Velocity = new Vector2();

        // If we don't have a drawable, add one
        public MoveToArrowKeys(Entity parent) : base(parent)
        {
            if (!parent.Has<Drawable>())
            {
                parent.colour(Color.White, 32, 32);
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
                this.SetVelocityIfKeysPressed();
                drawable.X += (this.Velocity.X * gameTime.ElapsedGameTime.TotalSeconds);
                drawable.Y += (this.Velocity.Y * gameTime.ElapsedGameTime.TotalSeconds);
            }
            base.Update(gameTime);
        }

        private void SetVelocityIfKeysPressed()
        {
            var state = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
            {
                this.Velocity.X = -this.moveSpeed;
            }
            else if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
            {
                this.Velocity.X = this.moveSpeed;
            }
            else
            {
                this.Velocity.X = 0;
            }

            if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
            {
                this.Velocity.Y = -this.moveSpeed;
            }
            else if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
            {
                this.Velocity.Y = this.moveSpeed;
            }
            else
            {
                this.Velocity.Y = 0;
            }
        }
    }
}

