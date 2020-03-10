using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialTweening : MonoBehaviour
{
    public GameObject parent;
    public GameObject objectToColorChange;
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
        iTween.ValueTo(objectToColorChange, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", fadeTime, "easetype", "linear",
            "onupdate", "LerpMaterial"));

        //Debug.Log($"Merging materials on {this.transform.parent.name} from {startMaterial.color} to {endMaterial.color}");
    }

    public void MergeMaterial(Material startMat, Material endMat, float time)
    {
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(objectToColorChange, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", time, "easetype", "linear",
            "onupdate", "LerpMaterial"));

        //Debug.Log($"Merging materials on {this.transform.parent.name} from {startMaterial.color} to {endMaterial.color}");
    }

    public void MergeMaterialGlow(Material startMat, Material endMat)
    {
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(objectToColorChange, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", fadeTime, "easetype", "linear",
            "onupdate", "LerpMaterial", "oncomplete", "TriggerGlow"));
    }

    private void TriggerGlow()
    {
        objectToColorChange.GetComponent<Renderer>().material = endMaterial;
    }

    public void MergeMultipleMaterial(Material startMat, Material endMat, float time)
    {
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(objectToColorChange, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", time, "easetype", "linear",
            "onupdate", "LerpMultMaterial"));

        //Debug.Log($"Merging materials on {this.transform.parent.name} from {startMaterial.color} to {endMaterial.color}");
    }

    public void MergeMaterial(GameObject objectToChange, Material startMat, Material endMat, float time)
    {
        objectToColorChange = objectToChange;
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(objectToChange, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", time, "easetype", "linear",
            "onupdate", "LerpMaterial"));

        //Debug.Log($"Tweening {objectToChange.name}, from {startMat} to {endMat}");
    }

    private void LerpMaterial(float time)
    {
        //Debug.Log($"Lerping {tankColorChanger} from {startMaterial} to {endMaterial}");
        objectToColorChange.GetComponent<Renderer>().material.Lerp(startMaterial, endMaterial, time);
    }

    private void LerpMultMaterial(float time)
    {
        foreach (MeshRenderer mr in objectToColorChange.GetComponentsInChildren<MeshRenderer>())
        {
            //Debug.Log($"Lerping {mr.name} from {startMaterial} to {endMaterial}");
            mr.material.Lerp(startMaterial, endMaterial, time);
        }
    }

    public void FlickerMaterial(Material startMat, Material endMat)
    {
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(objectToColorChange, iTween.Hash(
            "from", 0f, "to", .3f,
            "time", .3f, "easetype", "linear",
            "onupdate", "LerpMaterial", "oncomplete", "TriggerFlicker"));
    }

    public void FlickerMaterial(Material startMat, Material endMat, float time)
    {
        //Debug.Log("flicker");
        startMaterial = startMat;
        endMaterial = endMat;
        fadeTime = time;
        iTween.ValueTo(objectToColorChange, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", time, "easetype", "linear",
            "onupdate", "LerpMaterial", "oncomplete", "TriggerFlicker"));
    }

    public void TriggerFlicker()
    {
        //Debug.Log("stage 2");
        if (parent != null)
        {
            if (parent.GetComponent<TankStatusColor>())
            {
                objectToColorChange.GetComponent<Renderer>().material = endMaterial;
                parent.GetComponent<TankStatusColor>().ResetPrevFillLevel();
            }
            //else if (parent.GetComponent<HealthTankLights>())
            //{
            //    objectToColorChange.GetComponent<Renderer>().material = endMaterial;
            //    parent.GetComponent<HealthTankLights>().SetTankLightMaterial(3);
            //}
        }
        else
        {
            objectToColorChange.GetComponent<Renderer>().material = endMaterial;
            PingPongMaterial(endMaterial, startMaterial, fadeTime);
        }
    }

    public void PingPongMaterial(Material startMat, Material endMat, float time)
    {
        startMaterial = startMat;
        endMaterial = endMat;
        iTween.ValueTo(objectToColorChange, iTween.Hash(
            "from", 0f, "to", 1f,
            "time", time, "easetype", "linear",
            "onupdate", "LerpMaterial", "looptype", "pingPong"));
    }
}
