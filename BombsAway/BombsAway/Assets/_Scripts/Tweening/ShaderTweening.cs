using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Color_14CB3FDC
public class ShaderTweening : MonoBehaviour
{
    [Range(0, 1)]
    public float colorDifference;
    private float fadeTime = .1f;

    public GameObject enemyModels;
    private Color[] originalColor;
    private Color[] whiteColor; // save max white color
    // Start is called before the first frame update
    void Start()
    {
        originalColor = new Color[enemyModels.transform.childCount];
        whiteColor = new Color[enemyModels.transform.childCount];


        for (int i = 0; i < originalColor.Length; i++)
        {
            Renderer renderer;
            if (enemyModels.transform.GetChild(i).GetComponent<MeshRenderer>() && enemyModels.transform.GetChild(i).TryGetComponent<Renderer>(out renderer))
            {
                originalColor[i] = renderer.material.GetColor("Color_14CB3FDC");
                whiteColor[i] = new Color(originalColor[i].r + colorDifference, originalColor[i].g + colorDifference, originalColor[i].b + colorDifference);
            }
        }
    }

    public void FlashWhite()
    {
        FadeOut();
    }

    private void FadeOut()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f, "to", colorDifference,
            "time", fadeTime, "easetype", "linear",
            "onupdate", "ChangeToWhite", "onComplete", "FadeIn"));

        //Debug.Log($"Fading out {this.transform.parent.name}");
    }

    private void FadeIn()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f, "to", colorDifference,
            "time", fadeTime, "easetype", "linear",
            "onupdate", "ReturnToColor"));

        //Debug.Log($"Fading in {this.transform.parent.name}");
    }
    private void ChangeToWhite(float colorChange)
    {
        for (int i = 0; i < originalColor.Length; i++)
        {
            Renderer renderer;
            if (enemyModels.transform.GetChild(i).GetComponent<MeshRenderer>() && enemyModels.transform.GetChild(i).TryGetComponent<Renderer>(out renderer))
            {
                Color newColor = new Color(originalColor[i].r + colorChange, originalColor[i].g + colorChange, originalColor[i].b + colorChange);
                //MaterialPropertyBlock block = new MaterialPropertyBlock();
                //block.SetColor("_BaseColor", newColor);

                enemyModels.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("Color_14CB3FDC", newColor);
            }
        }
    }

    private void ReturnToColor(float colorChange)
    {
        for (int i = 0; i < originalColor.Length; i++)
        {
            Renderer renderer;
            if (enemyModels.transform.GetChild(i).GetComponent<MeshRenderer>() && enemyModels.transform.GetChild(i).TryGetComponent<Renderer>(out renderer))
            {
                Color newColor = new Color(whiteColor[i].r - colorChange, whiteColor[i].g - colorChange, whiteColor[i].b - colorChange);
                MaterialPropertyBlock block = new MaterialPropertyBlock();
                block.SetColor("_BaseColor", newColor);

                enemyModels.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("Color_14CB3FDC", newColor);
            }
        }
    }
}
