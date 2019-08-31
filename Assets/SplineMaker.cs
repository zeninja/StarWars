using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SplineMaker : MonoBehaviour
{
    private static SplineMaker instance;
    public static SplineMaker GetInstance()
    {
        return instance;
    }


    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        instance = this;
    }

    [SerializeField]
    List<SplinePoint> points = new List<SplinePoint>();
    // List<Vector3> velocities = new List<Vector3>();

    int numPoints = 0;
    LineRenderer line;
    public int resolution = 100;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))       // Click to add a point
        // {
        //     Vector3 newPos = Extensions.MouseScreenToWorld2D();
        //     AddPointToList(newPos);
        // }

        DrawSpline();
        DrawPoints();
        SetCurveVelocity();
    }

    void DrawPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Debug.DrawRay(points[i].point, points[i].velocity, Color.red, 1f);
        }
    }

    SplinePoint lastPt;


    public void AddPointToList(Vector3 newPos)
    {

        SplinePoint newPoint = new SplinePoint();

        newPoint.index = numPoints;
        newPoint.point = newPos;

        points.Add(newPoint);
        numPoints = points.Count;

        lastPt = newPoint;
    }

    public void DrawPathThroughObjects(List<GameObject> targets)
    {
        int i = 0;

        List<SplinePoint> tempList = new List<SplinePoint>();

        foreach (GameObject t in targets)
        {
            SplinePoint newPoint = new SplinePoint();
            newPoint.index = i;
            newPoint.point = t.transform.position;

            tempList.Add(newPoint);
        }

        points = tempList;
    }

    void DrawSpline()
    {
        List<Vector3> linePoints = new List<Vector3>();

        if (numPoints > 1)      // need at least two points to make a curve
        {
            for (int i = 0; i < numPoints; i++)
            {
                for (int r = 0; r < resolution; r++)
                {
                    float t = (float)r / (float)resolution;
                    Vector3 point = FindSplinePoint(i, t);
                    linePoints.Add(point);
                }
            }
        }

        int count = linePoints.Count;
        line.positionCount = count;
        line.SetPositions(linePoints.ToArray());
    }

    [Range(0, 1)]
    public float tension = 0;

    Vector3 FindSplinePoint(int i, float t)
    {
        if (i > 0)
        {
            // Debug.Log(i);
            return Extensions.Splines.CubicBezier(points[i - 1], points[i], t);
        }
        else
        {
            return points[i].point;
        }
    }



    // Vector3 GetVelocity(int velocityIndex)
    // {

    //     int prevIndex = velocityIndex - 1;
    //     int nextIndex = velocityIndex + 1;

    //     if (prevIndex > 0 && nextIndex < velocities.Count)
    //     {
    //         Debug.Log((velocities[nextIndex] - velocities[prevIndex]) / 2);
    //         return (velocities[nextIndex] - velocities[prevIndex]) / 2;
    //     }
    //     else
    //     {
    //         // add a condition :
    //         // if loop complete allow rollover to fix continuity

    //         return Vector3.zero;
    //     }
    // }

    void SetCurveVelocity()
    {
        int count = points.Count;
        if (count < 2) { return; } // need at least 3 points to use hermite curve calculation

        for (int i = count - 1; i > 1; i--)
        {
            int nextIndex = i;
            int target = i - 1;
            int prevIndex = i - 2;

            Debug.Log("nextIndex: " + nextIndex + "; target: " + target + "; prevIndex: " + prevIndex);

            Vector3 nextPoint = points[nextIndex].point;
            Vector3 prevPoint = points[prevIndex].point;

            Vector3 vel = (1 - tension) * (nextPoint - prevPoint) / 2;

            points[target].velocity = vel;
            Debug.Log("Set target[" + target + "] to " + vel);
        }
    }
}