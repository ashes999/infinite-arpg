using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;

namespace infinitearpg
{
    public class CoreGameScene : AbstractScene
    {
        private readonly Texture2D player;
        private readonly ScriptEngine pythonEngine = Python.CreateEngine();

        public CoreGameScene(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {
            this.player = this.LoadImage("Content/player.png");
            var source = this.pythonEngine.CreateScriptSourceFromString("print('HI!!!!!')");
            source.Execute();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(player, new Vector2(300, 100));
        }
    }
}

