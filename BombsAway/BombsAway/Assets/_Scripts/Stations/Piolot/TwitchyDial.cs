using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchyDial : MonoBehaviour
{
    public GameObject arrow;

    private Vector2 range = new Vector2(-50f, 50f);
    private Vector2 desnity = new Vector2(0, 15f);
    private float maxTimer;
    private float timer = 0f;

    private float prevTwitch = 0f;
    private bool quickTwitch = true;
    // Start is called before the first frame update
    void Start()
    {
        maxTimer = ChooseTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= maxTimer)
        {
            ChooseIfQuickTwitch();
            RandomTwitch();
            timer = 0f;
            maxTimer = ChooseTimer();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void RandomTwitch()
    {
        float angle;
        float time;
        if (!quickTwitch)
        {
            angle = Random.Range(range.x, range.y);
            time = 7f;
        }
        else
        {
            if (Random.Range(0f, 1f) > .5f) angle = prevTwitch + 10f;
            else angle = prevTwitch - 10f;
            time = 1f;
        }

        arrow.GetComponent<RotationZTween>().TweenRotation(angle, time);
        prevTwitch = angle;

    }

    private float ChooseTimer()
    {
        if (!quickTwitch)
        {
            return Random.Range(desnity.x, desnity.y);
        }
        else
        {
            return Random.Range(.5f, 3f);
        }
    }

    private void ChooseIfQuickTwitch()
    {
        if (Random.Range(0f, 1f) > .6f)
        {
            quickTwitch = true;
            //Debug.Log($"QUICK TWITCH");
        }
        else
        {
            quickTwitch = false;
            //Debug.Log($"Normal");
        }
    }
}
