﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombCountUI : MonoBehaviour
{
    //public TextMeshProUGUI bombCountText;
    public PhysicalDoubleDigits counterDigits;

    private BombBayControls bombBaySript;
    private int prevBombCount;

    // Start is called before the first frame update
    void Start()
    {
        bombBaySript = this.GetComponentInChildren<BombBayControls>();
        prevBombCount = bombBaySript.numOfBombs;
        //bombCountText.text = prevBombCount.ToString();
        counterDigits.SetDoubleNumber(prevBombCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (prevBombCount != bombBaySript.numOfBombs)
        {
            prevBombCount = bombBaySript.numOfBombs;
            //bombCountText.text = prevBombCount.ToString();
            counterDigits.SetDoubleNumber(prevBombCount);
        }
    }
}
