using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Mental3
{

    class TileSet
    {
        public struct tile
        {
            public int id;
            public List<properties> properties;
        }

        public struct properties
        {
            public string name;
            public string type;
            public dynamic value;
        }
        public int columns;
        public string image;
        public int imageheight;
        public int imagewidth;
        public int margin;
        public string name;
        public int spacing;
        public int tilecount;
        public string tiledversion;
        public int tileheight;
        public List<tile> tiles;
        public int tilewidth;
        public string type;
        public string version;
        
        public Texture2D texture;

        public static TileSet import(string directory,string source, ContentManager content)
        {
            TileSet t = new TileSet();
            string s = File.ReadAllText(directory + source);
            t = JsonConvert.DeserializeObject<TileSet>(s);
            t.texture = content.Load<Texture2D>(t.image);
            return t;
        }
    }
}
