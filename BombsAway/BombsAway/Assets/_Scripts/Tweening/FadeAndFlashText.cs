using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class FadeAndFlashText : MonoBehaviour
{
    public bool setActive = false;
    public void FlashDeathReason()
    {
        iTween.ValueTo(this.gameObject, iTween.Hash("from", 0f, "to", 1f,
                                                            "time", 2f, "easetype", "linear",
                                                            "onupdate", "setAlpha",
                                                            "looptype", "pingPong"));
    }

    private void setAlpha(float newAlpha)
    {
        Color currentColor = this.GetComponent<TMP_Text>().color;
        Color changeColor = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        this.GetComponent<TMP_Text>().color = changeColor;
    }
}
