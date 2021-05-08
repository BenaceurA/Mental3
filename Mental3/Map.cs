using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Xna.Framework;

namespace Mental3
{

    class Map
    {
        public struct layer
        {
            public List<int> data { get; set; }
            public int height { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int opacity { get; set; }
            public string type { get; set; }
            public bool visible { get; set; }
            public int width { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }
        public struct tileset
        {
            public int firstgid { get; set; }
            public string source { get; set; }
        }
        public int compressionlevel { get; set; }
        public int height { get; set; }
        public bool infinite { get; set; }
        public List<layer> layers { get; set; }
        public int nextlayerid { get; set; }
        public int nextobjectid { get; set; }
        public string orientation { get; set; }
        public string renderorder { get; set; }
        public string tiledversion { get; set; }
        public int tileheight { get; set; }
        public List<tileset> tileSets { get; set; }
        public int tilewidth { get; set; }
        public string type { get; set; }
        public string version { get; set; }
        public int width { get; set; }

        private string name;
        private string directory;
        private string file;
        TileSet tileSet;
        public float scaledTileHeight { get; set; }
        public float scaledTileWidth { get; set; }
        

        public static Map import(string source,ContentManager content)
        {            
            Map m = new Map();  
            string s = File.ReadAllText("Content/Maps/" + source + "/" + source + ".json");
            m = JsonConvert.DeserializeObject<Map>(s);
            m.directory = "Content/Maps/" + source + "/";
            m.file = source + ".json";
            m.tileSet = TileSet.import(m.directory, m.tileSets[0].source, content);
            m.scaledTileHeight = m.tileheight * Game1.gameScaleRatio.Y;
            m.scaledTileWidth = m.tilewidth * Game1.gameScaleRatio.X;
            return m;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (var layer in layers)
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
            return new Rectangle((int)position.X,(int)position.Y, tilewidth, tileheight);
        }

        private Vector2 getMapRealTilePositionByIndex(int index) 
        {
            float x = (index * scaledTileWidth) % (scaledTileWidth * width);
            float y = (index / width) * scaledTileHeight;

            return new Vector2(x, y);
        }

        private Vector2 getTextureRealTilePosition(int index)
        {
            int x = ((index-1) * tilewidth) % (tilewidth * tileSet.columns);
            int y = ((index-1) / tileSet.columns) * tileheight;

            return new Vector2(x, y);
        }

        public Vector2 getMapRealTilePositionByCoords(int x , int y)
        {
            return new Vector2(x * scaledTileWidth, y * scaledTileHeight);
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
            position.X = point.X - (point.X % scaledTileWidth);
            position.Y = point.Y - (point.Y % scaledTileHeight);
            return position;
        }

        public Vector2 getLeftTile(Point point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X - tilewidth, v.Y);
        }

        public Vector2 getRightTile(Point point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X + tilewidth, v.Y);
        }

        public Vector2 getTopTile(Point point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X, v.Y - tileheight);
        }

        public Vector2 getBottomTile(Point point)
        {
            Vector2 v = getTilePositionContainingPoint(point);
            return new Vector2(v.X, v.Y + tilewidth);
        }

    }
}
