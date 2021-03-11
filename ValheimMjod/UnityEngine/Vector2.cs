using System;

namespace UnityEngine
{
    public struct Vector2 : IEquatable<Vector2>
    {
        private static readonly Vector2 zeroVector = new Vector2(0.0f, 0.0f);
        private static readonly Vector2 oneVector = new Vector2(1f, 1f);
        private static readonly Vector2 upVector = new Vector2(0.0f, 1f);
        private static readonly Vector2 downVector = new Vector2(0.0f, -1f);
        private static readonly Vector2 leftVector = new Vector2(-1f, 0.0f);
        private static readonly Vector2 rightVector = new Vector2(1f, 0.0f);
        private static readonly Vector2 positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        private static readonly Vector2 negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);
        public float x;
        public float y;
        public const float kEpsilon = 1E-05f;
        public const float kEpsilonNormalSqrt = 1E-15f;

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
            }
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Scale(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public void Normalize()
        {
            float magnitude = this.magnitude;
            if ((double)magnitude > 9.99999974737875E-06)
                this = this / magnitude;
            else
                this = Vector2.zero;
        }

        public Vector2 normalized
        {
            get
            {
                Vector2 vector2 = new Vector2(this.x, this.y);
                vector2.Normalize();
                return vector2;
            }
        }

        public override string ToString()
        {
            return String.Format("({0:F1}, {1:F1})", new object[2]
            {
        (object) this.x,
        (object) this.y
            });
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            return other is Vector2 other1 && this.Equals(other1);
        }

        public bool Equals(Vector2 other)
        {
            return (double)this.x == (double)other.x && (double)this.y == (double)other.y;
        }

        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y);
        }

        public float magnitude
        {
            get
            {
                return (float)Math.Sqrt((double)this.x * (double)this.x + (double)this.y * (double)this.y);
            }
        }

        public float sqrMagnitude
        {
            get
            {
                return (float)((double)this.x * (double)this.x + (double)this.y * (double)this.y);
            }
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            float num1 = a.x - b.x;
            float num2 = a.y - b.y;
            return (float)Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
        }

        public static float SqrMagnitude(Vector2 a)
        {
            return (float)((double)a.x * (double)a.x + (double)a.y * (double)a.y);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.x / d, a.y / d);
        }

        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            float num1 = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            return (double)num1 * (double)num1 + (double)num2 * (double)num2 < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.x, v.y, 0.0f);
        }

        public static Vector2 zero
        {
            get
            {
                return Vector2.zeroVector;
            }
        }

        public static Vector2 one
        {
            get
            {
                return Vector2.oneVector;
            }
        }

        public static Vector2 up
        {
            get
            {
                return Vector2.upVector;
            }
        }

        public static Vector2 right
        {
            get
            {
                return Vector2.rightVector;
            }
        }
    }
}