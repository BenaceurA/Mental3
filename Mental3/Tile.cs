using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace Mental3
{
    class Tile
    {
        public Tile(int index,Vector2 position)
        {
            this.position = position;
            this.index = index;
        }
        public Vector2 position { get; set; }
        public int index { get; set; }
    }
}
