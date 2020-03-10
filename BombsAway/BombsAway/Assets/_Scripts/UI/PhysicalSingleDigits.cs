using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalSingleDigits : MonoBehaviour
{
    public GameObject[] onesNumbs;

    private int prevOnes;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject num in onesNumbs)
        {
            num.SetActive(false);
        }

        prevOnes = 0;
    }

    public void SetNumber(int number)
    {
        int ones = number % 10;

        onesNumbs[prevOnes].SetActive(false);

        onesNumbs[ones].SetActive(true);

        prevOnes = ones;
    }
}
