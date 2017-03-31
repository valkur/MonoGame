using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Design;





namespace Jogo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static public Random sRan;
        static public GraphicsDeviceManager sGraphics;
        static public SpriteBatch sSpriteBatch;
        static public ContentManager sContent;

        const int kWindowW = 1000;
        const int kWindowH = 700;

        GameState mMyGame;

        public Game1()
        {
            sGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Game1.sContent = Content;

            Game1.sRan = new Random();

            sGraphics.PreferredBackBufferHeight = kWindowH;
            sGraphics.PreferredBackBufferWidth = kWindowW;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Game1.sSpriteBatch = new SpriteBatch(GraphicsDevice);
            Camera.SetCameraWindow(new Vector2(-10f, -10f), 150f);

            mMyGame = new GameState();
           
            // TODO: use this.Content to load your game content here
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Input.InputWrapper.Buttons.Back==ButtonState.Pressed)this.Exit();

            // TODO: Add your update logic here
           

            mMyGame.UpdateGame();

            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Game1.sSpriteBatch.Begin();

            mMyGame.DrawGame();


            Game1.sSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
