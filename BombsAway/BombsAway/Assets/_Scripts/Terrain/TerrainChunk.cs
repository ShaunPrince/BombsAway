﻿using UnityEngine;

public class TerrainChunk
{
    const float colliderGenerationDistanceThreshold = 20000;

    //public event System.Action<TerrainChunk, bool> onVisibilityChanged;
    public Vector2 coord;

    GameObject meshObject;
    Vector2 sampleCenter;
    Bounds bounds;

    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    LevelOfDetailInfo[] detailLevels;
    int prevLevelOfDetailIndex = -1;    // dont update lod if it is the same as last time
    LevelOfDetailMesh levelOfDetailMesh;
    //LevelOfDetailMesh collisionLODMesh;
    int colliderLODIndex;

    HeightMap heightMap;
    bool heightMapReceived = false;
    bool hasSetCollider = false;
    float maxViewDistance;

    HeightMapSettings heightMapSettings;
    MeshSettings meshSettings;

    //Transform viewer;

    private float meshYposition = -3000f;

    public TerrainChunk(Vector2 coord, HeightMapSettings heightMapSettings, MeshSettings meshSettings, LevelOfDetailInfo[] detailLevels, int colliderLODIndex, Transform parent, Material material)
    {
        this.coord = coord;
        this.detailLevels = detailLevels;
        this.colliderLODIndex = colliderLODIndex;
        this.heightMapSettings = heightMapSettings;
        this.meshSettings = meshSettings;

        sampleCenter = coord * meshSettings.meshWorldSize / meshSettings.meshScale;
        Vector2 position = coord * 10000;   // this is a random number, should probably fix it later >.<
        bounds = new Bounds(position, Vector2.one * meshSettings.meshWorldSize);

        //meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        meshObject = new GameObject("TerrainChunk");
        meshRenderer = meshObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
        meshFilter = meshObject.AddComponent<MeshFilter>(); // when adding, returns object that was added
        meshCollider = meshObject.AddComponent<MeshCollider>();

        meshObject.transform.position = new Vector3(position.x, meshYposition, position.y);
        meshObject.layer = 9;
        //meshObject.transform.localScale = Vector3.one * size / 10f; // default scale is 10 units (for planes)
        meshObject.transform.parent = parent;
        SetVisible(true);

        // make a mesh for each level of detail
        levelOfDetailMesh = new LevelOfDetailMesh(colliderLODIndex);
        //for (int i = 0; i < detailLevels.Length; i++)
        //{
            //levelOfDetailMeshes[i] = new LevelOfDetailMesh(detailLevels[i].levelOfDetail);
            //if (detailLevels[i].useForCollider) collisionLODMesh = levelOfDetailMeshes[i];

            // since no longer doing it in constructor
            //levelOfDetailMeshes[i].updateCallback += UpdateTerrainChunk;
            //if (i == colliderLODIndex) levelOfDetailMeshes[i].updateCallback += UpdateCollisionMesh;
        //}

        //maxViewDistance = detailLevels[detailLevels.Length - 1].visibleDstThreshold;

        this.heightMap = HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, meshSettings, sampleCenter);

        levelOfDetailMesh.RequestMesh(heightMap, meshSettings);
        meshFilter.mesh = levelOfDetailMesh.mesh;
        meshCollider.sharedMesh = levelOfDetailMesh.mesh;

