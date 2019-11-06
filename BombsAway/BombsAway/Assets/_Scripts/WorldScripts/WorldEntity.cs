using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEntity : MonoBehaviour
{
    private float worldCenterX = 0;
    private float worldCenterZ = 0;
    private float worldRadius = 20000;

    public Vector2 WorldCenter {
        get {
            return new Vector2(worldCenterX, worldCenterZ);
        }
    
    
    }

    public float WorldLength
    {
        get {
            return worldRadius;
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        // draw spawn radius
        Vector3 debugPos = new Vector3(WorldCenter.x, 100, WorldCenter.y);
        Vector3 debugSize = new Vector3(worldRadius*2, 1, worldRadius*2);
        UnityEditor.Handles.DrawWireCube(debugPos, debugSize);
#endif
    }

}
