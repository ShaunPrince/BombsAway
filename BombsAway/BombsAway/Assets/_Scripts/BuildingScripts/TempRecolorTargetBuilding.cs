﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRecolorTargetBuilding : MonoBehaviour
{
    public Material tempTargetColor;
    public GameObject pollution;
    private bool colorUpdated = false;
    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<TerrainObject>().objectType == ETerrainObjectType.Target && !colorUpdated)
        {
            Transform model = this.transform.GetChild(0);
            for (int i = 0; i < model.childCount; i++)
            {
                if (model.GetChild(i).TryGetComponent<MeshRenderer>(out MeshRenderer updateRenderer))
                {
                    model.GetChild(i).GetComponent<MeshRenderer>().material = tempTargetColor;
                }
            }

            this.transform.GetChild(1).GetComponent<MeshRenderer>().material = tempTargetColor;

            if (pollution != null)
                pollution.SetActive(true);

            colorUpdated = true;
        }
    }
}
