using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeText : MonoBehaviour
{
    public void FadeIn()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", 1f, "easetype", "linear",
            "onupdate", "ChangeAlfa"));
    }

    private void ChangeAlfa(float amount)
    {
        Color ogColor = this.GetComponent<TMP_Text>().color;
        this.GetComponent<TMP_Text>().color = new Color(ogColor.r, ogColor.g, ogColor.b, amount);
    }
}
