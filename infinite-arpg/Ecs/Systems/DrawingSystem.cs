using System;
using DeenGames.InfiniteArpg.Ecs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeenGames.InfiniteArpg
{
    public class DrawingSystem : AbstractSystem
    {
        private readonly SpriteBatch spriteBatch;
        private readonly GraphicsDevice graphicsDevice;
        
        public DrawingSystem(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice) : base(typeof(Drawable))
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
        }

        override public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, Color clearColour)
        {
            graphicsDevice.Clear(clearColour);
            spriteBatch.Begin();

            foreach (var entity in this.entities)
            {
                var drawable = entity.get<Drawable>();

                if (drawable.color.HasValue)
                {
                    spriteBatch.Draw(drawable.texture2D, null,
                        new Rectangle((int)drawable.X, (int)drawable.Y, drawable.width, drawable.height),
                        null, null, 0, Vector2.One, drawable.color);
                }
                else
                {
                    // TODO: cache (or something) the vector
                    spriteBatch.Draw(drawable.texture2D, new Vector2((int)drawable.X, (int)drawable.Y));
                }
            }

            spriteBatch.End();
        }
    }
}

