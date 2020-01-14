using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [Range(0,1)]
    public float colorDifference;

    public GameObject enemyModels;
    private Color[] originalColor;

    private bool enemyHit;
    private float time = 0f;
    private float maxTime = .2f;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = new Color[enemyModels.transform.childCount];


        for (int i = 0; i < originalColor.Length; i++)
        {
            Renderer renderer;
            if (enemyModels.transform.GetChild(i).TryGetComponent<Renderer>(out renderer))
            {
                originalColor[i] = renderer.material.GetColor("_BaseColor");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHit)
        {
            if (time > maxTime)
            {
                enemyHit = false;
                time = 0f;

                for (int i = 0; i < originalColor.Length; i++)
                {
                    Renderer renderer;
                    if (enemyModels.transform.GetChild(i).TryGetComponent<Renderer>(out renderer))
                    {
                        MaterialPropertyBlock block = new MaterialPropertyBlock();
                        block.SetColor("_BaseColor", originalColor[i]);

                        enemyModels.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(block);
                    }
                }
            }
            else
            {
                time += Time.deltaTime;
            }
        }

    }

    public void VisuallyShowEnemyHit()
    {
        enemyHit = true;

        for (int i = 0; i < originalColor.Length; i++)
        {
            Renderer renderer;
            if (enemyModels.transform.GetChild(i).TryGetComponent<Renderer>(out renderer))
            {
                Color newColor = new Color(originalColor[i].a + colorDifference, originalColor[i].b + colorDifference, originalColor[i].g + colorDifference);
                MaterialPropertyBlock block = new MaterialPropertyBlock();
                block.SetColor("_BaseColor", newColor);


                enemyModels.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(block);
                //Debug.Log($"og color: {originalColor[i]}, new color: {newColor}, current color: {enemyModels.transform.GetChild(i).GetComponent<Renderer>().material.color}");
            }
        }
    }
}
