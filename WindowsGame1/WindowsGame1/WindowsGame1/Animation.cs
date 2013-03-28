using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    public class Animation
    {
        protected Texture2D image;
        protected string text;
        protected SpriteFont font;
        protected Color color;
        protected Rectangle sourceRect;
        float rotation, scale, axis;
        Vector2 origin, position;

        public void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            this.image = image;
            this.text = text;
            this.position = position;
            if (text != String.Empty)
            {
                font = Content.Load<SpriteFont>("AnimationFont");
                color = new Color(114, 77, 255);
            }
            if (image != null)
                sourceRect = new Rectangle(0, 0, image.Width, image.Height);
            rotation = 0.0f;
            axis = 0.0f;
            scale = 1.0f;
        }

        public void UnloadContent()
        {
            image = position = font = color = sourceRect = null;
        }
    }
}
