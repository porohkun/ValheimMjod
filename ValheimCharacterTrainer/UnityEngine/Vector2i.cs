namespace UnityEngine
{
    public struct Vector2i
    {
        public static Vector2i zero = new Vector2i(0, 0);
        public int x;
        public int y;

        public Vector2i(Vector2 v)
        {
            this.x = (int)v.x;
            this.y = (int)v.y;
        }

        public Vector2i(Vector3 v)
        {
            this.x = (int)v.x;
            this.y = (int)v.y;
        }

        public Vector2i(Vector2i v)
        {
            this.x = v.x;
            this.y = v.y;
        }

        public Vector2i(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }

        public static Vector2i operator +(Vector2i v0, Vector2i v1)
        {
            return new Vector2i(v0.x + v1.x, v0.y + v1.y);
        }

        public static Vector2i operator -(Vector2i v0, Vector2i v1)
        {
            return new Vector2i(v0.x - v1.x, v0.y - v1.y);
        }

        public static bool operator ==(Vector2i v0, Vector2i v1)
        {
            return v0.x == v1.x && v0.y == v1.y;
        }

        public static bool operator !=(Vector2i v0, Vector2i v1)
        {
            return v0.x != v1.x || v0.y != v1.y;
        }

        public int Magnitude()
        {
            return Mathf.Abs(this.x) + Mathf.Abs(this.y);
        }

        public static int Distance(Vector2i a, Vector2i b)
        {
            return (a - b).Magnitude();
        }

        public Vector2 ToVector2()
        {
            return new Vector2((float)this.x, (float)this.y);
        }

        public override string ToString()
        {
            return this.x.ToString() + "," + this.y.ToString();
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Vector2i vector2i && vector2i.x == this.x && vector2i.y == this.y;
        }
    }
}