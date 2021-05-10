using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
namespace Mental3
{
    public struct myRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
    }

    public struct myPoint
    {
        public myPoint(float x, float y)
        {
            X = x;
            Y = y;
        }
        public float X;
        public float Y;
        public Vector2 toVector2()
        {
            return new Vector2(X, Y);
        }
    }

    class Helper
    {
        public static myPoint vectorToPoint(Vector2 v)
        {
            return new myPoint(v.X, v.Y);
        }
        public static myPoint[] getRectanglePoints(myRectangle rectangle)
        {
            
            myPoint[] points = new myPoint[4];
            points[0] = new myPoint(rectangle.X, rectangle.Y);
            points[1] = new myPoint(rectangle.X + rectangle.Width - 1, rectangle.Y);
            points[2] = new myPoint(rectangle.X, rectangle.Y + rectangle.Height - 1);
            points[3] = new myPoint(rectangle.X + rectangle.Width - 1, rectangle.Y + rectangle.Height - 1);
            return points;
        }
    }
}
