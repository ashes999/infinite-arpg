using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.IO;

using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using DeenGames.InfiniteArpg.Scenes;
using System.Reflection;
using Ninject;

namespace DeenGames.InfiniteArpg
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		readonly GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

        private AbstractScene currentScene;
        private readonly ScriptEngine pythonEngine = Python.CreateEngine();
        private const string MainSceneFile = "Content/Scripts/CoreGameScene.py";
        private readonly FileWatcher fileWatcher = new FileWatcher();
        public static StandardKernel Kernel { get; private set; }
           
		public Game1 ()
		{
            Kernel = new StandardKernel();

			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

            // Watch the script file. If it's updated, copy to bin and reload.
            // Not the file in bin. The file in the source directory.
            var sourceFile = string.Format("../../{0}", MainSceneFile);
            fileWatcher.Watch(sourceFile, () =>
            {
                File.Copy(sourceFile, MainSceneFile, true);
                ReloadMainScene();
            });

            this.Exiting += (sender, e) => fileWatcher.Stop = true;

            // 1024x576
            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 576;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();

            // Wire up XNA dependencies so we can get them easily. Bind to instances, because
            // constructing these things is really complex.
            Kernel.Bind<GraphicsDevice>().ToConstant(this.GraphicsDevice);

            this.ReloadMainScene();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(this.GraphicsDevice);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif
                        
			base.Update (gameTime);

            this.currentScene.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
            if (this.currentScene != null)
            {
                this.currentScene.Draw(spriteBatch);
            }

			base.Draw (gameTime);
		}

        private void ReloadMainScene()
        {
            // The definition of our scene class lives in IronPython code.
            // Execute the script that defines it. Then, create an instance of it.

            var scope = this.pythonEngine.CreateScope();

            // Expose the assembly containing AbstractScene to the IronPython runtime
            this.pythonEngine.Runtime.LoadAssembly(typeof(AbstractScene).Assembly);
            this.pythonEngine.Runtime.LoadAssembly(typeof(Vector2).Assembly);

            // Execute the Python code to define our scene type
            var source = this.pythonEngine.CreateScriptSourceFromFile(MainSceneFile);
            try
            {
                source.Execute(scope);
            }
            catch (SyntaxErrorException s)
            {
                throw new SyntaxErrorException(string.Format("{0} on line {1}", s.Message, s.Line), s);
            }

            // Get the Python class type/definition
            var sceneType = scope.GetVariable("CoreGameScene");
            this.currentScene = pythonEngine.Operations.CreateInstance(sceneType, this.GraphicsDevice);
        }
	}
}

