using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using NCodeRiddian;
using DataLoader;

namespace FP1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameScreenManager ScreenManager;
        LCamera GlobalCamera;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            PropertyObject ScreendataObject = PropertyObject.Load("SCRDAT");
            bool FullScr = ScreendataObject.GetProperty("Full").value<int>() > 0;
            if (FullScr)
            {
                graphics.ToggleFullScreen();
            }
            else
            {
                graphics.PreferredBackBufferWidth = ScreendataObject.GetProperty("SCREEN_X").value<int>();
                graphics.PreferredBackBufferHeight = ScreendataObject.GetProperty("SCREEN_Y").value<int>();
            }
            Settings.Screen_P = new Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

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
            Components.Add(new TimerComponent(this));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            TextureManager.setDebug(true);

            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.loadAllImages(Content);
            GlobalCamera = new LCamera(graphics.GraphicsDevice, spriteBatch, Settings.TargetRectangle, Settings.GP_P);
            Camera.setupCamera(Settings.GP_P);
            Camera.setupGenericTexture(GraphicsDevice);
            SafeImage.SetPlaceholder(new Image("phld"));
            TemporaryPlayer.LOAD();
            Screen.Load(Content);
            Settings.Load();
            ScreenManager = new GameScreenManager();
            Screens.MinigameScreen.Load(Content);

            //DEBUGGING:
            
            GameManager.Setup(new Player[] { new Player("TST P", Difficulty.NON_COMP, PlayerIndex.One), 
                new Player("CMP E", Difficulty.Easy, PlayerIndex.Two),
                new Player("CMP M", Difficulty.Medium, PlayerIndex.Three),
                new Player("CMP H", Difficulty.Hard, PlayerIndex.Four)});
            GameManager.ChangeP1(GameManager.Players[0], 0);
            ScreenManager.ChangeScreen(new Screens.MinigameScreen(
                Screens.MinigameScreen.AllMinigames[1], // Minigame goes here 
                GameManager.Players));
            
            // END DEBUGGING
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        protected override void OnExiting(Object sender, EventArgs args)
        {

            // Save PrvName list
            PropertyObject probject = new PropertyObject();
            foreach (string s in Settings.PrvNames)
            {
                probject.AddDefinition(new PropertyDefinition("name", new StringProperty(s)));
            }
            probject.WriteToContentFile("PrevNames");
            base.OnExiting(sender, args);

            // Stop the threads
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            GlobalCamera.Begin();

            ScreenManager.Draw(gameTime, spriteBatch);

            GlobalCamera.End();
            base.Draw(gameTime);
        }
    }
}
