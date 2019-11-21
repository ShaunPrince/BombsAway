using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBayDrop : MonoBehaviour
{
    public int numOfBombs;
    public float reloadTime;
    public GameObject bombPrefab;

    private GameObject currentBomb;
    private bool reloading = false;
    private float timeReloading = 0.0f;
    private float distanceBelowShip = 20f;
    private float distanceInFrontShip = 5f;
    //private GameObject myBomb;

    // Start is called before the first frame update
    void Start()
    {
        currentBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), Quaternion.Euler(-90,0,0), this.transform);
        //myBomb = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !reloading && numOfBombs > 0)
        {
            DropBomb();
        }
        else if (reloading)
        {
            ReloadBay();
        }
    }

    private void DropBomb()
    {
        currentBomb.GetComponent<BombController>().SetToDrop();
        reloading = true;
        numOfBombs--;
    }

    private void ReloadBay()
    {
        if (timeReloading >= reloadTime)
        {           
            currentBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), Quaternion.Euler(-90, 0, 0), this.transform);

            reloading = false;
            timeReloading = 0f;
        }
        else
        {

            timeReloading += Time.deltaTime;
        }
    }
}
