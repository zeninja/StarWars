using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlanetSpline : MonoBehaviour
{
    #region
    private static PlanetSpline instance;
    public static PlanetSpline GetInstance()
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
    #endregion

    [SerializeField]
    List<SplinePoint> points = new List<SplinePoint>();
    public List<Vector3> rawVelocities = new List<Vector3>();

    LineRenderer line;
    public int resolution = 100;
    public List<Vector3> tensionedVelocities;

    // public GameObject stargate;
    
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


        for(int i = 0; i < tensionedVelocities.Count - 2; i++) {
            Debug.Log(tensionedVelocities[i] + tensionedVelocities[i + 1]);
        }

    }

    void DrawPoints()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Debug.DrawRay(points[i].point,  points[i].velocity, Color.blue, 1f);
            Debug.DrawRay(points[i].point, -points[i].velocity, Color.blue, 1f);

            // Debug.DrawRay(points[i].point, (points[i].point - transform.position).normalized * 10, Color.red, 1);
        }
    }

    void DrawSpline()
    {
        if (points.Count <= 1)  { return; }    // need at least two points to make a line

        List<Vector3> linePoints = new List<Vector3>();

        for (int i = 0; i < points.Count; i++)
        {
            for (int r = 0; r < resolution; r++)
            {
                float t = (float)r / (float)resolution;
                Vector3 point = FindSplinePoint(i, t);
                linePoints.Add(point);
            }
        }

        int count = linePoints.Count;
        line.positionCount = count;
        line.SetPositions(linePoints.ToArray());
    }

    [Range(0, 1)]
    public float tension = 0;
    // List<float> tensions;

    public float planetTension;
    public bool usePlanetTension = false;
    public Vector2 angleRange   = new Vector2(0, 90);
    public Vector2 tensionRange = new Vector2(0, 1);
    public float angleToNextPlanet;

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

    public void AddSplinePoint(SplinePoint p) {
        points.Add(p);
    }

    void SetCurveVelocity()
    {
        int count = points.Count;
        if (count < 2) { return; } // need at least 3 points to use hermite curve calculation

        rawVelocities = new List<Vector3>();
        tensionedVelocities = new List<Vector3>();

        for (int i = count - 1; i > 1; i--)
        {
            int nextIndex = i;
            int targetIndex = i - 1;
            int prevIndex = i - 2;

            // Debug.Log("nextIndex: " + nextIndex + "; target: " + target + "; prevIndex: " + prevIndex);

            Vector3 nextPoint = points[nextIndex].point;
            Vector3 prevPoint = points[prevIndex].point;

            Vector3 rawVelocity = (nextPoint - prevPoint) / 4;

            rawVelocities.Add(rawVelocity);


            // DEBUG
            if(usePlanetTension){
                planetTension = Extensions.mapRange(angleRange.x, angleRange.y, tensionRange.x, tensionRange.y, angleToNextPlanet);
                tension = planetTension;
            }


            Vector3 vel = (1 - planetTensions[targetIndex]) * rawVelocity;
            points[targetIndex].velocity = vel;


            tensionedVelocities.Add(vel);
            // Debug.Log("Set target[" + target + "] to " + vel);
        }
    }
}