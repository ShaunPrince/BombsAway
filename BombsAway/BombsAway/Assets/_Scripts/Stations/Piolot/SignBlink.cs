using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignBlink : MonoBehaviour
{
    private float maxTime = .6f;
    private float timer = 0f;
    private bool blinking = false;
    private bool fadingColor = false;
    private bool prevColorLit = true;
    private Color litColor;
    private Color unlitColor;
    // Start is called before the first frame update
    void Start()
    {
        litColor = Color.red;
        unlitColor = new Color(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (blinking)
        {
            if (!fadingColor) Fade();
            
            if (timer > maxTime)
            {
                fadingColor = false;
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else if (timer > 0)
        {
            timer = 0f;
        }
    }

    public void Blink(bool blinkStatus)
    {
        blinking = blinkStatus;
        this.gameObject.GetComponent<TMP_Text>().CrossFadeColor(Color.red, maxTime, false, false);
    }

    private void Fade()
    {
        fadingColor = true;
        if (prevColorLit)
        {
            //Debug.Log("FadingUp");
            //this.gameObject.GetComponent<Image>().CrossFadeColor(unlitColor, maxTime, false, false);
            this.gameObject.GetComponent<TMP_Text>().CrossFadeColor(Color.black, maxTime, false, false); ;
            prevColorLit = false;
        }
        else
        {
            //Debug.Log("FadingDown");
            //this.gameObject.GetComponent<Image>().CrossFadeColor(litColor, maxTime, false, false);
            this.gameObject.GetComponent<TMP_Text>().CrossFadeColor(Color.red, maxTime, false, false);
            prevColorLit = true;
        }
    }
}
