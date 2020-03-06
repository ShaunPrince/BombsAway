using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLightTrigger : MonoBehaviour
{
    public GameObject bombLight;
    public Material onMaterial;
    public Material offMaterial;
    private float timeAmount = 5f;
    // Start is called before the first frame update
    void Start()
    {
        bombLight.GetComponent<MaterialTweening>().PingPongMaterial(onMaterial, offMaterial, timeAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
