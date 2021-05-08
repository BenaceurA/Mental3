using System;
using System.Collections.Generic;
using System.Text;

namespace Mental3
{
    class MapData
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

    }
}
