using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackWhiteTween : MonoBehaviour
{
    private float prevColorValue;

    private void Start()
    {
        prevColorValue = this.GetComponent<Image>().color.r;
    }

    public void TweenColor(bool white, float time)
    {
        if (!white)
        {
            this.gameObject.GetComponent<Image>().CrossFadeColor(Color.white, time, false, false);
            //iTween.ValueTo(this.gameObject, iTween.Hash("from", prevColorValue, "to", 255f,
            //                                            "time", time, "easetype", "linear",
            //                                            "onupdate", "UpdateColor"));
        }
        else
        {
            this.gameObject.GetComponent<Image>().CrossFadeColor(new Color(55f,55f,55f), time, false, false);
            //iTween.ValueTo(this.gameObject, iTween.Hash("from", prevColorValue, "to", 55f,
            //                                            "time", time, "easetype", "linear",
            //                                            "onupdate", "UpdateColor"));
        }
    }

    private void UpdateColor(float amount)
    {
        Color newColor = new Color((int)amount, (int)amount, (int)amount);
        this.gameObject.GetComponent<Image>().color = newColor;
        prevColorValue = amount;
        Debug.Log($"{this.name} {this.gameObject.GetComponent<Image>().color}");
    }
}
