using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBayControls : ControlScheme
{
    public int numOfBombs;
    public float reloadTime;
    public GameObject bombPrefab;

    private GameObject currentBomb;
    private ReloadManager rm;
    private bool reloading = false;
    private float timeReloading = 0.0f;
    private float distanceBelowShip = 20f;
    private float distanceInFrontShip = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Quaternion rotation = this.transform.rotation * Quaternion.Euler(-90, 0, 0);
        currentBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);
        rm = this.GetComponentInParent<ReloadManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !reloading && numOfBombs > 0)
        {
            DropBomb();
            rm.ReloadWeapon(reloadTime);
        }
        else if (reloading)
        {
            reloading = rm.getReloadingStatus();
            if (!reloading)
            {
                ReloadBay();
            }
        }
    }

    private void DropBomb()
    {
        currentBomb.GetComponent<BombController>().Drop();
        reloading = true;
        numOfBombs--;
    }

    private void ReloadBay()
    {
        //if (timeReloading >= reloadTime)
        //{
            Quaternion rotation = this.transform.rotation * Quaternion.Euler(-90, 0, 0);
            currentBomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x, this.transform.position.y - distanceBelowShip, this.transform.position.z + distanceInFrontShip), rotation, this.transform.parent);

        /*    reloading = false;
            timeReloading = 0f;
        }
        else
        {

            timeReloading += Time.deltaTime;
        }*/
    }
}
