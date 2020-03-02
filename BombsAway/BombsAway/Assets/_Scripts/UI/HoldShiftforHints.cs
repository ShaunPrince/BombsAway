using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldShiftforHints : MonoBehaviour
{
    public GameObject[] HintList;
    private int listlen;
    void Start()
    {
        for (int i = 0; i < HintList.Length; i++) {
            HintList[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            for (int i = 0; i < HintList.Length; i++)
            {
                HintList[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < HintList.Length; i++)
            {
                HintList[i].SetActive(false);
            }
        }
    }
}
