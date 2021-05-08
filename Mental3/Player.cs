using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
namespace Mental3
{
    class Player
    {
        Vector2 position;
        Vector2 velocity;
        Map currentMap;
        Texture2D sprite;
        Rectangle collider;
        float speed;
        public Player(Map m,ContentManager content)
        {
            currentMap = m;
            sprite = content.Load<Texture2D>("Characters/player");
            position = currentMap.getMapRealTilePositionByCoords(8, 7);
            collider.Width = sprite.Width;
            collider.Height = sprite.Height / 2;
            velocity = Vector2.Zero;
            speed = 2;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite,
                            position,
                            null,
                            Color.White,
                            0.0f,
                            new Vector2(0.0f, 0.0f),
                            new Vector2(Game1.gameScaleRatio.X, Game1.gameScaleRatio.Y),
                            SpriteEffects.None,
                            0.0f
                            );
        }

        public void update()
        {
            updateVelocity();
            //anticipate collision then update the velocity
            updatePosition();
        }

        private void updateVelocity()
        {           
            if (KeyboardManager.TriggeredDown(Keys.Right))
            {
                velocity += new Vector2(speed, 0.0f);
            }
            else if (KeyboardManager.TriggeredUp(Keys.Right))
            {
                velocity += new Vector2(-speed, 0.0f);
            }
            if (KeyboardManager.TriggeredDown(Keys.Left))
            {
                velocity += new Vector2(-speed, 0.0f);
            }
            else if (KeyboardManager.TriggeredUp(Keys.Left))
            {
                velocity += new Vector2(speed, 0.0f);
            }
            if (KeyboardManager.TriggeredDown(Keys.Up))
            {
                velocity += new Vector2(0.0f, -speed);
            }
            else if (KeyboardManager.TriggeredUp(Keys.Up))
            {
                velocity += new Vector2(0.0f, speed);
            }
            if (KeyboardManager.TriggeredDown(Keys.Down))
            {
                velocity += new Vector2(0.0f, speed);
            }
            else if (KeyboardManager.TriggeredUp(Keys.Down))
            {
                velocity += new Vector2(0.0f, -speed);
            }
        } 

        private void updatePosition()
        {
            position += velocity;
            collider.Location = position.ToPoint();
        }

        private void detectCollisions()
        {       
            List<Vector2> Tiles  = currentMap.getTilesAroundRectangle(collider);

            
        }
    }
}
