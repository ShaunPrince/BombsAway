using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTween : MonoBehaviour
{
    private float fadeTime = .3f;
    private bool currentlyFading = false;   // to deal with fades in the middle of fades
    // Start is called before the first frame update
    void Start()
    {

    }
    /*
    public void FadeOutFadeIn() {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 1.0f, "to", 0.0f,
            "time", fadeTime, "easetype", "linear",
            "onupdate", "setAlpha", "onComplete", "FadeIn"));

        //Debug.Log($"Fading out {this.transform.parent.name}");
    }*/

    public void FadeOut()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 1.0f, "to", 0.0f,
            "time", fadeTime, "easetype", "easeOutQuad",
            "onupdate", "setAlpha"));

        //Debug.Log($"Fading out {this.transform.parent.name}");
    }
    public void FadeIn()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", fadeTime, "easetype", "easeOutQuad",
            "onupdate", "setAlpha"));

        //Debug.Log($"Fading in {this.transform.parent.name}");
    }
    private void setAlpha(float newAlpha)
    {
        //foreach (Material mObj in renderer.materials)
        //{
        //    mObj.color = new Color(
        //        mObj.color.r, mObj.color.g,
        //        mObj.color.b, newAlpha);
        //}
        Color currentColor = this.GetComponent<RawImage>().color;
        Color changeColor = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        this.GetComponent<RawImage>().color = changeColor;


    }

}
