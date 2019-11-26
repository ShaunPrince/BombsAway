using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RadarEnemyPing : MonoBehaviour
{
    private Image img;
    private float timer;
    public float maxTimer;
    public Color color;
    
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponentInChildren<Image>();
        //maxTimer = 1f;
        timer = 0;
        //color = new Color(1, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        color.a = Mathf.Lerp(maxTimer, 0f, timer / maxTimer);
        img.color = color;

        if (timer >= maxTimer) {
            Destroy(gameObject);
        }
    }
}
