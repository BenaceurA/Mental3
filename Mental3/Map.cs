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

        public Map(string name,ContentManager content)
        {
            mapData = MapReader.Read(name);
            directory = "Content/Maps/" + name + "/";
            file = name + ".json";
            tileSet = TileSet.import(directory, mapData.tileSets[0].source, content);

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
                            new Vector2(1.0f,1.0f),
                            SpriteEffects.None,
                            0.0f 
                            );
                    }                    
                }
            }
        }

        private Vector2 getMapRealTilePositionByIndex(int index) 
        {
            float x = (index * mapData.tilewidth) % (mapData.tilewidth * mapData.width);
            float y = (index / mapData.width) * mapData.tileheight;

            return new Vector2(x, y);
        }

        public Vector2 getMapRealTilePositionByCoords(int x, int y)
        {
            return new Vector2(x * mapData.tilewidth, y * mapData.tileheight);
        }

        private Rectangle getTextureRegionByIndex(int index)
        {
            Vector2 position = getTextureRealTilePosition(index);
            return new Rectangle((int)position.X, (int)position.Y, tileSet.tilewidth, tileSet.tileheight);
        }

        private Vector2 getTextureRealTilePosition(int index)
        {   
            int x = ((index-1) * mapData.tilewidth) % (mapData.tilewidth * tileSet.columns);
            int y = ((index-1) / tileSet.columns) * mapData.tileheight;

            return new Vector2(x, y);
        }

        public int getTileIndexByPosition(float x, float y)
        {
            float t = (y / mapData.tileheight * (float)mapData.width) + (x / mapData.tilewidth);
            return ((int)t); 
        }

        private List<Vector2> getTilesPositionAroundRectangle(myRectangle rectangle) 
        {
            //important : only checks case where rectangle can be on 4 tiles max; meaning it's dimensions can't exceed tile dimensions

            List<Vector2> tilePositions = new List<Vector2>();

            myPoint[] points = new myPoint[4];
            myPoint top_left = new myPoint(rectangle.X, rectangle.Y);
            myPoint top_Right = new myPoint(rectangle.X + rectangle.Width, rectangle.Y);
            myPoint bottom_left = new myPoint(rectangle.X, rectangle.Y + rectangle.Height);
            myPoint bottom_Right = new myPoint(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            
            
            Vector2[] v = new Vector2[12];
            v[0] = getTopTile(top_left);
            v[1] = getTopTile(top_Right);
            v[2] = getBottomTile(bottom_left);
            v[3] = getBottomTile(bottom_Right);
            v[4] = getRightTile(bottom_Right);
            v[5] = getRightTile(top_Right);
            v[6] = getLeftTile(top_left);
            v[7] = getLeftTile(bottom_left);

            v[8] = getLeftTile(Helper.vectorToPoint(v[0]));
            v[9] = getRightTile(Helper.vectorToPoint(v[1]));
            v[10] = getLeftTile(Helper.vectorToPoint(v[2]));
            v[11] = getRightTile(Helper.vectorToPoint(v[3]));

            foreach (var tile in v)
            {
                if (!tilePositions.Contains(tile))
                {
                    tilePositions.Add(tile);
                }
            }
            return tilePositions;
        }

        public List<Tile> getTilesAroundRectangle(myRectangle rectangle)
        {
            List<Tile> tiles = new List<Tile>();
            List<Vector2> tilePositions = getTilesPositionAroundRectangle(rectangle);

            foreach (var position in tilePositions)
            {
                tiles.Add(new Tile(getTileIndexByPosition((int)position.X, (int)position.Y), position));
            }

            return tiles;
        }

        private Vector2 getTilePositionContainingPoint(myPoint point)
        {
            Vector2 position = new Vector2();
            position.X = point.X - (point.X % mapData.tilewidth);
            position.Y = point.Y - (point.Y % mapData.tileheight);
            return position;
        }

        public Tile getTileContainingPoint(myPoint point)
        {
            Vector2 tilePosition = getTilePositionContainingPoint(point);
            return new Tile(getTileIndexByPosition(tilePosition.X,tilePosition.Y),tilePosition); 
        }

        public Vector2 getLeftTile(myPoint point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X - mapData.tilewidth, v.Y);
        }

        public Vector2 getRightTile(myPoint point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X + mapData.tilewidth, v.Y);
        }

        public Vector2 getTopTile(myPoint point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X, v.Y - mapData.tileheight);
        }

        public Vector2 getBottomTile(myPoint point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X, v.Y + mapData.tilewidth);
        }

        public dynamic getTileProperty(Tile tile,string property)
        {
            return tileSet.getTileProperty(getTileId(tile), property);
        }

        private int getTileId(Tile tile)
        {
            return mapData.layers[0].data[tile.index] - 1;
        }

        public int getTileHeight()
        {
            return mapData.tileheight;
        }

        public int getTileWidth()
        {
            return mapData.tilewidth;
        }
    }
}
