using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.IO;
using Microsoft.Xna.Framework;

namespace Mental3
{

    class Map
    {
        MapData mapData;
        private string name;
        private string directory;
        private string file;
        TileSet tileSet;
        public float tileHeight { get; set; }
        public float tileWidth { get; set; }        
        
        public Map(string name,ContentManager content)
        {
            mapData = MapReader.Read(name);
            directory = "Content/Maps/" + name + "/";
            file = name + ".json";
            tileSet = TileSet.import(directory, mapData.tileSets[0].source, content);
            tileHeight = mapData.tileheight * Game1.gameScaleRatio.Y;
            tileWidth = mapData.tilewidth * Game1.gameScaleRatio.X;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (var layer in mapData.layers)
            {
                for (int i = 0; i < layer.data.Count; i++)
                {
                    if (layer.data[i] != 0)
                    {
                        spriteBatch.Draw(tileSet.texture,
                            getMapRealTilePositionByIndex(i),
                            getTextureRegionByIndex(layer.data[i]),
                            Color.White,
                            0.0f,
                            new Vector2(0.0f, 0.0f),
                            new Vector2(Game1.gameScaleRatio.X, Game1.gameScaleRatio.Y),
                            SpriteEffects.None,
                            0.0f 
                            );
                    }                    
                }
            }
        }

        private Rectangle getTextureRegionByIndex(int index)
        {
            Vector2 position = getTextureRealTilePosition(index);
            return new Rectangle((int)position.X,(int)position.Y, mapData.tilewidth, mapData.tileheight);
        }

        private Vector2 getMapRealTilePositionByIndex(int index) 
        {
            float x = (index * this.tileWidth) % (this.tileWidth * mapData.width);
            float y = (index / mapData.width) * this.tileHeight;

            return new Vector2(x, y);
        }

        private Vector2 getTextureRealTilePosition(int index)
        {
            int x = ((index-1) * mapData.tilewidth) % (mapData.tilewidth * tileSet.columns);
            int y = ((index-1) / tileSet.columns) * mapData.tileheight;

            return new Vector2(x, y);
        }

        public Vector2 getMapRealTilePositionByCoords(int x , int y)
        {
            return new Vector2(x * this.tileWidth, y * this.tileHeight);
        }

        public List<Vector2> getTilesAroundRectangle(Rectangle rectangle) 
        {
            //important : only checks case where rectangle can be on 4 tile max; meaning it's dimensions can't exceed tile dimensions

            List<Vector2> tiles = new List<Vector2>();

            Point[] points = new Point[4];
            Point top_left = new Point(rectangle.X, rectangle.Y);
            Point top_Right = new Point(rectangle.X + rectangle.Width, rectangle.Y);
            Point bottom_left = new Point(rectangle.X, rectangle.Y + rectangle.Height);
            Point bottom_Right = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);

            Vector2[] v = new Vector2[8];
            v[0] = getLeftTile(top_left);
            v[1] = getTopTile(top_left);
            v[2] = getTopTile(top_Right);
            v[3] = getRightTile(top_Right);
            v[4] = getLeftTile(bottom_left);
            v[5] = getBottomTile(bottom_left);
            v[6] = getRightTile(bottom_Right);
            v[7] = getBottomTile(bottom_Right);

            foreach (var tile in v)
            {
                if (!tiles.Contains(tile))
                {
                    tiles.Add(tile);
                }
            }
            return tiles;
        }

        public Vector2 getTilePositionContainingPoint(Point point)
        {
            Vector2 position = new Vector2();
            position.X = point.X - (point.X % this.tileWidth);
            position.Y = point.Y - (point.Y % this.tileHeight);
            return position;
        }

        public Vector2 getLeftTile(Point point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X - this.tileWidth, v.Y);
        }

        public Vector2 getRightTile(Point point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X + this.tileWidth, v.Y);
        }

        public Vector2 getTopTile(Point point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X, v.Y - this.tileHeight);
        }

        public Vector2 getBottomTile(Point point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X, v.Y + this.tileWidth);
        }

    }
}
