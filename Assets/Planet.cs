using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Assets.Scripts;

public class Planet : MonoBehaviour
{
    public float size;
    public float radius = 2f;
    public enum GateState { empty, hasIn, };
    public GateState gateStatus = GateState.empty;

    public SplinePoint splinePointPrefab;

    public void HandleSelection(Vector3 dir, bool firstPoint = false) {
        if (firstPoint) {
           // Do unique first point stuff

        }

        switch(gateStatus) {
            case GateState.empty:
                // Add In Point
                dirToLastPlanet = dir;
                gateStatus = GateState.hasIn;

                AddPoint(dirToLastPlanet);
                
                break;
            case GateState.hasIn:
                // Add out
                dirToNextPlanet = dir;

                Vector3 last = dirToLastPlanet.normalized;
                Vector3 next = dirToNextPlanet.normalized;
                Vector3 midPtDirection = (last + next) / 2;

                AddPoint(midPtDirection);
                AddPoint(dirToNextPlanet);

                angleToNextPlanet = 180 - Vector3.Angle(next, last);
                Debug.Log(angleToNextPlanet);

                ConvertPlanetTension();

                break;
        }
    }

    public Vector2 angleRange;
    public Vector2 tensionRange;

    void ConvertPlanetTension() {
        // angleToNextPlanet = 90; // .459

    }

    Vector3 dirToLastPlanet;
    Vector3 dirToNextPlanet;

    public float angleToNextPlanet;

    void Update() {
        // Vector3 last = dirToLastPlanet.normalized;
        // Vector3 next = dirToNextPlanet.normalized;

        // Debug.DrawRay(transform.position, last, Color.blue, 1f);
        // Debug.DrawRay(transform.position, next, Color.red, 1f);

        // Debug.DrawRay(transform.position, -(last + next) / 2, Color.green, 1f);
    }

    void AddPoint(Vector3 direction) {
        SplinePoint p = Instantiate(splinePointPrefab);

        Vector3 vec1 = direction;
        Vector3 vec2 = Vector3.back;

        Vector3 normal = Vector3.Cross(vec1, vec2);

        p.transform.position = transform.position + -normal.normalized * radius;
        
        p.GetComponent<Stargate>().SetInfo(this, -normal);

        PlanetSpline.GetInstance().AddSplinePoint(p);
    }

    // GameObject inPt;
    // GameObject outPt;
    // GameObject mid;


    // public void AddOutPoint()
    // {
    //     if (outPt != null) { return; }
    //     outPt = Instantiate(splinePointPrefab);

    //     Vector3 vec1 = linkInfo.outDir;
    //     Vector3 vec2 = Vector3.back;

    //     Vector3 normal = Vector3.Cross(vec1, vec2);

    //     outPt.transform.position = transform.position + normal.normalized * radius;
    //     // outPt.GetComponent<Stargate>().SetInfo(this, normal);
    //     outPt.GetComponent<Stargate>().myPlanet = this;
    //     outPt.GetComponent<Stargate>().directionToPlanet = normal;
    //     outPt.GetComponent<SplinePoint>().point = outPt.transform.position;

    //     if (inPt != null) {
    //         AddMidPoint();
    //     }
    //     // PlanetSpline.GetInstance().AddSplinePoint(outPt);
    // }

    // void AddMidPoint() {
    //     Debug.Log(outPt.GetComponent<Stargate>().directionToPlanet + "; " + inPt.GetComponent<Stargate>().directionToPlanet);

    //     Vector3 dir = (outPt.GetComponent<Stargate>().directionToPlanet + inPt.GetComponent<Stargate>().directionToPlanet) / 2;
    //     Vector3 norm = dir.normalized;

    //     midPt = Instantiate(splinePointPrefab);
    //     // midPt.GetComponent<Stargate>().SetInfo(this, norm);
    //     midPt.GetComponent<Stargate>().myPlanet = this;
    //     midPt.GetComponent<Stargate>().directionToPlanet = norm;
    //     midPt.GetComponent<SplinePoint>().point = midPt.transform.position;

    //     // PlanetSpline.GetInstance().AddSplinePoint(midPt);
    // }

    // public void AddInPoint()
    // {
    //     if (inPt != null) { return; }
    //     inPt = Instantiate(splinePointPrefab);

    //     Vector3 vec1 = linkInfo.inDir;
    //     Vector3 vec2 = Vector3.back;

    //     Vector3 normal = Vector3.Cross(vec1, vec2);

    //     inPt.transform.position = transform.position + normal.normalized * radius;

    //     inPt.GetComponent<Stargate>().directionToPlanet = normal;
    //     inPt.GetComponent<Stargate>().myPlanet = this;

    //     inPt.GetComponent<SplinePoint>().point = inPt.transform.position;
    //     // PlanetSpline.GetInstance().AddSplinePoint(inPt);

    //     // Debug.Log(outPt.GetComponent<Stargate>().directionToPlanet + "; " + inPt.GetComponent<Stargate>().directionToPlanet);
    // }
}
