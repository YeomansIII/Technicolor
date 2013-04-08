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
    class Player : Entity
    {
        public override void LoadContent(ContentManager content, InputManager input)
        {
            base.LoadContent(content, input);
            fileManager = new FileManager();
            moveAnimation = new SpriteSheetAnimation();
            Vector2 tempFrames = Vector2.Zero;
            moveSpeed = 100f;
 
            fileManager.LoadContent("Load/Player.cme", attributes, contents);
            for (int i = 0; i <attributes.Count; i++)
            {
                for (int j = 0; j < attributes<em>.Count; j++)
                {
                    switch (attributes<em>[j])
                    {
                        case "Health":
                            health = int.Parse(contents<em>[j]);
                            break;
                        case "Frames":
                            string[] frames = contents<em>[j].Split(' ');
                            tempFrames = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                            break;
                        case "Image":
                            image = this.content.Load<Texture2D>(contents<em>[j]);
                            break;
                        case "Position":
                            frames = contents<em>[j].Split(' ');
                            position = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                            break;
                    }
                }
            }
 
            moveAnimation.LoadContent(content, image, "", position);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            moveAnimation.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager input, Collision col, Layers layer)
        {
            moveAnimation.IsActive = true;
            if (input.KeyDown(Keys.Right, Keys.D))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 2);
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
            else if (input.KeyDown(Keys.Left, Keys.A))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
            else if (input.KeyDown(Keys.Down, Keys.S))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0);
                position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
            else if (input.KeyDown(Keys.Up, Keys.W))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 3);
                position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            }
            else
                moveAnimation.IsActive = false;
 
            for (int i = 0; i <col.CollisionMap.Count; i++)
            {
                for (int j = 0; j < col.CollisionMap<em>.Count; j++)
                {
                    if (col.CollisionMap<em>[j] == "x")
                    {
                        if (position.X + moveAnimation.FrameWidth < j * layer.TileDimensions.X ||
                            position.X > j * layer.TileDimensions.X + layer.TileDimensions.X ||
                            position.Y + moveAnimation.FrameHeight <i * layer.TileDimensions.Y ||
                            position.Y > i * layer.TileDimensions.Y + layer.TileDimensions.Y)
                        {
                            // no collision
                        }
                        else
                        {
                            position = moveAnimation.Position;
                        }
                    }
                }
            }
 
            moveAnimation.Position = position;
            moveAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
        }
    }
}