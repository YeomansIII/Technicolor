using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1
{
    public class ScreenManager
    {

        #region Variables

        /// <summary>
        /// Creating custom contentManager
        /// </summary>
        ContentManager content;

        /// <summary>
        ///The current screen that is beign displayed
        /// </summary>
        GameScreen currentScreen;

        /// <summary>
        /// The new screen that wil lbe taking effect
        /// </summary>
        GameScreen newScreen;

        /// <summary>
        /// ScreenManager Instance
        /// </summary>

        private static ScreenManager instance;

        /// <summary>
        /// Screen Stack
        /// </summary>
      
        Stack<GameScreen> screenStack = new Stack<GameScreen>();

        /// <summary>
        /// Screens width and height
        /// </summary>
        
        Vector2 dimensions;

        bool transition;

        FadeAnimation fade;
        Texture2D fadeTexture;

        #endregion

        #region Properties

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }

        #endregion

        #region Main Methods

        public void AddScreen(GameScreen screen)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.Alpha = 1.0f;
            fade.ActivateValue = 1.0f;
        }

        public void Initialize() {
            currentScreen = new TitleScreen();
            fade = new FadeAnimation();
        }
        public void LoadContent(ContentManager Content) {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content);
            fadeTexture = Content.Load<Texture2D>("fade");
            fade.LoadContent(content, fadeTexture, "", Vector2.Zero);
            fade.Scale = dimensions.X;
        }
        public void Update(GameTime gameTime) {
            if (!transition)
                currentScreen.Update(gameTime);
            else
            {
                Console.WriteLine("Tranitioning..");
                Transition(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch) {
            currentScreen.Draw(spriteBatch);
            if (transition)
                fade.Draw(spriteBatch);
        }

        #endregion

        #region Private Methods

        private void Transition(GameTime gameTime)
        {
            fade.Update(gameTime);
            if (fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                Console.WriteLine("Transition to: " + newScreen);
                screenStack.Push(newScreen);
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent(content);
            }
            else if (fade.Alpha == 0.0f) {
                Console.WriteLine("Not transitioning");
                transition = false;
                fade.IsActive = false;
            }

        }

        #endregion

    }
}
