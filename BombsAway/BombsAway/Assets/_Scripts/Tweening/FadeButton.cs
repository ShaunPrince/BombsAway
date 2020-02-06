using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeButton : MonoBehaviour
{
    public void FadeIn()
    {
        this.GetComponent<Button>().interactable = false;
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", 1f, "easetype", "linear",
            "onupdate", "ChangeAlfa", "oncomplete", "ReEnable"));
    }

    private void ChangeAlfa(float amount)
    {
        Color disabledColor = this.GetComponent<Button>().colors.disabledColor;
        Color newColor = new Color(disabledColor.r, disabledColor.g, disabledColor.b, amount);
        ColorBlock colorBlock = this.GetComponent<Button>().colors;
        colorBlock.disabledColor = newColor;
        this.GetComponent<Button>().colors = colorBlock;
    }

    private void ReEnable()
    {
        this.GetComponent<Button>().interactable = true;
    }

}
