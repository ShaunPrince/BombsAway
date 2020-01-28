using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    //[Range(0,1)]
    //public float colorDifference;

    public GameObject enemyModels;
    //private Color[] originalColor;
    private Material[] originalMaterial;
    public Material hitMaterial;

    private bool enemyHit;
    private float time = 0f;
    private float maxTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //originalColor = new Color[enemyModels.transform.childCount];
        originalMaterial = new Material[enemyModels.transform.childCount];


        for (int i = 0; i < originalMaterial.Length; i++)
        {
            Renderer renderer;
            if (enemyModels.transform.GetChild(i).TryGetComponent<Renderer>(out renderer))
            {
                //originalColor[i] = renderer.material.GetColor("_BaseColor");
                originalMaterial[i] = renderer.material;
                enemyModels.transform.GetChild(i).gameObject.AddComponent<MaterialTweening>();
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

                for (int i = 0; i < originalMaterial.Length; i++)
                {
                    MaterialTweening materialMerger; ;
                    if (enemyModels.transform.GetChild(i).TryGetComponent<MaterialTweening>(out materialMerger))
                    {
                        //MaterialPropertyBlock block = new MaterialPropertyBlock();
                        //block.SetColor("_BaseColor", originalColor[i]);

                        //enemyModels.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(block);
                        materialMerger.MergeMaterial(enemyModels.transform.GetChild(i).gameObject,hitMaterial, originalMaterial[i], maxTime);
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

        for (int i = 0; i < originalMaterial.Length; i++)
        {
            MaterialTweening materialMerger;
            if (enemyModels.transform.GetChild(i).TryGetComponent<MaterialTweening>(out materialMerger))
            {
                //Color newColor = new Color(originalColor[i].a + colorDifference, originalColor[i].b + colorDifference, originalColor[i].g + colorDifference);
                //MaterialPropertyBlock block = new MaterialPropertyBlock();
                //block.SetColor("_BaseColor", newColor);

                //enemyModels.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(block);

                materialMerger.MergeMaterial(enemyModels.transform.GetChild(i).gameObject, originalMaterial[i], hitMaterial, maxTime);
                //Debug.Log($"og color: {originalColor[i]}, new color: {newColor}, current color: {enemyModels.transform.GetChild(i).GetComponent<Renderer>().material.color}");
            }
        }
    }
}
