using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTweening : MonoBehaviour
{
    public GameObject tankColorChanger;
    private float fadeTime = 1f;
    private Material startMaterial;
    private Material endMaterial;
    // this gameobject

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MergeMaterial(Material startMat, Material endMat)
    {
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", fadeTime, "easetype", "linear",
            "onupdate", "LerpMaterial"));

        //Debug.Log($"Merging materials on {this.transform.parent.name} from {startMaterial.color} to {endMaterial.color}");
    }

    public void MergeMaterial(GameObject objectToChange, Material startMat, Material endMat, float time)
    {
        tankColorChanger = objectToChange;
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(objectToChange, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", time, "easetype", "linear",
            "onupdate", "LerpMaterial"));

        Debug.Log($"Tweening {objectToChange.name}, from {startMat} to {endMat}");
    }

    private void LerpMaterial(float time)
    {
        tankColorChanger.GetComponent<MeshRenderer>().material.Lerp(startMaterial, endMaterial, time);
    }

    public void FlickerMaterial(Material startMat, Material endMat)
    {
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f, "to", .3f,
            "time", .3f, "easetype", "linear",
            "onupdate", "LerpMaterial", "oncomplete", "TriggerFlicker"));
    }

    public void TriggerFlicker()
    {
        this.GetComponent<TankStatusColor>().ResetPrevFillLevel();
    }
}
