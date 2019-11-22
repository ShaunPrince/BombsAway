using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
    // changes height and direction randomly

    private float deltaTime = 0f;
    private float timeBetween = 5f;

    private Flying flyingComponent;

    private void Start()
    {
        flyingComponent = GameObject.FindWithTag("PilotStation").GetComponent<Flying>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deltaTime >= timeBetween)
        {
            float randomAlt = Random.Range(100, 400);
            float randomDir = Random.Range(0, 360);
            float randomSpd = Random.Range(100, 300);

            flyingComponent.SetDesAlt(randomAlt);
            //flyingComponent.SetDesDir(randomDir);
            flyingComponent.SetDesSpeed(randomSpd);

            deltaTime = 0;
        }
        else
        {
            deltaTime += Time.deltaTime;
        }
    }
}
