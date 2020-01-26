using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBayControls : ControlScheme
{
    public int numOfBombs;
    public float reloadTime;
    public GameObject bombPrefab;

    private GameObject currentBomb;
    private bool dropLeftBomb = true;   // which bomb to drop
    private GameObject leftBomb;
    private GameObject rightBomb;
    private ReloadManager rm;
    private bool reloading = false;
    private float timeReloading = 0.0f;
    private float distanceToSideShip = 20f;
    private float distanceBelowShip = 20f;
    private float distanceInFrontShip = 0f;

    private BombDropController bdc;

    // Start is called before the first frame update
    void Awake()
    {
        bdc = this.GetComponentInParent<BombDropController>();
        //Quaternion rotation = this.transform.rotation * Quaternion.Euler(-90, 0, 0);
        //currentBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
        //leftBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x - distanceToSideShip, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
        //rightBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x + distanceToSideShip, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
        reloading = true;
        ReloadBay();
        ReloadBay();
        rm = this.GetComponentInParent<ReloadManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !reloading &&  numOfBombs > 0)
        {
            DropBomb();
            rm.ReloadWeapon(reloadTime);
            ReloadBay();
        }
        else if (reloading)
        {
            reloading = rm.getReloadingStatus();
            if (!reloading)
            {
                
            }
        }
    }

    private void DropBomb()
    {
        if (dropLeftBomb) bdc.Drop(leftBomb);
        else bdc.Drop(rightBomb);
        reloading = true;
        numOfBombs--;
    }

    private void ReloadBay()
    {
        //if (timeReloading >= reloadTime)
        //{
            Quaternion rotation = this.transform.rotation * Quaternion.Euler(-90, 0, 0);
        if (dropLeftBomb)
        {
            leftBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x - distanceToSideShip, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
            leftBomb.GetComponent<BombReloadingAnimation>().ReloadAnimation(distanceToSideShip/2, reloadTime);
            dropLeftBomb = false;
        }
        else
        {
            rightBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x + distanceToSideShip, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
            rightBomb.GetComponent<BombReloadingAnimation>().ReloadAnimation(-distanceToSideShip/2, reloadTime);
            dropLeftBomb = true;
        }
            //currentBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);

        /*    reloading = false;
            timeReloading = 0f;
        }
        else
        {

            timeReloading += Time.deltaTime;
        }*/
    }
}
