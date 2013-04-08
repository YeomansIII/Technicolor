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
    public class SplashScreen : GameScreen
    {
        SpriteFont font;
        List<FadeAnimation> fade;
        List<Texture2D> images;

        FileManager fileManager;
        int imageNumber;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("Font1");
             
            imageNumber = 0;
            fileManager = new FileManager();
            fade = new List<FadeAnimation>();
            images = new List<Texture2D>();
 
            fileManager.LoadContent("Load/Splash.cme", attributes, contents);
 
            for (int i = 0; i <attributes.Count; i++)
            {
                for (int j = 0; j < attributes<em>.Count; j++)
                {
                    switch (attributes<em>[j])
                    {
                        case "Image":
                            images.Add(this.content.Load<Texture2D>(contents<em>[j]));
                            fade.Add(new FadeAnimation());
                            break;
                    }
                }
            }
 
            for (int i = 0; i <attributes.Count; i++)
            {
                fade<em>.LoadContent(content, images<em>, "", new Vector2(ScreenManager.Instance.Dimensions.X / 2 - images<em>.Width / 2, ScreenManager.Instance.Dimensions.Y / 2 - images<em>.Height / 2));
                fade<em>.Scale = 1.0f;
                fade<em>.IsActive = true;
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            fade[imageNumber].Update(gameTime);

            if (fade[imageNumber].Alpha == 0.0f)
                imageNumber++;

            if (imageNumber >= fade.Count - 1 || inputManager.KeyPressed(Keys.Escape))
            {
                if (fade[imageNumber].Alpha != 1.0f)
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager, fade[imageNumber].Alpha);
                else
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fade[imageNumber].Draw(spriteBatch);
        }
    }
}