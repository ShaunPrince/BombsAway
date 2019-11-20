using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadManager : MonoBehaviour
{
    private float timer;
    private float timeLimit;
    private bool currentlyReloadingObj;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        timeLimit = 0.0f;
        currentlyReloadingObj = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyReloadingObj)
        {
            if (timer < timeLimit)
            {
                timer += Time.deltaTime;
            }
            else
            {
                currentlyReloadingObj = false;
                timer = 0.0f;
            }
        }
    }

    public void ReloadWeapon(float timeToReload)
    {
        timeLimit = timeToReload;
        currentlyReloadingObj = true;
    }

    public bool getReloadingStatus()
    {
        return currentlyReloadingObj;
    }
}
