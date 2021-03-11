using System;

namespace UnityEngine
{
    public struct MathfInternal
    {
        public static volatile float FloatMinNormal = 1.175494E-38f;
        public static volatile float FloatMinDenormal = float.Epsilon;
        public static bool IsFlushToZeroEnabled = (double)MathfInternal.FloatMinDenormal == 0.0;
    }

    public struct Mathf
    {
        public static readonly float Epsilon = MathfInternal.IsFlushToZeroEnabled ? MathfInternal.FloatMinNormal : MathfInternal.FloatMinDenormal;

        public static float GammaToLinearSpace(float value) { throw new NotImplementedException(); }

        public static float LinearToGammaSpace(float value) { throw new NotImplementedException(); }

        public static float PerlinNoise(float x, float y) { throw new NotImplementedException(); }

        public static float Sin(float f)
        {
            return (float)Math.Sin((double)f);
        }

        public static float Cos(float f)
        {
            return (float)Math.Cos((double)f);
        }

        public static float Tan(float f)
        {
            return (float)Math.Tan((double)f);
        }

        public static float Asin(float f)
        {
            return (float)Math.Asin((double)f);
        }

        public static float Acos(float f)
        {
            return (float)Math.Acos((double)f);
        }

        public static float Atan(float f)
        {
            return (float)Math.Atan((double)f);
        }

        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2((double)y, (double)x);
        }

        public static float Sqrt(float f)
        {
            return (float)Math.Sqrt((double)f);
        }

        public static float Abs(float f)
        {
            return Math.Abs(f);
        }

        public static int Abs(int value)
        {
            return Math.Abs(value);
        }

        public static float Min(float a, float b)
        {
            return (double)a < (double)b ? a : b;
        }

        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        public static float Max(float a, float b)
        {
            return (double)a > (double)b ? a : b;
        }

        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public static float Pow(float f, float p)
        {
            return (float)Math.Pow((double)f, (double)p);
        }

        public static float Exp(float power)
        {
            return (float)Math.Exp((double)power);
        }

        public static float Log(float f, float p)
        {
            return (float)Math.Log((double)f, (double)p);
        }

        public static float Log(float f)
        {
            return (float)Math.Log((double)f);
        }

        public static float Ceil(float f)
        {
            return (float)Math.Ceiling((double)f);
        }

        public static float Floor(float f)
        {
            return (float)Math.Floor((double)f);
        }

        public static float Round(float f)
        {
            return (float)Math.Round((double)f);
        }

        public static int CeilToInt(float f)
        {
            return (int)Math.Ceiling((double)f);
        }

        public static int FloorToInt(float f)
        {
            return (int)Math.Floor((double)f);
        }

        public static int RoundToInt(float f)
        {
            return (int)Math.Round((double)f);
        }

        public static float Sign(float f)
        {
            return (double)f >= 0.0 ? 1f : -1f;
        }

        public static float Clamp(float value, float min, float max)
        {
            if ((double)value < (double)min)
                value = min;
            else if ((double)value > (double)max)
                value = max;
            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (value < min)
                value = min;
            else if (value > max)
                value = max;
            return value;
        }

        public static float Clamp01(float value)
        {
            if ((double)value < 0.0)
                return 0.0f;
            return (double)value > 1.0 ? 1f : value;
        }

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * Mathf.Clamp01(t);
        }

        public static float LerpUnclamped(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float LerpAngle(float a, float b, float t)
        {
            float num = Mathf.Repeat(b - a, 360f);
            if ((double)num > 180.0)
                num -= 360f;
            return a + num * Mathf.Clamp01(t);
        }

        public static float MoveTowards(float current, float target, float maxDelta)
        {
            return (double)Mathf.Abs(target - current) <= (double)maxDelta ? target : current + Mathf.Sign(target - current) * maxDelta;
        }

        public static float SmoothStep(float from, float to, float t)
        {
            t = Mathf.Clamp01(t);
            t = (float)(-2.0 * (double)t * (double)t * (double)t + 3.0 * (double)t * (double)t);
            return (float)((double)to * (double)t + (double)from * (1.0 - (double)t));
        }

        public static bool Approximately(float a, float b)
        {
            return (double)Mathf.Abs(b - a) < (double)Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
        }

        public static float SmoothDamp(
          float current,
          float target,
          ref float currentVelocity,
          float smoothTime,
          float maxSpeed)
        {
            throw new NotImplementedException();
        }

        public static float SmoothDamp(
          float current,
          float target,
          ref float currentVelocity,
          float smoothTime,
           float maxSpeed,
           float deltaTime)
        {
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num1 = 2f / smoothTime;
            float num2 = num1 * deltaTime;
            float num3 = (float)(1.0 / (1.0 + (double)num2 + 0.479999989271164 * (double)num2 * (double)num2 + 0.234999999403954 * (double)num2 * (double)num2 * (double)num2));
            float num4 = current - target;
            float num5 = target;
            float max = maxSpeed * smoothTime;
            float num6 = Mathf.Clamp(num4, -max, max);
            target = current - num6;
            float num7 = (currentVelocity + num1 * num6) * deltaTime;
            currentVelocity = (currentVelocity - num1 * num7) * num3;
            float num8 = target + (num6 + num7) * num3;
            if ((double)num5 - (double)current > 0.0 == (double)num8 > (double)num5)
            {
                num8 = num5;
                currentVelocity = (num8 - num5) / deltaTime;
            }
            return num8;
        }

        public static float Repeat(float t, float length)
        {
            return Mathf.Clamp(t - Mathf.Floor(t / length) * length, 0.0f, length);
        }

        public static float InverseLerp(float a, float b, float value)
        {
            return (double)a != (double)b ? Mathf.Clamp01((float)(((double)value - (double)a) / ((double)b - (double)a))) : 0.0f;
        }

        public static float DeltaAngle(float current, float target)
        {
            float num = Mathf.Repeat(target - current, 360f);
            if ((double)num > 180.0)
                num -= 360f;
            return num;
        }
    }
}
