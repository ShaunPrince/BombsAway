using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombAnimation : MonoBehaviour
{
    public GameObject bombModel;
    public Transform dropPoint;
    public float speed = 1f;
    private bool animate = false;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

        //AVA IS CUTEST
    void Update()
    {
        if (animate)
        {
            AnimateBomb();
        }
    }

    public void SetDropPoint(Transform point)
    {
        dropPoint = point;
    }

    public void Animate()
    {
        bombModel.transform.parent = bombModel.transform.parent.transform.parent;
        startTime = Time.time;
        journeyLength = Vector3.Distance(bombModel.transform.position, dropPoint.position);
        animate = true;
    }

    private void AnimateBomb()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        bombModel.transform.position = Vector3.Lerp(bombModel.transform.position, dropPoint.position, fractionOfJourney);
        //Debug.Log($"{fractionOfJourney}");

        if (fractionOfJourney > .1)
        {
            GameObject.Destroy(bombModel);
            animate = false;
        }
    }
}
