using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLightTrigger : MonoBehaviour
{
    public GameObject bombLight;
    public Material onMaterial;
    public Material offMaterial;
    private float timeAmount = 1f;

    private float maxNumBombs;
    private BombBayControls bombBaySript;
    private bool lightSet = false;

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
            bombLight.GetComponent<MaterialTweening>().FlickerMaterial(offMaterial, onMaterial, timeAmount);
        }
    }
}
