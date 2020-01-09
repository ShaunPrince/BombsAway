using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEntity : MonoBehaviour
{
    private float worldCenterX = 28200;
    private float worldCenterZ = 28200;
    private float worldRadius = 42000;

    private float sizeOfChunk = 28200;

    private void Awake()
    {
        float numChunks = GameObject.FindWithTag("MapGenerator").GetComponent<IslandGenerator>().numOfTerrainChunks.x;

        worldCenterX = (sizeOfChunk * (numChunks - 1  > 0 ? numChunks - 1 : 1)) / 2;
        worldCenterZ = (sizeOfChunk * (numChunks - 1 > 0 ? numChunks - 1 : 1)) / 2;

        worldRadius = (sizeOfChunk * numChunks) / 2;
        worldRadius = (sizeOfChunk * numChunks) / 2;

        //Debug.Log($"Num chunks: {numChunks}, World center: ({worldCenterX}, {worldCenterZ}), World radius: {worldRadius}");
    }

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
        try
        {
            UnityEditor.Handles.DrawWireCube(debugPos, debugSize);
            UnityEditor.Handles.DrawWireDisc(debugPos, this.transform.up, worldRadius);
        }
        catch
        {

        }
#endif
    }

}
