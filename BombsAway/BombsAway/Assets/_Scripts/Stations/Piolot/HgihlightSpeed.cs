using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HgihlightSpeed : MonoBehaviour
{
    public TMP_Text[] speedTexts;
    public Color deselectedColor;
    public Color highlightedColor;

    private int prevIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HighlightSpeed(ESpeeds index)
    {
        // hihlight the speed that matches the current speed
        speedTexts[prevIndex].color = deselectedColor;
        speedTexts[(int)index].color = highlightedColor;
        prevIndex = (int)index;
    }
}
