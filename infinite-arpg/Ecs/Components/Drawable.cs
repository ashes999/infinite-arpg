using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Ninject;
using DeenGames.InfiniteArpg.Scenes;

namespace DeenGames.InfiniteArpg.Ecs.Components
{
    public class Drawable : Component
    {
        internal double X { get; set; }
        internal double Y { get; set; }

        internal Color? color = null;
        internal Texture2D texture2D;
        internal int width = 0;
        internal int height = 0;

        public Drawable(Entity parent, string fileName) : base(parent)
        {
            this.X = 0;
            this.Y = 0;

            var graphicsDevice = Game1.Kernel.Get<GraphicsDevice>();

            using (var stream = File.Open(fileName, FileMode.Open))
            {
                this.texture2D = Texture2D.FromStream(graphicsDevice, stream);
            }
        }

        public Drawable(Entity parent, Color color, int width, int height) : base(parent)
        {
            this.X = 0;
            this.Y = 0;

            this.color = color;
            this.texture2D = AbstractScene.WhiteTexture;
            this.width = width;
            this.height = height;
        }
    }
}

