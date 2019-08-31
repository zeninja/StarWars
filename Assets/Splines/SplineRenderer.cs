using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SplineRenderer : MonoBehaviour
{
    LineRenderer line;
    SplinePoint startPt = new SplinePoint();
    SplinePoint endPt = new SplinePoint();

    public Transform start, end, startVelocity, endVelocity;
    public int resolution = 100;


    void Awake()
    {
        line = GetComponent<LineRenderer>();

        // start = transform.Find("Start");
        // startVelocity = transform.Find("Start/Velocity");
        // end = transform.Find("End");
        // endVelocity = transform.Find("End/Velocity");

    }

    void Start()
    {
        UpdateSplinePositions();
    }

    void LateUpdate()
    {
        DrawSpline();
        UpdateSplinePositions();
    }

    public void SetPositions(Vector3[] positions)
    {
        startPt.point = positions[0];
        startPt.velocity = positions[1];
        endPt.point   = positions[2];
        endPt.velocity   = positions[3];

        start.position         = startPt.point;
        startVelocity.position = startPt.velocity;
        end.position           = endPt.point;
        endVelocity.position   = -endPt.velocity;                   // this is negative so that it is treated as a velocity instead of a control point
    }

    void UpdateSplinePositions()
    {
        startPt.point = start.position;
        startPt.velocity = startVelocity.localPosition;
        endPt.point   = end.position;
        endPt.velocity   = -endVelocity.localPosition;          
    }

    // public void SetPositionsFromCurve(Extensions.Splines.Curve c) {
    //     start.position              = c.start.position;
    //     end.position                = c.end.position;

    //     startVelocity.localPosition = c.start.velocity;
    //     endVelocity.localPosition   = c.end.velocity;
    // }

    // public void MatchCurve(Extensions.Splines.Curve c)
    // {
    //     start.position         = c.start.position;
    //     startVelocity.position = c.start.velocity;
    //     end.position           = c.end.position;
    //     endVelocity.position   = c.end.velocity;

    //     Debug.Log("Curve matched");
    // }

    void DrawSpline()
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < resolution; i++)
        {
            float p = (float)i / (float)resolution;

            positions.Add(Extensions.Splines.CubicBezier(startPt, endPt, p));
        }
        positions.Add(Extensions.Splines.CubicBezier(startPt, endPt, 1));

        line.positionCount = positions.Count;
        line.SetPositions(positions.ToArray());
    }
}
