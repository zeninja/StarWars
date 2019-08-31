using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    // Implementing a realtime Hermite Curve spline based on Squirrel Eiserloh's talk https://www.gdcvault.com/play/1017922/Math-for-Game

    Vector3[] positions; // point locations
    int splineIndex = 0; // using "progressive t" ie. t increments at end of each segment
    Vector3 shipPos;

    void Update()
    {

        MoveShip();

    }

    void MoveShip()
    {
        // The "ship" is the point that travels along the spline

    }

}