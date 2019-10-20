using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_test_script : MonoBehaviour
{
    public GameObject bullet;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        bullet.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 1.0f) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
