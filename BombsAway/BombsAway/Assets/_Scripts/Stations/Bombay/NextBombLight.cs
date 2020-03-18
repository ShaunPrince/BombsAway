using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBombLight : MonoBehaviour
{
    public GameObject leftBombLight;
    public GameObject rightBombLight;
    public Material onMaterial;
    public Material offMaterial;
    //private float timeAmount = 1f;

    private ReloadManager reloader;
    private BombBayControls bombBaySript;
    private bool prevBomb = true;
    private bool lightTurnedOn = true;

    private void Start()
    {
        reloader = this.GetComponent<ReloadManager>();
        bombBaySript = this.GetComponentInChildren<BombBayControls>();
        leftBombLight.GetComponent<Renderer>().material = onMaterial;
        rightBombLight.GetComponent<Renderer>().material = offMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloader.getReloadingStatus() && lightTurnedOn && prevBomb != bombBaySript.LeftBombNext())
        {
            if (prevBomb)
            {
                leftBombLight.GetComponent<MaterialTweening>().MergeMaterialGlow(onMaterial, offMaterial);
                //leftBombLight.GetComponent<Renderer>().material = offMaterial;
                //Debug.Log("left off");
            }
            else
            {
                rightBombLight.GetComponent<MaterialTweening>().MergeMaterialGlow(onMaterial, offMaterial);
                //rightBombLight.GetComponent<Renderer>().material = offMaterial;
                //Debug.Log("right off");
            }
            lightTurnedOn = false;
        }
        if (!reloader.getReloadingStatus() && prevBomb != bombBaySript.LeftBombNext())
        {
            if (bombBaySript.LeftBombNext())
            {
                //leftBombLight.GetComponent<MaterialTweening>().MergeMaterialGlow(offMaterial, onMaterial);
                leftBombLight.GetComponent<Renderer>().material = onMaterial;
                //Debug.Log("left on");
            }
            else
            {
                //rightBombLight.GetComponent<MaterialTweening>().MergeMaterialGlow(offMaterial, onMaterial);
                rightBombLight.GetComponent<Renderer>().material = onMaterial;
                //Debug.Log("right on");
            }
            prevBomb = bombBaySript.LeftBombNext();
            lightTurnedOn = true;
        }
    }
}
