using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGraphics : MonoBehaviour
{
    Planet planet;

    // Start is called before the first frame update
    void Start()
    {
        planet = GetComponentInParent<Planet>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one * planet.size;
    }
}
