using System;
using System.Globalization;

namespace UnityEngine
{
    public struct Vector3 : IEquatable<Vector3>
    {
        private static readonly Vector3 zeroVector = new Vector3(0.0f, 0.0f, 0.0f);
        private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);
        private static readonly Vector3 upVector = new Vector3(0.0f, 1f, 0.0f);
        private static readonly Vector3 downVector = new Vector3(0.0f, -1f, 0.0f);
        private static readonly Vector3 leftVector = new Vector3(-1f, 0.0f, 0.0f);
        private static readonly Vector3 rightVector = new Vector3(1f, 0.0f, 0.0f);
        private static readonly Vector3 forwardVector = new Vector3(0.0f, 0.0f, 1f);
        private static readonly Vector3 backVector = new Vector3(0.0f, 0.0f, -1f);
        private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
        public const float kEpsilon = 1E-05f;
        public const float kEpsilonNormalSqrt = 1E-15f;
        public float x;
        public float y;
        public float z;

        public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
        {
            Vector3 ret;
            Vector3.Slerp_Injected(ref a, ref b, t, out ret);
            return ret;
        }

        public static Vector3 RotateTowards(
          Vector3 current,
          Vector3 target,
          float maxRadiansDelta,
          float maxMagnitudeDelta)
        {
            Vector3 ret;
            Vector3.RotateTowards_Injected(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta, out ret);
            return ret;
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathf.Clamp01(t);
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        public static Vector3 MoveTowards(
          Vector3 current,
          Vector3 target,
          float maxDistanceDelta)
        {
            float num1 = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = target.z - current.z;
            float num4 = (float)((double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3);
            if ((double)num4 == 0.0 || (double)maxDistanceDelta >= 0.0 && (double)num4 <= (double)maxDistanceDelta * (double)maxDistanceDelta)
                return target;
            float num5 = (float)Math.Sqrt((double)num4);
            return new Vector3(current.x + num1 / num5 * maxDistanceDelta, current.y + num2 / num5 * maxDistanceDelta, current.z + num3 / num5 * maxDistanceDelta);
        }

        public static Vector3 SmoothDamp(
          Vector3 current,
          Vector3 target,
          ref Vector3 currentVelocity,
          float smoothTime,
          float maxSpeed)
        {
            throw new NotImplementedException();
        }

        public static Vector3 SmoothDamp(
          Vector3 current,
          Vector3 target,
          ref Vector3 currentVelocity,
          float smoothTime,
          float maxSpeed,
          float deltaTime)
        {
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            float num4 = current.x - target.x;
            float num5 = current.y - target.y;
            float num6 = current.z - target.z;
            Vector3 vector3 = target;
            float num7 = maxSpeed * smoothTime;
            float num8 = num7 * num7;
            float num9 = (float)((double)num4 * (double)num4 + (double)num5 * (double)num5 + (double)num6 * (double)num6);
            if ((double)num9 > (double)num8)
            {
                float num10 = (float)Math.Sqrt((double)num9);
                num4 = num4 / num10 * num7;
                num5 = num5 / num10 * num7;
                num6 = num6 / num10 * num7;
            }
            target.x = current.x - num4;
            target.y = current.y - num5;
            target.z = current.z - num6;
            float num11 = (currentVelocity.x + num1 * num4) * deltaTime;
            float num12 = (currentVelocity.y + num1 * num5) * deltaTime;
            float num13 = (currentVelocity.z + num1 * num6) * deltaTime;
            currentVelocity.x = (currentVelocity.x - num1 * num11) * num3;
            currentVelocity.y = (currentVelocity.y - num1 * num12) * num3;
            currentVelocity.z = (currentVelocity.z - num1 * num13) * num3;
            float x = target.x + (num4 + num11) * num3;
            float y = target.y + (num5 + num12) * num3;
            float z = target.z + (num6 + num13) * num3;
            float num14 = vector3.x - current.x;
            float num15 = vector3.y - current.y;
            float num16 = vector3.z - current.z;
            float num17 = x - vector3.x;
            float num18 = y - vector3.y;
            float num19 = z - vector3.z;
            if ((double)num14 * (double)num17 + (double)num15 * (double)num18 + (double)num16 * (double)num19 > 0.0)
            {
                x = vector3.x;
                y = vector3.y;
                z = vector3.z;
                currentVelocity.x = (x - vector3.x) / deltaTime;
                currentVelocity.y = (y - vector3.y) / deltaTime;
                currentVelocity.z = (z - vector3.z) / deltaTime;
            }
            return new Vector3(x, y, z);
        }

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
                    case 2:
                        return this.z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
                    case 2:
                        this.z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3((float)((double)lhs.y * (double)rhs.z - (double)lhs.z * (double)rhs.y), (float)((double)lhs.z * (double)rhs.x - (double)lhs.x * (double)rhs.z), (float)((double)lhs.x * (double)rhs.y - (double)lhs.y * (double)rhs.x));
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        public override bool Equals(object other)
        {
            return other is Vector3 other1 && this.Equals(other1);
        }

        public bool Equals(Vector3 other)
        {
            return (double)this.x == (double)other.x && (double)this.y == (double)other.y && (double)this.z == (double)other.z;
        }

        public static Vector3 Normalize(Vector3 value)
        {
            float num = Vector3.Magnitude(value);
            return (double)num > 9.99999974737875E-06 ? value / num : Vector3.zero;
        }

        public void Normalize()
        {
            float num = Vector3.Magnitude(this);
            if ((double)num > 9.99999974737875E-06)
                this = this / num;
            else
                this = Vector3.zero;
        }

        public Vector3 normalized
        {
            get
            {
                return Vector3.Normalize(this);
            }
        }

        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return (float)((double)lhs.x * (double)rhs.x + (double)lhs.y * (double)rhs.y + (double)lhs.z * (double)rhs.z);
        }

        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
        {
            float num1 = Vector3.Dot(onNormal, onNormal);
            if ((double)num1 < (double)Mathf.Epsilon)
                return Vector3.zero;
            float num2 = Vector3.Dot(vector, onNormal);
            return new Vector3(onNormal.x * num2 / num1, onNormal.y * num2 / num1, onNormal.z * num2 / num1);
        }

        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            float num1 = Vector3.Dot(planeNormal, planeNormal);
            if ((double)num1 < (double)Mathf.Epsilon)
                return vector;
            float num2 = Vector3.Dot(vector, planeNormal);
            return new Vector3(vector.x - planeNormal.x * num2 / num1, vector.y - planeNormal.y * num2 / num1, vector.z - planeNormal.z * num2 / num1);
        }

