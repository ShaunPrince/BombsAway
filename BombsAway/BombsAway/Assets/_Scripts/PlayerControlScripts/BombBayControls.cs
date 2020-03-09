using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBayControls : ControlScheme
{
    public int numOfBombs;
    public float reloadTime;
    public GameObject bombPrefab;
    public Transform dropPoint;

    private GameObject currentBomb;
    private bool dropLeftBomb = true;   // which bomb to drop
    private GameObject leftBomb;
    private GameObject rightBomb;
    private GameObject prevDroppedBomb;
    private ReloadManager rm;
    private bool reloading = false;
    private float timeReloading = 0.0f;
    private float distanceToSideShip = 27f;
    private float distanceBelowShip = 21f;
    private float distanceInFrontShip = 10f;

    private BombDropController bdc;

    public GameObject GetMostRecentDroppedBomb()
    {
        return prevDroppedBomb;
    }

    public bool IsReloading()
    {
        return reloading;
    }

    // Start is called before the first frame update
    void Awake()
    {
        bdc = this.GetComponentInParent<BombDropController>();
        //Quaternion rotation = this.transform.rotation * Quaternion.Euler(-90, 0, 0);
        //currentBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
        //leftBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x - distanceToSideShip, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
        //rightBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x + distanceToSideShip, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
        //reloading = true;
        rm = this.GetComponentInParent<ReloadManager>();
    }

    public void InitialLoad()
    {
        rm.ReloadWeapon(reloadTime);
        reloading = rm.getReloadingStatus();
        ReloadBay();
        UpdateBombToDrop();
        ReloadBay();
        UpdateBombToDrop();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.W)) && 
            !reloading &&  numOfBombs > 0)
        {
            DropBomb();
            rm.ReloadWeapon(reloadTime);
            if (numOfBombs > 1 )
            {
                ReloadBay();
            }
            UpdateBombToDrop();
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
        if (dropLeftBomb)
        {
            leftBomb.GetComponent<DropBombAnimation>().Animate();
            bdc.Drop(leftBomb);
            prevDroppedBomb = leftBomb;
        }
        else
        {
            rightBomb.GetComponent<DropBombAnimation>().Animate();
            bdc.Drop(rightBomb);
            prevDroppedBomb = rightBomb;
        }
        reloading = true;
        numOfBombs--;
    }

    private void ReloadBay()
    {
        //if (timeReloading >= reloadTime)
        //{
            Quaternion rotation = this.transform.parent.rotation * Quaternion.Euler(0, 0, 0);
        if (dropLeftBomb)
        {
            // this.transform.parent.position.x - distanceToSideShip, this.transform.parent.position.y - distanceBelowShip, this.transform.parent.position.z
            leftBomb = Instantiate(bombPrefab, new Vector3(0,0,0), rotation, this.transform.parent);
            leftBomb.transform.localPosition = new Vector3( -distanceToSideShip, -distanceBelowShip, distanceInFrontShip);
            Transform model = leftBomb.transform.Find("BombModel").transform;
            model.localPosition = new Vector3(-distanceToSideShip*2, model.localPosition.y, model.localPosition.z);
            leftBomb.GetComponent<DropBombAnimation>().SetDropPoint(dropPoint);
            leftBomb.GetComponent<BombReloadingAnimation>().ReloadAnimation(distanceToSideShip*2, reloadTime);
        }
        else
        {
            rightBomb = Instantiate(bombPrefab, new Vector3(0, 0, 0), rotation, this.transform.parent); //Instantiate(bombPrefab, new Vector3(this.transform.parent.position.x + distanceToSideShip, this.transform.parent.position.y - distanceBelowShip, this.transform.parent.position.z), rotation, this.transform.parent);
            rightBomb.transform.localPosition = new Vector3(distanceToSideShip, -distanceBelowShip, distanceInFrontShip);
            Transform model = rightBomb.transform.Find("BombModel").transform;
            model.localPosition = new Vector3(distanceToSideShip * 2, model.localPosition.y, model.localPosition.z);
            rightBomb.GetComponent<DropBombAnimation>().SetDropPoint(dropPoint);
            rightBomb.GetComponent<BombReloadingAnimation>().ReloadAnimation(-distanceToSideShip*2, reloadTime);
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

    private void UpdateBombToDrop()
    {
        if (dropLeftBomb)
        {
            dropLeftBomb = false;
        }
        else
        {
            dropLeftBomb = true;
        }
    }
}