        //mapGenerator.RequestHeightMap(sampleCenter, OnMapDataRecieved);
    }

    /*
    public void Load()
    {
        // () creates a new lambda with no parameters that returns what is on the right of the =>
        ThreadedDataRequester.RequestData(() => HeightMapGenerator.GenerateHeightMap(meshSettings.numVertsPerLine, meshSettings.numVertsPerLine, heightMapSettings, meshSettings, sampleCenter), OnHeightMapReceived);
    }*/

    /*
    private void OnHeightMapReceived(object heightMapObject)
    {
        // when recieve the mapData, we want to store it
        this.heightMap = (HeightMap)heightMapObject;
        heightMapReceived = true;

        UpdateTerrainChunk();
    }*/
    /*
    void OnMeshDataRecieved(MeshData meshData)
    {
        meshFilter.mesh = meshData.CreateMesh();
    }*/

    
    // tell terrian chunk to update itself
    // find point on parameter that is closest to viewers position, find that distance
    // if the distance is less then the max view distance, make sure mesh is enabled, else dissable mesh object
    // dependeing on the distance, display mesh with appropriate level of detail
    //public void UpdateTerrainChunk()
    //{

        //float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));

        //int levelOfDetailIndex = 1; // keep consistent level of detail

        /*
        if (levelOfDetailIndex != prevLevelOfDetailIndex)
        {
            LevelOfDetailMesh levelOfDetailMesh = levelOfDetailMeshes[levelOfDetailIndex];
            if (levelOfDetailMesh.hasMesh)
            {
                prevLevelOfDetailIndex = levelOfDetailIndex;
                meshFilter.mesh = levelOfDetailMesh.mesh;
                //meshCollider.sharedMesh = levelOfDetailMesh.mesh; // creates too high of complexity mesh
            }
            else if (!levelOfDetailMesh.hasRequestedMesh)
            {
                levelOfDetailMesh.RequestMesh(heightMap, meshSettings);
            }
        }*/

        //levelOfDetailMesh.RequestMesh(heightMap, meshSettings);
        //meshFilter.mesh = levelOfDetailMesh.mesh;

        // only if the player is close enough, generate the collider (slighty lower then need be as well)
        /*if (levelOfDetailIndex == 0) {
            if (collisionLODMesh.hasMesh) meshCollider.sharedMesh = collisionLODMesh.mesh;
            else if (!collisionLODMesh.hasRequestedMesh) collisionLODMesh.RequestMesh(mapData);
        }*/

        //visibleTerrainChunks.Add(this);   // to fix terrain chunks getting displayed but not added to list
        /*
        // only update visibility if it has changed
        if (wasVisible != visible)
        {
            //if (visible) visibleTerrainChunks.Add(this);
            //else visibleTerrainChunks.Remove(this);

            SetVisible(visible);

            if (onVisibilityChanged != null) onVisibilityChanged(this, visible);
        }*/
    //}

        /*
    public void UpdateCollisionMesh()
    {
        if (!hasSetCollider)
        {
            float sqrDistanceFromViewerToEdge = bounds.SqrDistance(viewerPosition);

            if (sqrDistanceFromViewerToEdge < detailLevels[colliderLODIndex].sqrVisibleDstThreshold)
            {
                if (!levelOfDetailMeshes[colliderLODIndex].hasRequestedMesh)
                {
                    levelOfDetailMeshes[colliderLODIndex].RequestMesh(heightMap, meshSettings);
                }
            }

            if (sqrDistanceFromViewerToEdge < colliderGenerationDistanceThreshold * colliderGenerationDistanceThreshold)
            {
                if (levelOfDetailMeshes[colliderLODIndex].hasMesh)
                {
                    meshCollider.sharedMesh = levelOfDetailMeshes[colliderLODIndex].mesh;
                    hasSetCollider = true;
                }
            }
        }
    }*/

    public void SetVisible(bool visible)
    {
        meshObject.SetActive(visible);
    }

    public bool isVisible()
    {
        return meshObject.activeSelf;
    }
}

class LevelOfDetailMesh
{
    public Mesh mesh;
    public bool hasRequestedMesh = false;
    public bool hasMesh = false;
    int levelOfDetail;
    //public event System.Action updateCallback;

    public LevelOfDetailMesh(int lod)
    {
        levelOfDetail = lod;
        //this.updateCallback = updateCallback;
    }

    /*
    void OnMeshDataReceived(object meshDataObject)
    {
        mesh = ((MeshData)meshDataObject).CreateMesh();
        hasMesh = true;

        updateCallback();   // have to manually update the mesh
    }*/

    public void RequestMesh(HeightMap heightMap, MeshSettings meshSettings)
    {
        hasRequestedMesh = true;
        //mapGenerator.RequestMeshData(mapData, levelOfDetail, OnMeshDataReceived);
        mesh = MeshGenerator.GenerateTerrainMesh(heightMap.values, meshSettings, levelOfDetail).CreateMesh();
        hasMesh = true;
    }
}