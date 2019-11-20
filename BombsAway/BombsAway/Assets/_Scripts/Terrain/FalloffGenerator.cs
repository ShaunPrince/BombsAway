using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FalloffGenerator
{
    private static Dictionary<Vector2, float> wholeFalloffMap = new Dictionary<Vector2, float>();
    private static int worldRadius = 20000;

    //public static Sprite falloffMapRenderer;


    public static float[,] GenerateFallofMap(int size)
    {
        float[,] map = new float[size,size];

        for (int i = 0; i < size; i++)  {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1; // *2-1 to get a number in the range of 0-1
                float y = j / (float)size * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                map[i, j] = Evaluate(value);
            }
        }

        return map;
    }

    // get mesh scale from meshSettings and use it to calculate the real world position of the chunk
    public static float[,] GenerateFallofMap(int size, Vector2 center, NoiseSettings settings, Sprite falloffSprite)
    {
        //falloffMapRenderer = settings.falloffRenderer;

        float[,] map = new float[size, size];
        /*
        System.Random seededRandomness = new System.Random(settings.seed);

        float halfSize = size / 2f;    // so that scaling is centered
        
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {

                //float noiseHeight = 0;

                //for (int i = 0; i < settings.octaves; i++)
                //{
                // the higher the frequency, the further the sample points, more radical height changes
                //float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;  
                //float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

                float sampleX = (x - halfSize + seededRandomness.Next(-100000, 100000) + center.x) / settings.scale;   // so that landmass does not change with offset
                float sampleY = (y - halfSize + seededRandomness.Next(-100000, 100000) + center.y) / settings.scale;
                Vector3 sampleVector = new Vector3(sampleX, 0f, sampleY);

                //Debug.Log($"{sampleX}, {sampleY}; {halfSize}; {center}");
                
                if (Mathf.Abs(Vector3.Distance(sampleVector, Vector3.zero)) <= 28000)
                {                                                     
                    // Get point on falloffMapObject                  
                    //Vector3 point = new Vector3(sampleX, 200, sampleY);
                    //RaycastHit hit;
                    //Physics.Raycast(point, Vector3.down, out hit);
                    //Debug.Log($"{Physics.Raycast(point, Vector3.down, out hit)}, {hit.transform.name}");
                    //Vector2 pixelUV = hit.textureCoord;
                    Texture2D falloffTexture = falloffSprite.texture;
                    Color colorValue = falloffTexture.GetPixel((int)sampleX/28000 * falloffTexture.width, (int)sampleY / 28000 * falloffTexture.height);
                    float falloffValue = 1 - colorValue.r;  // black is zero

                    Debug.Log($"color: {colorValue}, falloffValue: {falloffValue} @ sampleX: {sampleX}, sampleY: {sampleY}");

                    map[x, y] = Evaluate(falloffValue);
                }
                else
                {
                    map[x, y] = Evaluate(1);
                }
            }
        }*/

        return map;
    }


    // decreases the effect of the falloff map, making the falloff only affect the edges
    static float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;

        // x^a / ( x^a + (b - bx)^a)    formula for a nice sin like curve _/-

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}
