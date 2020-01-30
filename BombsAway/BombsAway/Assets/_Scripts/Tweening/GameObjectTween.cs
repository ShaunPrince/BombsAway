using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectTween : MonoBehaviour
{
    private float fadeTime = .5f;
    private bool currentlyFading = false;   // to deal with fades in the middle of fades
    // Start is called before the first frame update

    public void FadeOut()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 1.0f, "to", 0.0f,
            "time", fadeTime, "easetype", "easeOutQuad",
            "onupdate", "setFade", "oncomplete", "SetHidden"));

        //Debug.Log($"Fading out {this.transform.parent.name}");
    }
    public void FadeIn()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", fadeTime, "easetype", "easeOutQuad",
            "onupdate", "setFade"));

        //Debug.Log($"Fading in {this.transform.parent.name}");
    }
    private void setFade(float newAlpha)
    {
        //foreach (Material mObj in renderer.materials)
        //{
        //    mObj.color = new Color(
        //        mObj.color.r, mObj.color.g,
        //        mObj.color.b, newAlpha);
        //}

        Debug.Log($"{this.name} has {this.transform.GetChild(0).transform.childCount} children with last one at alpha {newAlpha}");
        Color currentColor = this.transform.GetChild(0).transform.GetChild(this.transform.childCount - 1).GetComponent<RawImage>().color;
        Color changeColor = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        this.transform.GetChild(this.transform.childCount - 1).GetComponent<RawImage>().color = changeColor;
    }

    private void SetHidden()
    {
        this.gameObject.SetActive(false);
    }
}
