using System;

namespace UnityEngine
{
    public struct Quaternion : IEquatable<Quaternion>
    {
        private static readonly Quaternion identityQuaternion = new Quaternion(0.0f, 0.0f, 0.0f, 1f);
        public float x;
        public float y;
        public float z;
        public float w;
        public const float kEpsilon = 1E-06f;

        public static Quaternion Inverse(Quaternion rotation)
        {
            Quaternion ret;
            Quaternion.Inverse_Injected(ref rotation, out ret);
            return ret;
        }

        public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion ret;
            Quaternion.Slerp_Injected(ref a, ref b, t, out ret);
            return ret;
        }

        public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
        {
            Quaternion ret;
            Quaternion.SlerpUnclamped_Injected(ref a, ref b, t, out ret);
            return ret;
        }

        public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            Quaternion ret;
            Quaternion.Lerp_Injected(ref a, ref b, t, out ret);
            return ret;
        }

        private static Quaternion Internal_FromEulerRad(Vector3 euler)
        {
            Quaternion ret;
            Quaternion.Internal_FromEulerRad_Injected(ref euler, out ret);
            return ret;
        }

        private static Vector3 Internal_ToEulerRad(Quaternion rotation)
        {
            Vector3 ret;
            Quaternion.Internal_ToEulerRad_Injected(ref rotation, out ret);
            return ret;
        }

        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            Quaternion ret;
            Quaternion.AngleAxis_Injected(angle, ref axis, out ret);
            return ret;
        }

        public static Quaternion LookRotation(Vector3 forward, Vector3 upwards)
        {
            Quaternion ret;
            Quaternion.LookRotation_Injected(ref forward, ref upwards, out ret);
            return ret;
        }

        public static Quaternion LookRotation(Vector3 forward)
        {
            return Quaternion.LookRotation(forward, Vector3.up);
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
                    case 3:
                        return this.w;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
                    case 3:
                        this.w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Quaternion index!");
                }
            }
        }

        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Quaternion identity
        {
            get
            {
                return Quaternion.identityQuaternion;
            }
        }

        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            return new Quaternion((float)((double)lhs.w * (double)rhs.x + (double)lhs.x * (double)rhs.w + (double)lhs.y * (double)rhs.z - (double)lhs.z * (double)rhs.y), (float)((double)lhs.w * (double)rhs.y + (double)lhs.y * (double)rhs.w + (double)lhs.z * (double)rhs.x - (double)lhs.x * (double)rhs.z), (float)((double)lhs.w * (double)rhs.z + (double)lhs.z * (double)rhs.w + (double)lhs.x * (double)rhs.y - (double)lhs.y * (double)rhs.x), (float)((double)lhs.w * (double)rhs.w - (double)lhs.x * (double)rhs.x - (double)lhs.y * (double)rhs.y - (double)lhs.z * (double)rhs.z));
        }

        public static Vector3 operator *(Quaternion rotation, Vector3 point)
        {
            float num1 = rotation.x * 2f;
            float num2 = rotation.y * 2f;
            float num3 = rotation.z * 2f;
            float num4 = rotation.x * num1;
            float num5 = rotation.y * num2;
            float num6 = rotation.z * num3;
            float num7 = rotation.x * num2;
            float num8 = rotation.x * num3;
            float num9 = rotation.y * num3;
            float num10 = rotation.w * num1;
            float num11 = rotation.w * num2;
            float num12 = rotation.w * num3;
            Vector3 vector3;
            vector3.x = (float)((1.0 - ((double)num5 + (double)num6)) * (double)point.x + ((double)num7 - (double)num12) * (double)point.y + ((double)num8 + (double)num11) * (double)point.z);
            vector3.y = (float)(((double)num7 + (double)num12) * (double)point.x + (1.0 - ((double)num4 + (double)num6)) * (double)point.y + ((double)num9 - (double)num10) * (double)point.z);
            vector3.z = (float)(((double)num8 - (double)num11) * (double)point.x + ((double)num9 + (double)num10) * (double)point.y + (1.0 - ((double)num4 + (double)num5)) * (double)point.z);
            return vector3;
        }

        private static bool IsEqualUsingDot(float dot)
        {
            return (double)dot > 0.999998986721039;
        }

        public static bool operator ==(Quaternion lhs, Quaternion rhs)
        {
            return Quaternion.IsEqualUsingDot(Quaternion.Dot(lhs, rhs));
        }

        public static bool operator !=(Quaternion lhs, Quaternion rhs)
        {
            return !(lhs == rhs);
        }

        public static float Dot(Quaternion a, Quaternion b)
        {
            return (float)((double)a.x * (double)b.x + (double)a.y * (double)b.y + (double)a.z * (double)b.z + (double)a.w * (double)b.w);
        }

        public void SetLookRotation(Vector3 view, Vector3 up)
        {
            this = Quaternion.LookRotation(view, up);
        }

        public static float Angle(Quaternion a, Quaternion b)
        {
            float num = Quaternion.Dot(a, b);
            return Quaternion.IsEqualUsingDot(num) ? 0.0f : (float)((double)Mathf.Acos(Mathf.Min(Mathf.Abs(num), 1f)) * 2.0 * 57.2957801818848);
        }

        private static Vector3 Internal_MakePositive(Vector3 euler)
        {
            float num1 = -9f / (500f * (float)Math.PI);
            float num2 = 360f + num1;
            if ((double)euler.x < (double)num1)
                euler.x += 360f;
            else if ((double)euler.x > (double)num2)
                euler.x -= 360f;
            if ((double)euler.y < (double)num1)
                euler.y += 360f;
            else if ((double)euler.y > (double)num2)
                euler.y -= 360f;
            if ((double)euler.z < (double)num1)
                euler.z += 360f;
            else if ((double)euler.z > (double)num2)
                euler.z -= 360f;
            return euler;
        }

        public Vector3 eulerAngles
        {
            get
            {
                return Quaternion.Internal_MakePositive(Quaternion.Internal_ToEulerRad(this) * 57.29578f);
            }
            set
            {
                this = Quaternion.Internal_FromEulerRad(value * ((float)Math.PI / 180f));
            }
        }

        public static Quaternion Euler(float x, float y, float z)
        {
            return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * ((float)Math.PI / 180f));
        }

        public static Quaternion RotateTowards(
          Quaternion from,
          Quaternion to,
          float maxDegreesDelta)
        {
            float num = Quaternion.Angle(from, to);
            return (double)num == 0.0 ? to : Quaternion.SlerpUnclamped(from, to, Mathf.Min(1f, maxDegreesDelta / num));
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2 ^ this.w.GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            return other is Quaternion other1 && this.Equals(other1);
        }

        public bool Equals(Quaternion other)
        {
            return this.x.Equals(other.x) && this.y.Equals(other.y) && this.z.Equals(other.z) && this.w.Equals(other.w);
        }

        public override string ToString()
        {
            return String.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[4]
            {
        (object) this.x,
        (object) this.y,
        (object) this.z,
        (object) this.w
            });
        }

        private static void Inverse_Injected(ref Quaternion rotation, out Quaternion ret)
        { throw new NotImplementedException(); }

        private static void Slerp_Injected(
          ref Quaternion a,
          ref Quaternion b,
          float t,
          out Quaternion ret)
        { throw new NotImplementedException(); }

        private static void SlerpUnclamped_Injected(
          ref Quaternion a,
          ref Quaternion b,
          float t,
          out Quaternion ret)
        { throw new NotImplementedException(); }

        private static void Lerp_Injected(
          ref Quaternion a,
          ref Quaternion b,
          float t,
          out Quaternion ret)
        { throw new NotImplementedException(); }

        private static void Internal_FromEulerRad_Injected(ref Vector3 euler, out Quaternion ret)
        { throw new NotImplementedException(); }

        private static void Internal_ToEulerRad_Injected(
          ref Quaternion rotation,
          out Vector3 ret)
        { throw new NotImplementedException(); }

        private static void AngleAxis_Injected(
          float angle,
          ref Vector3 axis,
          out Quaternion ret)
        { throw new NotImplementedException(); }

        private static void LookRotation_Injected(
          ref Vector3 forward,
           ref Vector3 upwards,
          out Quaternion ret)
        { throw new NotImplementedException(); }
    }
}
