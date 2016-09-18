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

		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize ();

            /// Create our scene through IronPython code

            var scope = this.pythonEngine.CreateScope();

            // Expose the assembly containing AbstractScene to the IronPython runtime
            this.pythonEngine.Runtime.LoadAssembly(typeof(AbstractScene).Assembly);
            this.pythonEngine.Runtime.LoadAssembly(typeof(Vector2).Assembly);

            // Execute the Python code to define our scene type
            var source = this.pythonEngine.CreateScriptSourceFromFile("Scripts/CoreGameScene.py");
            source.Execute(scope);

            // Get the Python class type/definition
            var sceneType = scope.GetVariable("CoreGameScene");
            this.currentScene = pythonEngine.Operations.CreateInstance(sceneType, this.GraphicsDevice);
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
            
			// TODO: Add your update logic here
            
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
            graphics.GraphicsDevice.Clear (Color.Black);
            
            if (this.currentScene != null)
            {
                spriteBatch.Begin();
                this.currentScene.Draw(spriteBatch);
                spriteBatch.End();
            }
            
			base.Draw (gameTime);
		}
	}
}

