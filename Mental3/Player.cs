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
        enum Direction
        {
            left,
            right,
            top,
            bottom
        }
        Direction direction;
        Vector2 position;
        Vector2 directionVector;
        private Vector2 velocity;
        Map currentMap;
        Texture2D sprite;
        myRectangle collider;
        int speed;

        public Player(Map m,ContentManager content)
        {                 
            currentMap = m;
            sprite = content.Load<Texture2D>("Characters/player");
            position = currentMap.getMapRealTilePositionByCoords(8, 7);
            collider.X = (int)position.X;
            collider.Y = (int)position.Y + (currentMap.getTileHeight() / 2);
            collider.Width = currentMap.getTileWidth();
            collider.Height = currentMap.getTileHeight() / 2;
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
                            new Vector2(1.0f, 1.0f),
                            SpriteEffects.None,
                            0.0f
                            );
        }

        public void update()
        {
            updateVelocity();
            moveX();
            detectXCollisions();
            moveY();
            detectYCollisions();        
        }

        private void updateVelocity()
        {           
            if (KeyboardManager.TriggeredDown(Keys.Right))
            {
                directionVector += new Vector2(1, 0);               
            }
            else if (KeyboardManager.TriggeredUp(Keys.Right))
            {
                directionVector += new Vector2(-1, 0);
            }
            if (KeyboardManager.TriggeredDown(Keys.Left))
            {
                directionVector += new Vector2(-1, 0);
            }
            else if (KeyboardManager.TriggeredUp(Keys.Left))
            {
                directionVector += new Vector2(1, 0);
            }
            if (KeyboardManager.TriggeredDown(Keys.Up))
            {
                directionVector += new Vector2(0, -1);        
            }
            else if (KeyboardManager.TriggeredUp(Keys.Up))
            {
                directionVector += new Vector2(0, 1);
            }
            if (KeyboardManager.TriggeredDown(Keys.Down))
            {
                directionVector += new Vector2(0, 1);               
            }
            else if (KeyboardManager.TriggeredUp(Keys.Down))
            {
                directionVector += new Vector2(0, -1);
            }
            velocity = directionVector*speed;
        } 

        private void moveX()
        {
            position.X += velocity.X;
            collider.X = (int)position.X;
        }

        private void moveY()
        {
            position.Y += velocity.Y;
            collider.Y = (int)position.Y + (currentMap.getTileHeight() / 2);
        }      

        private void detectXCollisions()
        {
            myPoint[] colliderPoints = Helper.getRectanglePoints(collider);

            foreach (var point in colliderPoints)
            {
                Tile tileContainingPoint = currentMap.getTileContainingPoint(point);
                bool isTilePassable = currentMap.getTileProperty(tileContainingPoint, "Passable");
                if (!isTilePassable)
                {
                    if (directionVector.X > 0)
                    {
                        position.X = tileContainingPoint.position.X - currentMap.getTileWidth();
                        collider.X = (int)position.X;
                    }
                    else if (directionVector.X < 0)
                    {
                        position.X = tileContainingPoint.position.X + currentMap.getTileWidth();
                        collider.X = (int)position.X;
                    }
                    break;
                }
            }
        }

        private void detectYCollisions()
        {
            myPoint[] colliderPoints = Helper.getRectanglePoints(collider);

            foreach (var point in colliderPoints)
            {
                Tile tileContainingPoint = currentMap.getTileContainingPoint(point);
                bool isTilePassable = currentMap.getTileProperty(tileContainingPoint, "Passable");
                if (!isTilePassable)
                {                  
                    if (directionVector.Y < 0)
                    {
                        position.Y = tileContainingPoint.position.Y + collider.Height;
                        collider.Y = (int)position.Y + (currentMap.getTileHeight() / 2);
                    }
                    else if (directionVector.Y > 0)
                    {
                        position.Y = tileContainingPoint.position.Y - currentMap.getTileHeight();
                        collider.Y = (int)position.Y + (currentMap.getTileHeight() / 2);
                    }
                    break;
                }
            }
        }

        private void updateColliderPosition()
        {           
            collider.X = (int)position.X;
            collider.Y = (int)position.Y + (currentMap.getTileHeight() / 2);
        }
    }
}
