using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPipeShape : MonoBehaviour
{
    public GameObject models;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (!this.transform.GetChild(i).gameObject.activeSelf) {
                models.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
