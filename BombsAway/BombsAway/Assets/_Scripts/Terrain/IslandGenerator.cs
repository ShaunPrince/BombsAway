using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{
    public bool randomGeneration;
    public Vector2 numOfTerrainChunks;

    public int colliderLODIndex;
    public LevelOfDetailInfo[] detailLevels;

    public MeshSettings meshSettings;
    public HeightMapSettings heightMapSettings;
    public TexturedTextureData textureSettings;

    public Material mapMaterial;
    public Sprite fallOffSprite;

    float meshWorldSize;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> visibleTerrainChunks = new List<TerrainChunk>();

    private bool finishedTerrainSpawn = false;
    private bool allTerrianHasMesh = false;

    private float time = 0f;
    private float maxTime = 3f;

    private Dictionary<Vector2, bool> usedPositions = new Dictionary<Vector2, bool>();

    public bool FinishedTerrainGeneration()
    {
        return finishedTerrainSpawn;
    }

    private void Start()
    {
        Random.InitState(SeedRandomGeneration.GetRandomSeed());
        heightMapSettings.noiseSettings.seed = SeedRandomGeneration.GetRandomSeed();
        Debug.Log($"World Seed: {SeedRandomGeneration.GetRandomSeed()}");

        textureSettings.ApplyToMaterial(mapMaterial);
        textureSettings.UpdateMeshHeights(mapMaterial, heightMapSettings.minHeight, heightMapSettings.maxHeight);

        //mapGenerator = FindObjectOfType<MapGenerator>();

        float maxViewDistance = detailLevels[detailLevels.Length - 1].visibleDstThreshold;

        meshWorldSize = meshSettings.meshWorldSize;

        // spawn the chunks unorderly, needs to be islands for it to look good too
        if (randomGeneration && heightMapSettings.useFalloff)
        {
            UpdateVisibleChuncksRandomly();
        }
        // else if not random terrain generation, spawn the chunks as a grid
        else
        {
            UpdateVisibleChuncks(); // might not eval to true in update upon start
        }

        finishedTerrainSpawn = true;
    }

    private void Update()
    {
        if (!finishedTerrainSpawn &&  time < maxTime)
        {
            time += Time.deltaTime;
        }
        else if (!finishedTerrainSpawn && time >= maxTime)
        {
            finishedTerrainSpawn = true;
        }
        /*
        if (!finishedTerrainSpawn)
        {
            bool allHaveMesh = true;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (this.transform.GetChild(i).GetComponent<MeshCollider>().sharedMesh == null)
                {
                    allHaveMesh = false;
                    break;
                }
            }
            if (allHaveMesh) finishedTerrainSpawn = true;
        }
        */
        //if (!finishedTerrainSpawn && this.transform.childCount == Mathf.RoundToInt(numOfTerrainChunks.x) * Mathf.RoundToInt(numOfTerrainChunks.y))
        //{
        //    finishedTerrainSpawn = true;
        //}
    }

    void UpdateVisibleChuncks()
    {

        Vector2 chunkCenter = new Vector2(0, 0);
        int numChunks = Mathf.RoundToInt(numOfTerrainChunks.x) * Mathf.RoundToInt(numOfTerrainChunks.y);

        for (int y = 0; y < Mathf.RoundToInt(numOfTerrainChunks.y); y++)
        {
            for (int x = 0; x < Mathf.RoundToInt(numOfTerrainChunks.x); x++) {
                chunkCenter.x = x;
                chunkCenter.y = y;

                TerrainChunk newChunk = new TerrainChunk(chunkCenter, heightMapSettings, meshSettings, detailLevels, colliderLODIndex, this.transform, mapMaterial);
                terrainChunkDictionary.Add(chunkCenter, newChunk);
            }
        }
    }

    void UpdateVisibleChuncksRandomly()
    {

        Vector2 chunkCenter = new Vector2(0, 0);
        int numChunks = Mathf.RoundToInt(numOfTerrainChunks.x) * Mathf.RoundToInt(numOfTerrainChunks.y);

        for (int y = 0; y < Mathf.RoundToInt(numOfTerrainChunks.y); y++)
        {
            for (int x = 0; x < Mathf.RoundToInt(numOfTerrainChunks.x); x++)
            {
                chunkCenter = FindGoodPosition();

                TerrainChunk newChunk = new TerrainChunk(chunkCenter, heightMapSettings, meshSettings, detailLevels, colliderLODIndex, this.transform, mapMaterial);
                terrainChunkDictionary.Add(chunkCenter, newChunk);
            }
        }
    }

    Vector2 FindGoodPosition()
    {
        int randMax = 3;
        Vector2 position = new Vector2(Random.Range(0, randMax), Random.Range(0, randMax));
        while (usedPositions.ContainsKey(position))
        {
            position = new Vector2(Random.Range(0, randMax), Random.Range(0, randMax));
        }

        usedPositions.Add(position, true);
        return position;
    }
}

[System.Serializable]
public struct LevelOfDetailInfo
{
    [Range(0, MeshSettings.numSupportedLODs - 1)]
    public int levelOfDetail;
    public float visibleDstThreshold;

    public float sqrVisibleDstThreshold
    {
        get
        {
            return visibleDstThreshold * visibleDstThreshold;
        }
    }
}

