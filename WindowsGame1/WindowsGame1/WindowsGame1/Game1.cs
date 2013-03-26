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
        Texture2D Dude,CrouchDude,TestMap, WhiteTexture;
        Vector2 MySpritePosition = Vector2.Zero;
        bool W = false, A = false, S = false, D = false;
        string dir = "right";
        int gravity = 5, surface = 369, jump = 80;
        bool canJump = true, jumping = false;
        MouseState m;
        Rectangle dudeBox, basePlat,firstPlat,secPlat,thiPlat,fourPlat,fivePlat,sixPlat;
        List<Rectangle> platList;
        Color color = Color.White;

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
            graphics.PreferredBackBufferWidth = 896;
            graphics.ApplyChanges();

            dudeBox = new Rectangle((int)MySpritePosition.X,(int)MySpritePosition.Y,50,95);
           //basePlat = new Rectangle(0, 464, 896, 16);
            firstPlat = new Rectangle(192, 448, 64, 16);
            secPlat = new Rectangle(256, 432, 64, 16);
            thiPlat = new Rectangle(320, 416, 64, 16);
            fourPlat = new Rectangle(384, 400, 48, 16);
            fivePlat = new Rectangle(432, 384, 32, 16);
            sixPlat = new Rectangle(480, 368, 32, 16);

            platList = new List<Rectangle>();
            //platList.Add(basePlat);
            platList.Add(firstPlat);
            platList.Add(secPlat);
            platList.Add(thiPlat);
            platList.Add(fourPlat);
            platList.Add(fivePlat);
            platList.Add(sixPlat);

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

            WhiteTexture = Content.Load<Texture2D>("pixel");
            TestMap = Content.Load<Texture2D>("TestMap");
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

            dudeBox.X = (int)MySpritePosition.X;
            dudeBox.Y = (int)MySpritePosition.Y;

            for (int i = 0; i < platList.Count; i++)
            {
                if (dudeBox.Intersects(platList[i]))
                {
                   
                    Console.WriteLine("Intersected"+ i);
                    surface = platList[i].Top - 95;
                    jump = surface - 80;
                    Console.WriteLine("Surface:1 " + surface);
                }
                else if(!dudeBox.Intersects(platList[i]))
              {
                    surface = 369;
                }
            }
            Console.WriteLine("Surface 2: " + surface);


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.W) || (m.LeftButton == ButtonState.Pressed && (m.Y < MySpritePosition.Y - 110))) { W = true; /*MySpritePosition.Y -= 5;*/ }
            else { W = false; }
            if (keyboard.IsKeyDown(Keys.S) || (m.LeftButton == ButtonState.Pressed && (m.Y > MySpritePosition.Y && (m.X > MySpritePosition.X && m.X < MySpritePosition.X + 50)))) { surface = 369;  S = true;  /*MySpritePosition.Y += 5;*/ }
            else { S = false; }
            if (keyboard.IsKeyDown(Keys.A) || (m.LeftButton == ButtonState.Pressed && (m.X < MySpritePosition.X))) { dir = "left"; A = true; MySpritePosition.X -= 5; }
            else { A = false; }
            if (keyboard.IsKeyDown(Keys.D) || (m.LeftButton == ButtonState.Pressed && (m.X > MySpritePosition.X+50))) { dir = "right"; D = true; MySpritePosition.X += 5; }
            else { D = false; }

            if (W && canJump)
            {
                jumping = true;
            }
            if (jumping)
                {
                    if (MySpritePosition.Y > jump)
                    {
                        W = false;
                        MySpritePosition.Y -= gravity + 5;
                        canJump = false;
                    }
                    else
                    { jumping = false; }
                }
            
            else if (MySpritePosition.Y < surface)
            { MySpritePosition.Y += gravity; }
            if (MySpritePosition.Y >= surface)
            {
                
                canJump = true; }


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

            spriteBatch.Draw(TestMap, new Vector2(0, 0), Color.White);

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
