using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using WarnerEngine.Services;
using WarnerEngine.Services.Implementations;

namespace Warlocked
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class WarlockedGame : Game
    {
        GraphicsDeviceManager graphics;

        public WarlockedGame()
        {
            GameService.Initialize();

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GameService.GetService<IRenderService>()
                .SetGraphicsDevice(GraphicsDevice)
                .SetInternalResolution(640, 360);

            GameService.GetService<IContentService>()
                .Bootstrap(Content, GraphicsDevice)
                .LoadAllContent();

            GameService.GetService<ISceneService>()
                .RegisterScene(Scenes.PlayField.SceneKey, new Scenes.PlayField())
                .SetScene(Scenes.PlayField.SceneKey);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float DT = Math.Min((float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f, 0.033f);

            GameService.GetService<IStateService>().SetGlobalGameTime((float)gameTime.TotalGameTime.TotalMilliseconds);
            GameService.GetService<IStateService>().IncrementGlobalFrameCount();

            GameService.PreDraw(DT);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GameService.Draw();
            GameService.PostDraw();

            base.Draw(gameTime);
        }
    }
}
