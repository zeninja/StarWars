using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class NewSplineRenderer : MonoBehaviour
{
    LineRenderer line;

    List<Vector3> pointList = new List<Vector3>();
    int resolutionPerLine = 100;

    void Start() {
        line = GetComponent<LineRenderer>();
    }

    void FindLinePositions() {
        
    }
}
