using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banger : MonoBehaviour
{
    enum state { ready, exploding, retracting };
    state currentState = state.ready;

    [System.Serializable]
    public class Movement
    {
        public float inputHorizontal;
        public float rotationSpeed = 10;
    }
    public Movement movement = new Movement();

    // float explosionSpeed;
    // float retractionSpeed;

    bool inputFire;

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        ProcessInput();
    }

    void CheckInput()
    {
        inputFire = Input.GetKey(KeyCode.Space);
        movement.inputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    void ProcessInput()
    {
        switch (currentState)
        {
            case state.ready:
                Move();
                CheckForExplosion();
                break;

            case state.exploding:
                CheckForRetraction();
                break;

            case state.retracting:
                break;

        }

    }

    void Move()
    {
        if (movement.inputHorizontal != 0)
        {
            transform.Rotate(Vector3.forward, movement.inputHorizontal * movement.rotationSpeed * Time.deltaTime);
        }
    }

    void CheckForExplosion()
    {
        if (inputFire)
        {
            StartCoroutine(DoExplosion());
        }
    }

    void CheckForRetraction()
    {
        if (!inputFire)
        {
            StartCoroutine(DoRetraction());
        }
    }

    void SetState(state newState)
    {
        currentState = newState;
    }

    float growth;
    public float maxGrowth = 10;
    float growthDuration = 1.0f;
    public AnimationCurve growthCurve;

    float retractDuration = .25f;
    public AnimationCurve decayCurve;


    public int numDots = 7;
    public GameObject dot;
    List<GameObject> dots = new List<GameObject>();

    IEnumerator DoExplosion()
    {
        SetState(state.exploding);


        for (int i = 0; i < numDots; i++)
        {
            GameObject d = Instantiate(dot) as GameObject;
            d.transform.rotation = transform.rotation;
            d.transform.Rotate(0, 0, ((float)i / (float)numDots) * 360);

            dots.Add(d);
        }

        float t = 0;
        float p = 0;

        if (inputFire)
        {

            while (t < growthDuration)
            {
                t += Time.fixedDeltaTime;
                p = t / growthDuration;
                p = Mathf.Clamp01(p);

                // holding "fire", keep growing
                growth = growthCurve.Evaluate(p) * maxGrowth;

                MoveDots();


                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            // "fire" was released, start retracting
            SetState(state.retracting);
        }


        yield return null;

    }

    IEnumerator DoRetraction()
    {

        SetState(state.retracting);
        yield return null;

        float t = 0;
        float p = 0;

        while (t < retractDuration)
        {
            t += Time.fixedDeltaTime;
            p = (float)t / (float)retractDuration;
            p = Mathf.Clamp01(p);

            growth = decayCurve.Evaluate(p) * maxGrowth;

            MoveDots();
            yield return new WaitForFixedUpdate();
        }

        // retraction complete, reset to ready 
        SetState(state.ready);

        foreach (GameObject d in dots)
        {
            Destroy(d);
        }

        dots.Clear();
    }

    void MoveDots()
    {
        if (dots.Count == 0) { return; }

        foreach (GameObject d in dots)
        {
            d.transform.position = transform.position + d.transform.up * growth;
        }
    }
}
