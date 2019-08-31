using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stargate : MonoBehaviour
{
    public Planet myPlanet;
    public Vector3 directionToPlanet;

    void Update()
    {
        transform.position = myPlanet.transform.position + directionToPlanet.normalized * myPlanet.radius;

        DrawVector();
    }

    public void SetInfo(Planet p, Vector3 n) {
        myPlanet = p;
        directionToPlanet = n;
    }

      //      //      //      //      //      //      //      //       //      //     //
     //      //      //      //      //    DEBUG     //      //       //      //     //
    //      //      //      //      //      //      //      //       //      //     //

    void DrawVector() {
        Debug.DrawRay(transform.position, directionToPlanet * 10, Color.red, 1f);
    }

}
