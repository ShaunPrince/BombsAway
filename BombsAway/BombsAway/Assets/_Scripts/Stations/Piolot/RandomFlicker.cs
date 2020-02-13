using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlicker : MonoBehaviour
{
    public Vector2 density;
    public FlickerObject[] objectsToFlicker;

    private float maxTimer = 0f;
    private float timer = 0f;

    private void Start()
    {
        maxTimer = ChooseTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= maxTimer)
        {
            int randObject = Random.Range(0, objectsToFlicker.Length);
            ChangeMaterial(randObject);
            timer = 0f;
            maxTimer = ChooseTimer();
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private float ChooseTimer()
    {
        return Random.Range(density.x, density.y);
    }

    private void ChangeMaterial(int objectIndex)
    {
        if (objectsToFlicker[objectIndex].on)
        {
            //Debug.Log($"Chnaging object {objectIndex} from {objectsToFlicker[objectIndex].model.GetComponent<Renderer>().material} to {objectsToFlicker[objectIndex].offMaterial}");
            objectsToFlicker[objectIndex].model.GetComponent<Renderer>().material = objectsToFlicker[objectIndex].offMaterial;
            objectsToFlicker[objectIndex].on = false;
        }
        else
        {
            //Debug.Log($"Chnaging object {objectIndex} from {objectsToFlicker[objectIndex].model.GetComponent<Renderer>().material} to {objectsToFlicker[objectIndex].onMaterial}");
            objectsToFlicker[objectIndex].model.GetComponent<Renderer>().material = objectsToFlicker[objectIndex].onMaterial;
            objectsToFlicker[objectIndex].on = true;
        }

    }

}

[System.Serializable]
public class FlickerObject
{
    public GameObject model;
    public Material onMaterial;
    public Material offMaterial;
    public bool on;
}
