using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GentleCamMoveStation : ControlScheme
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

    //private GameObject myGun;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        myCamera = this.transform.parent.GetComponentInChildren<Camera>();
        startingXRotation = myCamera.transform.localEulerAngles.x;
        startingYRotation = myCamera.transform.parent.localEulerAngles.z;
        //myGun = this.transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    protected void LateUpdate()
    {
        if (this.isActiveControlScheme == true)
        {
            Look();
        }
    }

    private void Look()
    {

        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        //Rotation about x yields a up down change
        if (myCamera.transform.localRotation.eulerAngles.x + -deltaY * lookSensitivity <= 90 + maxXLook ||
            myCamera.transform.localRotation.eulerAngles.x + -deltaY * lookSensitivity >= 90 + minXLook)
        {
            myCamera.transform.Rotate(-deltaY * lookSensitivity, 0f, 0f);
        }
        //Go back later and force constraints
        //myCamera.transform.Rotate(-deltaY * lookSensitivity, 0f, 0f);

        if (myCamera.transform.parent.localRotation.eulerAngles.z + deltaX * lookSensitivity <= maxYLook ||
            myCamera.transform.parent.localRotation.eulerAngles.z + deltaX * lookSensitivity >= minYLook)
        {
            myCamera.transform.parent.Rotate(0f, 0f,deltaX * lookSensitivity);

        }

        //Go back and force constraints
        //this.transform.parent.Rotate(0f, deltaX * lookSensitivity, 0f);

    }
}
