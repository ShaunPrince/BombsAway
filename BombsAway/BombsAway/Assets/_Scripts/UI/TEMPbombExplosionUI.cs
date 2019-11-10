using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEMPbombExplosionUI : MonoBehaviour
{
    public static Image tempExplosion;
    private float timeDisplayImage = 10f;
    private float timeDisplaying = 0f;

    // Start is called before the first frame update
    void Start()
    {
        tempExplosion = GameObject.Find("TempExplosion").GetComponent<Image>();
        tempExplosion.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDisplaying >= timeDisplayImage)
        {
            timeDisplaying = 0f;
            tempExplosion.enabled = false;
        }
        else
        {
            timeDisplaying += Time.deltaTime;
        }
    }

    public static void MakeExplosion()
    {
        tempExplosion.enabled = true;
    }
}
