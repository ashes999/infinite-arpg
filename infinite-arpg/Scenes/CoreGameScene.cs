using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace infinitearpg
{
    public class CoreGameScene : AbstractScene
    {
        private readonly Texture2D player;

        public CoreGameScene(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            this.player = this.LoadImage("Content/player.png");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(player, new Vector2(300, 100));
        }
    }
}

