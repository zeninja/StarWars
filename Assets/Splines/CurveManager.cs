using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CurveManager : MonoBehaviour
{
    // public SplineRenderer splinePrefab;
    List<Vector3> positionList = new List<Vector3>();
    // List<Vector3> velocityList = new List<Vector3>();

    // List<SplineRenderer> splineRenderers = new List<SplineRenderer>();

    public void AddPointToList(Vector3 point)
    {
        if (positionList.Contains(point)) { return; }

        int pointIndex = positionList.Count;
        positionList.Add(point);

        
    }





















    // Curve ConvertPointsToCurve(Vector3[] points)
    // {
    //     Curve c = new Curve();

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

    // void HandlePointAdded()
    // {
    //     for (int i = 0; i < positionList.Count; i++)
    //     {

    //     }
    // }






    // if (points.Count <= 1) { return; }

    // int curveIndex = curves.Count;
    // Curve c = SpawnCurve();
    // FindCurveVelocity(c, curveIndex);
    // curves.Add(c);


    // SplineRenderer line = Instantiate(splinePrefab);
    // line.SetPositionsFromCurve(c);
    // splineRenderers.Add(line);

    // for (int i = 0; i < splineRenderers.Count - 1; i++)
    // {
    //     splineRenderers[i].SetPositionsFromCurve(curves[i]);
    // }
    // }



    // Curve SpawnCurve()
    // {
    //     Debug.Log(points.Count);

    //     int prevPt = points.Count - 2;
    //     int lastPt = points.Count - 1;
    //     Curve c = new Curve();
    //     c.start.position = points[prevPt];
    //     c.end.position = points[lastPt];

    //     c.start.velocity = Vector3.zero;
    //     c.end.velocity = Vector3.zero;

    //     curves.Add(c);
    //     return c;
    // }

    // void FindCurveVelocity(Curve c, int curveIndex)
    // {
    //     if (curveIndex < 1) { return; }
    //     Curve prevCurve = curves[curveIndex - 1];

    //     Vector3 pt0 = prevCurve.start.position;
    //     Vector3 pt2 = c.end.position;

    //     Vector3 vel = (pt2 - pt0) / 2;
    //     c.start.velocity = vel;
    //     prevCurve.end.velocity = vel;
    // }
}
