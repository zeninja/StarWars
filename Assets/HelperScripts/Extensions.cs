using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extensions : MonoBehaviour
{

    // Args: input min, input max, map to range min, range max, number to input
    // Inputs outside a1 to a2 will fall outside the output range
    public static float mapRange(float a1, float a2, float b1, float b2, float s)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);

    }

    // Output is clamped to range: b1 to b2
    public static float mapRangeMinMax(float a1, float a2, float b1, float b2, float s)
    {
        float value = b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        value = Mathf.Clamp(value, b1, b2);
        return value;
    }

    public static Vector3 ScreenToWorld(Vector3 input)
    {
        input = new Vector3(input.x, input.y, Camera.main.nearClipPlane);
        Vector3 output = Camera.main.ScreenToWorldPoint(input);
        return output;
    }

    public static Vector3 MouseScreenToWorld()
    {
        Vector3 input = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 output = Camera.main.ScreenToWorldPoint(input);
        return output;
    }

    public static Vector3 MouseScreenToWorld2D()
    {
        Vector3 input = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 output = Camera.main.ScreenToWorldPoint(input);
        return output;
    }

    public static Vector3 TouchScreenToWorld(Touch t)
    {
        Vector3 input = Camera.main.ScreenToWorldPoint(t.position);
        Vector3 output = new Vector3(input.x, input.y, 0);
        return output;
    }

    [System.Serializable]
    public class Property
    {
        public float start;
        public float end;
    }

    [System.Serializable]
    public class ColorProperty
    {
        public Color start;
        public Color end;
    }

    public static float GetSmoothStepRange(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStep3(t);
    }

    public static float GetSmoothStart2Range(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStart3(t);
    }

    public static float GetSmoothStart3Range(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStart3(t);
    }

    public static float GetSmoothStart4Range(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStart4(t);
    }

    public static float GetSmoothStart5Range(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStart5(t);
    }

    public static float GetSmoothStop2Range(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStop2(t);
    }

    public static float GetSmoothStop3Range(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStop3(t);
    }

    public static float GetSmoothStop4Range(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStop4(t);
    }

    public static float GetSmoothStop5Range(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.SmoothStop5(t);
    }

    public static float GetLinearRange(Property p, float t)
    {
        return p.start + (p.end - p.start) * EZEasings.Linear(t);
    }


    public static IEnumerator Wait(float d)
    {
        float t = 0;
        while (t < d)
        {
            t += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }


    //  -------------  SPLINES
    public class Splines
    {



        // public class Curve
        // {
        //     public SplinePoint start;
        //     public SplinePoint end;

        //     public Curve() {
        //         start = new SplinePoint();
        //         end   = new SplinePoint();
        //     }
        // }

                // public static Curve ConvertPointsToCurve(Vector3[] points)
        // {
        //     Curve c = new Extensions.Splines.Curve();

        //     // Debug.Log(points.Length);

        //     // foreach(Vector3 p in points) {
        //     //     Debug.Log(p);
        //     // }

        //     // Debug.Log(points[0]);

        //     c.start.position = points[0];
        //     c.start.velocity = points[1];

        //     c.end.position = points[2];
        //     c.end.velocity = points[3];

        //     return c;
        // }

        public static Vector3 LinearBezier(Vector3 A, Vector3 B, float t)
        {
            return ((1 - t) * A) + (t * B);
        }

        public static Vector3 QuadraticBezier(Vector3 A, Vector3 B, Vector3 C, float t)
        {
            Vector3 E = LinearBezier(A, B, t);
            Vector3 F = LinearBezier(B, C, t);

            return LinearBezier(E, F, t);
        }

        public static Vector3 CubicBezier(SplinePoint a, SplinePoint d, float t)
        {

            Vector2 A = a.point;
            Vector2 B = a.point + a.velocity;
            Vector2 C = d.point - d.velocity;
            Vector2 D = d.point;

            Vector2 E = LinearBezier(A, B, t);      // Linear Bezier (blend of A and B)
            Vector2 F = LinearBezier(B, C, t);      // Linear Bezier (blend of B and C)
            Vector2 G = LinearBezier(C, D, t);      // Linear Bezier (blend of C and D)

            Vector2 Q = LinearBezier(E, F, t);      // Quadratic Bezier (blend of E and F)
            Vector2 R = LinearBezier(F, G, t);

            return LinearBezier(Q, R, t);           // Cubic Bezier
        }

        // public static Vector3 CubicHermite(SplinePoint a, SplinePoint d, float t) {
        //     Vector2 A = a.point;
        //     Vector2 B = a.point + a.velocity;
        //     Vector2 C = d.point + d.velocity;
        //     Vector2 D = d.point;

        //     Vector2 E = LinearBezier()
        // }

        // public static Curve ConvertPointsToCurve(Vector3[] points)
        // {
        //     Curve c = new Extensions.Splines.Curve();

        //     // Debug.Log(points.Length);

        //     // foreach(Vector3 p in points) {
        //     //     Debug.Log(p);
        //     // }

        //     // Debug.Log(points[0]);

        //     c.start.position = points[0];
        //     c.start.velocity = points[1];

        //     c.end.position = points[2];
        //     c.end.velocity = points[3];

        //     return c;
        // }
    }





    // Range Map
    // (in, inStart, inEnd, outStart, outEnd) 
    // {
    //	out = in - inStart // Puts in [0, inEnd - inStart]
    //  out /= (inEnd - inStart); // Puts in [0,1]
    //  out = ApplyEasing(out); // in [0,1]
    //  out *= (outEnd - outStart); // puts in [0, outRange]
    //  return out + outStart; 
    // }

}