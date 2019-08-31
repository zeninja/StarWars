using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public PlanetSelector planetSelector;

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            LookForPlanets3D();
        }
    }

    void LookForPlanets3D()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mousePos, Vector3.forward, out hit, 10))
        {
            planetSelector.SelectPlanet(hit.collider.gameObject.GetComponentInParent<Planet>());            
            // planetSpline.AddPointToList(hit.collider.gameObject.transform.position);
        }
    }

    // void LookForPlanets2D()
    // {
    //     Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, Camera.main.transform.position.z);
    //     RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0);

    //     Debug.DrawRay(mousePos, Vector3.forward, Color.red, 1f);

    //     if(hit) {
    //         Debug.Log(" - - - 2D - - -");
    //         Debug.Log(hit.collider.gameObject.name);
    //         SelectPlanet(hit.collider.gameObject.GetComponentInParent<Planet>());
    //     }
    // }
}
