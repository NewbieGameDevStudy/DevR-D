using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public struct Vec2
    {
        public static Vec2 zero => new Vec2(0, 0);

        public double X { get; }
        public double Y { get; }

        public double magnitude => Math.Sqrt(X * X + Y * Y);

        public Vec2(double xPos, double yPos)
        {
            X = xPos;
            Y = yPos;
        }

        public Vec2(float xPos, float yPos)
        {
            X = xPos;
            Y = yPos;
        }
     
        public static Vec2 operator - (Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vec2 operator +(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static bool operator ==(Vec2 v1, Vec2 v2)
        {
            return Math.Abs(v1.X - v2.X) < double.Epsilon && Math.Abs(v1.Y - v2.Y) < double.Epsilon;
        }

        public static bool operator !=(Vec2 v1, Vec2 v2)
        {
            return Math.Abs(v1.X - v2.X) > double.Epsilon || Math.Abs(v1.Y - v2.Y) > double.Epsilon;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vec2 target) {
                return Math.Abs(X - target.X) <= double.Epsilon
                    && Math.Abs(Y - target.Y) <= double.Epsilon;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Vec2 Normalize()
        {
            double length = magnitude;
            if(length > 0) {
                return new Vec2(X / length, Y / length);
            }

            return zero;
        }

        public Vec2 MoveVecValue(double x, double y)
        {
            return new Vec2(X + x, Y + y);
        }
    }
}
