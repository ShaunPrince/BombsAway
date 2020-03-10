using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalDoubleDigits : MonoBehaviour
{
    public GameObject[] tensNums;
    public GameObject[] onesNumbs;

    private int prevTens;
    private int prevOnes;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject num in tensNums)
        {
            num.SetActive(false);
        }
        foreach (GameObject num in onesNumbs)
        {
            num.SetActive(false);
        }

        prevTens = 0;
        prevOnes = 0;
    }

    public void SetDoubleNumber(int number)
    {
        int tens = number / 10;
        int ones = number % 10;

        tensNums[prevTens].SetActive(false);
        onesNumbs[prevOnes].SetActive(false);

        tensNums[tens].SetActive(true);
        onesNumbs[ones].SetActive(true);

        prevTens = tens;
        prevOnes = ones;
    }
}
