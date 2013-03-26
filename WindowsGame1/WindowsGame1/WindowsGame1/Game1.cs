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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D Dude,CrouchDude;
        Vector2 MySpritePosition = Vector2.Zero;
        bool W = false, A = false, S = false, D = false;
        string dir = "right";
        int gravity = 2, jump = 50, surface = 385;
        bool click = false;
        bool canJump = true;
        MouseState m;

        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 854;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            // TODO: Add your initialization logic here
            MySpritePosition.Y = 385;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Dude = Content.Load<Texture2D>("dude");
            CrouchDude = Content.Load<Texture2D>("cdude");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            m = Mouse.GetState();

            
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W) || (m.LeftButton == ButtonState.Pressed && (m.Y < MySpritePosition.Y - 110))) { W = true; /*MySpritePosition.Y -= 5;*/ }
            else { W = false; }
            if (keyboard.IsKeyDown(Keys.S) || (m.LeftButton == ButtonState.Pressed && (m.Y > MySpritePosition.Y && (m.X > MySpritePosition.X && m.X<MySpritePosition.X+50)))) { S = true;  /*MySpritePosition.Y += 5;*/ }
            else { S = false; }
            if (keyboard.IsKeyDown(Keys.A) || (m.LeftButton == ButtonState.Pressed && (m.X < MySpritePosition.X))) { dir = "left"; A = true; MySpritePosition.X -= 5; }
            else { A = false; }
            if (keyboard.IsKeyDown(Keys.D) || (m.LeftButton == ButtonState.Pressed && (m.X > MySpritePosition.X+50))) { dir = "right"; D = true; MySpritePosition.X += 5; }
            else { D = false; }

            if (MySpritePosition.Y < surface)
            { MySpritePosition.Y += gravity; }
            else
            { if (MySpritePosition.Y == surface)
                { canJump = true; } }
            if (W) {
                if (canJump) {
                    W = false;
                    MySpritePosition.Y -= jump;
                    canJump = false; } }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (dir.Equals("right") && S==false)
            { spriteBatch.Draw(Dude, MySpritePosition, Color.White); }
            else if (dir.Equals("left") && S==false)
            { spriteBatch.Draw(Dude, MySpritePosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0); }
            if (S == true && dir.Equals("right"))
            { spriteBatch.Draw(CrouchDude, MySpritePosition, Color.White); }
            if (S == true && dir.Equals("left"))
            { spriteBatch.Draw(CrouchDude, MySpritePosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0); }
            //else { spriteBatch.Draw(Dude, MySpritePosition, Color.White); }


            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