        public static float Angle(Vector3 from, Vector3 to)
        {
            float num = (float)Math.Sqrt((double)from.sqrMagnitude * (double)to.sqrMagnitude);
            return (double)num < 1.00000000362749E-15 ? 0.0f : (float)Math.Acos((double)Mathf.Clamp(Vector3.Dot(from, to) / num, -1f, 1f)) * 57.29578f;
        }

        public static float SignedAngle(Vector3 from, Vector3 to, Vector3 axis)
        {
            float num1 = Vector3.Angle(from, to);
            float num2 = (float)((double)from.y * (double)to.z - (double)from.z * (double)to.y);
            float num3 = (float)((double)from.z * (double)to.x - (double)from.x * (double)to.z);
            float num4 = (float)((double)from.x * (double)to.y - (double)from.y * (double)to.x);
            float num5 = Mathf.Sign((float)((double)axis.x * (double)num2 + (double)axis.y * (double)num3 + (double)axis.z * (double)num4));
            return num1 * num5;
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            float num1 = a.x - b.x;
            float num2 = a.y - b.y;
            float num3 = a.z - b.z;
            return (float)Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3);
        }

        public static float Magnitude(Vector3 vector)
        {
            return (float)Math.Sqrt((double)vector.x * (double)vector.x + (double)vector.y * (double)vector.y + (double)vector.z * (double)vector.z);
        }

        public float magnitude
        {
            get
            {
                return (float)Math.Sqrt((double)this.x * (double)this.x + (double)this.y * (double)this.y + (double)this.z * (double)this.z);
            }
        }

        public static float SqrMagnitude(Vector3 vector)
        {
            return (float)((double)vector.x * (double)vector.x + (double)vector.y * (double)vector.y + (double)vector.z * (double)vector.z);
        }

        public float sqrMagnitude
        {
            get
            {
                return (float)((double)this.x * (double)this.x + (double)this.y * (double)this.y + (double)this.z * (double)this.z);
            }
        }

        public static Vector3 Min(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
        }

        public static Vector3 Max(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
        }

        public static Vector3 zero
        {
            get
            {
                return Vector3.zeroVector;
            }
        }

        public static Vector3 one
        {
            get
            {
                return Vector3.oneVector;
            }
        }

        public static Vector3 forward
        {
            get
            {
                return Vector3.forwardVector;
            }
        }

        public static Vector3 back
        {
            get
            {
                return Vector3.backVector;
            }
        }

        public static Vector3 up
        {
            get
            {
                return Vector3.upVector;
            }
        }

        public static Vector3 down
        {
            get
            {
                return Vector3.downVector;
            }
        }

        public static Vector3 left
        {
            get
            {
                return Vector3.leftVector;
            }
        }

        public static Vector3 right
        {
            get
            {
                return Vector3.rightVector;
            }
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.x / d, a.y / d, a.z / d);
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            float num1 = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = lhs.z - rhs.z;
            return (double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3 < 9.99999943962493E-11;
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return String.Format("({0:F1}, {1:F1}, {2:F1})", new object[3]
            {
        (object) this.x,
        (object) this.y,
        (object) this.z
            });
        }

        public string ToString(string format)
        {
            return String.Format("({0}, {1}, {2})", new object[3]
            {
        (object) this.x.ToString(format, (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat),
        (object) this.y.ToString(format, (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat),
        (object) this.z.ToString(format, (IFormatProvider) CultureInfo.InvariantCulture.NumberFormat)
            });
        }

        private static void Slerp_Injected(
          ref Vector3 a,
          ref Vector3 b,
          float t,
          out Vector3 ret)
        { throw new NotImplementedException(); }

        private static void RotateTowards_Injected(
          ref Vector3 current,
          ref Vector3 target,
          float maxRadiansDelta,
          float maxMagnitudeDelta,
          out Vector3 ret)
        { throw new NotImplementedException(); }
    }
}
