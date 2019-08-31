using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinePoint : MonoBehaviour
{
    public int index;
    public Vector3 point = Vector3.zero;
    public Vector3 velocity = Vector3.zero;

    void LateUpdate() {
        point = transform.position;
        // velocity = transform.Find("Velocity").position;
    }
}
