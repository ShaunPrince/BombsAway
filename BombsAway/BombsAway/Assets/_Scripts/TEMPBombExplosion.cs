using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEMPBombExplosion : MonoBehaviour
{
    public GameObject tempExplosionObject;
    private GameObject currentExplosion;
    private float timeDisplayImage = 10f;
    private float timeDisplaying = 0f;

    // Update is called once per frame
    void Update()
    {
        if (timeDisplaying >= timeDisplayImage)
        {
            timeDisplaying = 0f;
            Destroy(currentExplosion);
        }
        else
        {
            timeDisplaying += Time.deltaTime;
        }
    }

    public void MakeExplosion(Vector3 pos)
    {
        pos.y += 300;
        currentExplosion = Instantiate(tempExplosionObject, pos, new Quaternion(0,0,0,0));

    }
}
