using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float currentFillLevel;
    public float maxFillLevel;
    public float fillRate;
    public bool isConnectedToSource;
    public Junction tankJunction;

    private void Update()
    {
        isConnectedToSource = tankJunction.isConnectedToSource;
        //Debug.Log(isConnectedToSource);
        FillIfConnected();
    }

    public void LoseSteam(float steamLoss)
    {
        currentFillLevel -= steamLoss;
        if(currentFillLevel < 0)
        {
            currentFillLevel = 0;
        }
    }

    private void FillIfConnected()
    {
        if(isConnectedToSource)
        {
            currentFillLevel += fillRate * Time.deltaTime;
            if(currentFillLevel > maxFillLevel)
            {
                currentFillLevel = maxFillLevel;
            }
        }

    }

    public bool IsFilled()
    {
        if(currentFillLevel >= maxFillLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
