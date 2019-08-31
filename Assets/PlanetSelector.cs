using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelector : MonoBehaviour
{

    private static PlanetSelector instance;
    public static PlanetSelector GetInstance()
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

    public List<Planet> selectedPlanets = new List<Planet>();
    LineRenderer line;
    List<Vector3> selectedPlanetPositions;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        selectedPlanetPositions = new List<Vector3>();

    }

    void Update()
    {
        DrawSelectedPlanets();
    }

    Planet selectedPlanet;

    // public void SelectPlanet(Planet newPlanet)
    // {
    //     // if (newPlanet.linkInfo.inLine)
    //     // {
    //     //     // if(!CompletesLoop(newPlanet)) { return; }
    //     //     return;
    //     // }
    //     if (selectedPlanets.Count == 0) { newPlanet.linkInfo.lineStart = true; }

    //     // selectedPlanet = selectedPlanets[selectedPlanets.Count - 1]; 
    //     newPlanet.linkInfo.inLine = true;

    //     if (selectedPlanets.Count > 0)
    //     {
    //         Vector3 direction = newPlanet.transform.position - selectedPlanet.transform.position;

    //         // selectedPlanet.linkInfo.outDir = direction;
    //         // selectedPlanet.AddOutPoint();

    //         // newPlanet.linkInfo.inDir = direction;
    //         // newPlanet.AddInPoint();
    //     }

    //     selectedPlanet = newPlanet;
    //     selectedPlanets.Add(newPlanet);
    // }

    public void SelectPlanet(Planet newPlanet) {
        if(selectedPlanets.Contains(newPlanet)) { return; }

        if (selectedPlanets.Count > 0)
        {
            Vector3 direction = selectedPlanet.transform.position - newPlanet.transform.position;

            selectedPlanet.HandleSelection(direction);
            newPlanet.HandleSelection(direction);
        }

        selectedPlanet = newPlanet;
        selectedPlanets.Add(newPlanet);
    }

    void DrawSelectedPlanets()
    {
        GetPlanetPositions();
        line.positionCount = selectedPlanetPositions.Count;
        line.SetPositions(selectedPlanetPositions.ToArray());
    }

    public List<Vector3> GetPlanetPositions()
    {
        selectedPlanetPositions = new List<Vector3>();

        foreach (Planet p in selectedPlanets)
        {
            selectedPlanetPositions.Add(p.transform.position);
        }
        return selectedPlanetPositions;
    }


    // public List<Planet> GetSelectedPlanets()
    // {
    //     return selectedPlanets;
    // }

    // public bool CompletesLoop(Planet newPlanet)
    // {
    //     return newPlanet.linkInfo.lineStart;
    // }

    // void ResetPlanetSelection()
    // {
    //     selectedPlanets = new List<Planet>();
    // }

}
