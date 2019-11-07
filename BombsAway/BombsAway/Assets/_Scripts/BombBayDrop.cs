using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBayDrop : MonoBehaviour
{
    public float reloadTime;
    public GameObject bomb;

    private bool reloading;
    private float timeReloading;

    // Start is called before the first frame update
    void Start()
    {
        reloading = false;
        timeReloading = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !reloading)
        {
            dropBomb();
        }
    }

    private void dropBomb()
    {
        bomb.GetComponent<BombController>().Drop();
        reloading = true;
        ReloadBay();
    }

    private void ReloadBay()
    {
        if (timeReloading < reloadTime)
        {
            timeReloading += Time.deltaTime;
        }
        else
        {
            reloading = false;
            //make the bomb here
        }
    }
}
