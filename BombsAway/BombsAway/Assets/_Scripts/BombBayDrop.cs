using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBayDrop : MonoBehaviour
{
    public float reloadTime;
    public GameObject bombPrefab;
    public Collider playerCollider;

    private bool reloading;
    private float timeReloading;
    private GameObject myBomb;

    // Start is called before the first frame update
    void Start()
    {
        reloading = false;
        timeReloading = 0.0f;
        myBomb = this.transform.GetChild(0).gameObject;
        //Physics.IgnoreCollision(myBomb.GetComponentInChildren<Collider>(), playerCollider);
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
        myBomb.GetComponent<BombController>().Drop();
        Rigidbody bombRB = myBomb.GetComponent<Rigidbody>();
        bombRB.useGravity = true;
        reloading = true;
        //ReloadBay();
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
            GameObject newBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 10.0f), this.transform.rotation);
            newBomb.GetComponent<BombController>().allegiance = this.transform.GetComponentInParent<DamageableEntity>().allegiance;
        }
    }
}
