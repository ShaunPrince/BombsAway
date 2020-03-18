using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLightTrigger : MonoBehaviour
{
    public GameObject bombLight;
    public Material onMaterial;
    public Material offMaterial;
    private float maxTime = .7f;
    private float timer = 0f;

    private float maxNumBombs;
    private BombBayControls bombBaySript;
    private bool lightSet = false;
    private bool lightOn = true;

    private void Start()
    {
        bombBaySript = this.GetComponentInChildren<BombBayControls>();
        maxNumBombs = bombBaySript.numOfBombs;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lightSet && bombBaySript.numOfBombs < maxNumBombs / 3)
        {
            lightSet = true;
            //bombLight.GetComponent<MaterialTweening>().MergeMaterialGlow(onMaterial, offMaterial);
            bombLight.GetComponent<Renderer>().material = offMaterial;
            lightOn = false;
        }
        if (lightSet)
        {
            if (timer > maxTime)
            {
                timer = 0f;
                if (lightOn)
                {
                    //bombLight.GetComponent<MaterialTweening>().MergeMaterialGlow(onMaterial, offMaterial);
                    bombLight.GetComponent<Renderer>().material = offMaterial;
                    lightOn = false;
                }
                else
                {
                    //bombLight.GetComponent<MaterialTweening>().MergeMaterialGlow(offMaterial, onMaterial);
                    bombLight.GetComponent<Renderer>().material = onMaterial;
                    lightOn = true;
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
