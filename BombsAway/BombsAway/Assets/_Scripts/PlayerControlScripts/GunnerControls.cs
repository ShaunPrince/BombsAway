using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerControls : ControlScheme
{
    public float lookSensitivity;

    public float minXLook;
    public float maxXLook;
    public float minYLook;
    public float maxYLook;

    private float startingYRotation;
    private float startingXRotation;

    [SerializeField]
    private Camera myCamera;

    override public void SetActiveControl(bool active)
    {
        base.SetActiveControl(active);
        myCamera.transform.parent.GetComponentInChildren<PlayerGunController>().enabled = active;
        myCamera.transform.parent.GetComponentInChildren<ShootGun>().enabled = active;
        Cursor.lockState = CursorLockMode.Locked;
        //this.GetComponentInChildren<GunnerUIController>().enabled = true;
    }

    //private GameObject myGun;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        myCamera = this.transform.parent.GetComponentInChildren<Camera>();
        startingXRotation = myCamera.transform.localEulerAngles.x;
        startingYRotation = this.transform.parent.localEulerAngles.y;
        //myGun = this.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isActiveControlScheme == true)
        {
            Look();
        }
    }

    private void Look()
    {
        
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        //Rotation about x yields a up down change
        if (myCamera.transform.localRotation.eulerAngles.x + -deltaY * lookSensitivity <= maxXLook ||
            myCamera.transform.localRotation.eulerAngles.x + -deltaY * lookSensitivity >= minXLook)
        {
            myCamera.transform.Rotate(-deltaY * lookSensitivity, 0f, 0f);
        }
        //Go back later and force constraints
        //myCamera.transform.Rotate(-deltaY * lookSensitivity, 0f, 0f);

        if (this.transform.parent.localRotation.eulerAngles.y + deltaX * lookSensitivity <= maxYLook ||
            this.transform.parent.localRotation.eulerAngles.y + deltaX * lookSensitivity >= minYLook)
        {
            this.transform.parent.Rotate(0f, deltaX * lookSensitivity, 0f);

        }

        //Go back and force constraints
        //this.transform.parent.Rotate(0f, deltaX * lookSensitivity, 0f);

    }
}

